using Domain.Models;
using Domain.Models.Animals;
using Infrastructure.Repositories.Password;
using Infrastructure.Repositories.Users;
using MediatR;

namespace Application.Commands.Users.AddUser
{
    public sealed class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordEncryptor _passwordEncryptor;

        public AddUserCommandHandler(IUserRepository userRepository, IPasswordEncryptor passwordEncryptor)
        {
            _userRepository = userRepository;
            _passwordEncryptor = passwordEncryptor;
        }

        public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var userToCreate = new User
            {
                Id = Guid.NewGuid(),
                UserName = request.NewUser.UserName,
                Password = _passwordEncryptor.Encrypt(request.NewUser.Password)
            };
            List<Animal> animals = userToCreate.Animals ?? new List<Animal>();

            var dogs = request.NewUser.Dogs.Select(d => new Dog { Name = d.Name, Breed = d.Breed, Weight = d.Weight, Users = new List<User> { userToCreate } }).ToList();
            var cats = request.NewUser.Cats.Select(c => new Cat { Name = c.Name, Breed = c.Breed, LikesToPlay = c.LikesToPlay, Weight = c.Weight, Users = new List<User> { userToCreate } }).ToList();
            var birds = request.NewUser.Birds.Select(b => new Bird { Name = b.Name, Color = b.Color, CanFly = b.CanFly, Users = new List<User> { userToCreate } }).ToList();

            var combinedList = dogs.Cast<Animal>().Concat(cats).Concat(birds).ToList();

            userToCreate.Animals = animals.Concat(combinedList).ToList();

            await _userRepository.Add(userToCreate);

            return await Task.FromResult(userToCreate);
        }
    }
}
