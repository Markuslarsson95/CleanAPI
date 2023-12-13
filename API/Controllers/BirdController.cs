using Application.Commands.Birds;
using Application.Dtos;
using Application.Queries.Birds;
using Application.Queries.Birds.GetAll;
using Domain.Models.Animals;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdController : ControllerBase
    {
        internal readonly IMediator _mediator;

        public BirdController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all birds from database
        [HttpGet]
        [Route("getAllBirds")]
        [ProducesResponseType(typeof(List<Bird>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBirds()
        {
            return Ok(await _mediator.Send(new GetAllBirdsQuery()));
        }

        // Get a bird by Id
        [HttpGet]
        [Route("getBirdById/{birdId}")]
        [ProducesResponseType(typeof(Bird), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBirdById(Guid birdId)
        {
            var getBirdResult = await _mediator.Send(new GetBirdByIdQuery(birdId));

            if (getBirdResult == null)
                return NotFound($"Bird with ID {birdId} not found");

            return Ok(getBirdResult);
        }

        // Create a new bird 
        [HttpPost]
        [Route("addNewBird")]
        [Authorize]
        [ProducesResponseType(typeof(Bird), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBird([FromBody] BirdDto newBird, IValidator<AddBirdCommand> validator)
        {
            var addBirdCommand = new AddBirdCommand(newBird);

            var validatorResult = await validator.ValidateAsync(addBirdCommand);

            if (!validatorResult.IsValid)
            {
                return ValidationProblem(validatorResult.ToString());
            }

            await _mediator.Send(addBirdCommand);

            return Ok(addBirdCommand);
        }

        // Update a specific bird
        [HttpPut]
        [Route("updateBird/{birdId}")]
        [Authorize]
        [ProducesResponseType(typeof(Bird), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBirdById([FromBody] BirdDto updatedBird, Guid birdId, IValidator<UpdateBirdByIdCommand> validator)
        {
            var updateBirdCommand = new UpdateBirdByIdCommand(updatedBird, birdId);
            var updatedBirdResult = await _mediator.Send(updateBirdCommand);

            if (updatedBirdResult == null)
                return NotFound($"Bird with ID {birdId} not found");

            var validatorResult = await validator.ValidateAsync(updateBirdCommand);

            if (!validatorResult.IsValid)
                return ValidationProblem(validatorResult.ToString());

            return Ok(updateBirdCommand);
        }

        // Delete bird by id
        [HttpDelete]
        [Route("deleteBird/{birdId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Bird), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBirdById(Guid birdId)
        {
            var birdToDelete = await _mediator.Send(new DeleteBirdByIdCommand(birdId));

            if (birdToDelete == null)
                return NotFound($"Bird with ID {birdId} not found");

            return Ok(birdToDelete);
        }
    }
}
