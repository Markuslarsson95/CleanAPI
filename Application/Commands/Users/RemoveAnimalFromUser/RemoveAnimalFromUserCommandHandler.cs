using Domain.Models;
using Domain.Models.Animals;
using Infrastructure.Repositories;
using MediatR;

namespace Application.Commands.Users.RemoveAnimalFromUser
{
    public class RemoveAnimalFromUserCommandHandler : IRequestHandler<RemoveAnimalFromUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAnimalRepository<Animal> _animalRepository;

        public RemoveAnimalFromUserCommandHandler(IUserRepository userRepository, IAnimalRepository<Animal> animalRepository)
        {
            _userRepository = userRepository;
            _animalRepository = animalRepository;
        }
        public async Task<User> Handle(RemoveAnimalFromUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.RemoveAnimalFromUser.UserId);

            if (user == null)
            {
                return await Task.FromResult<User>(null!);
            }

            var animal = await _animalRepository.GetAnimalById(request.RemoveAnimalFromUser.AnimalId);
            if (animal == null)
            {
                return await Task.FromResult<User>(null!);
            }
            user.Animals.Remove(animal);
            await _userRepository.Update(user);

            return user;
        }
    }
}
