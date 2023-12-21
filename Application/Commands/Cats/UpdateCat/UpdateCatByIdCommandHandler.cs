using Domain.Models.Animals;
using Infrastructure.Repositories.Cats;
using MediatR;
using Serilog;

namespace Application.Commands.Cats
{
    public class UpdateCatByIdCommandHandler : IRequestHandler<UpdateCatByIdCommand, Cat>
    {
        private readonly ICatRepository _catRepository;

        public UpdateCatByIdCommandHandler(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }
        public async Task<Cat> Handle(UpdateCatByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information($"Updating cat with ID {request.Id}");

                var catToUpdate = await _catRepository.GetById(request.Id);

                if (catToUpdate == null)
                {
                    Log.Warning($"Cat with Id {request.Id} not found.");
                    return await Task.FromResult<Cat>(null!);
                }

                catToUpdate.Name = request.UpdatedCat.Name;
                catToUpdate.LikesToPlay = request.UpdatedCat.LikesToPlay;
                catToUpdate.Breed = request.UpdatedCat.Breed;
                catToUpdate.Weight = request.UpdatedCat.Weight;

                await _catRepository.Update(catToUpdate);

                return catToUpdate;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred while handling UpdateCatByIdCommand for cat with ID {request.Id}");
                throw new Exception($"Error occurred while handling UpdateCatByIdCommand for cat with ID {request.Id}", ex);
            }
        }
    }
}
