using Domain.Models.Animals;
using Infrastructure.Repositories.Cats;
using MediatR;
using Serilog;

namespace Application.Commands.Cats
{
    public class DeleteCatByIdCommandHandler : IRequestHandler<DeleteCatByIdCommand, Cat>
    {
        private readonly ICatRepository _catRepository;

        public DeleteCatByIdCommandHandler(ICatRepository catRepository)
        {
            _catRepository = catRepository;
        }

        public async Task<Cat> Handle(DeleteCatByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information($"Deleting cat with ID {request.Id}");

                var catToDelete = await _catRepository.GetById(request.Id);

                if (catToDelete == null)
                {
                    Log.Warning($"Cat with Id {request.Id} not found.");
                    return await Task.FromResult<Cat>(null!);
                }

                await _catRepository.Delete(catToDelete);

                return catToDelete;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occurred while handling DeleteCatByIdCommand for cat with ID {request.Id}");
                throw new Exception($"Error occurred while handling DeleteCatByIdCommand for cat with ID {request.Id}", ex);
            }
        }
    }
}
