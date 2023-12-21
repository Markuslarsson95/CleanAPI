using Domain.Models;
using Domain.Models.Animals;
using Infrastructure.Repositories.Animals;
using Infrastructure.Repositories.Users;
using MediatR;
using Serilog;

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
            try
            {
                Log.Information($"Removing animal from user - UserId: {request.RemoveAnimalFromUser.UserId}, AnimalId: {request.RemoveAnimalFromUser.AnimalId}");

                var user = await _userRepository.GetById(request.RemoveAnimalFromUser.UserId);

                if (user == null)
                {
                    Log.Warning($"User with UserId {request.RemoveAnimalFromUser.UserId} not found.");
                    return await Task.FromResult<User>(null!);
                }

                var animal = await _animalRepository.GetAnimalById(request.RemoveAnimalFromUser.AnimalId);
                if (animal == null)
                {
                    Log.Warning($"Animal with AnimalId {request.RemoveAnimalFromUser.AnimalId} not found.");
                    return await Task.FromResult<User>(null!);
                }

                user.Animals.Remove(animal);
                await _userRepository.Update(user);

                Log.Information($"Removed animal from user successfully - UserId: {request.RemoveAnimalFromUser.UserId}, AnimalId: {request.RemoveAnimalFromUser.AnimalId}");

                return user;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while removing animal from user");
                throw new Exception("An error occurred while removing animal from user", ex);
            }
        }
    }
}
