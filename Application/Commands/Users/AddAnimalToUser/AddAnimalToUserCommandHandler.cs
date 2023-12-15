using Domain.Models;
using Domain.Models.Animals;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Commands.Users.AddAnimalToUser
{
    public class AddAnimalToUserCommandHandler : IRequestHandler<AddAnimalToUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAnimalRepository<Animal> _animalRepository;

        public AddAnimalToUserCommandHandler(IUserRepository userRepository, IAnimalRepository<Animal> animalRepository)
        {
            _userRepository = userRepository;
            _animalRepository = animalRepository;
        }
        public async Task<User> Handle(AddAnimalToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.AnimalToUser.UserId);

            if (user == null)
            {
                return await Task.FromResult<User>(null!);
            }

            var animal = await _animalRepository.GetAnimalById(request.AnimalToUser.AnimalId);
            if (animal == null)
            {
                return null!;
            }
            user.Animals.Add(animal);
            await _userRepository.Update(user);

            return user;
        }
    }
}
