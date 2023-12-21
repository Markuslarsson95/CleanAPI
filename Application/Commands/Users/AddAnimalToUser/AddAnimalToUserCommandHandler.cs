using Domain.Models;
using Domain.Models.Animals;
using Infrastructure.Repositories.Animals;
using Infrastructure.Repositories.Users;
using MediatR;
using Serilog;

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
            try
            {
                Log.Information($"Adding animal to user - UserId: {request.AnimalToUser.UserId}, AnimalId: {request.AnimalToUser.AnimalId}");

                var user = await _userRepository.GetById(request.AnimalToUser.UserId);

                if (user == null)
                {
                    Log.Warning($"User with UserId {request.AnimalToUser.UserId} not found.");
                    return await Task.FromResult<User>(null!);
                }

                var animal = await _animalRepository.GetAnimalById(request.AnimalToUser.AnimalId);
                if (animal == null)
                {
                    Log.Warning($"Animal with AnimalId {request.AnimalToUser.AnimalId} not found.");
                    return await Task.FromResult<User>(null!);
                }

                user.Animals.Add(animal);
                await _userRepository.Update(user);

                Log.Information($"Added animal to user successfully - UserId: {request.AnimalToUser.UserId}, AnimalId: {request.AnimalToUser.AnimalId}");

                return user;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while adding animal to user");
                throw new Exception("An error occurred while adding animal to user", ex);
            }
        }
    }
}
