using Domain.Models;
using Domain.Models.Animals;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Commands.Users.AddUser
{
    public sealed class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public AddUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var userToCreate = new User
            {
                Id = Guid.NewGuid(),
                UserName = request.NewUser.UserName,
                Password = request.NewUser.Password
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
