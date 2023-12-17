using Application.Commands.Birds;
using Application.Dtos;
using Application.Queries.Birds;
using Application.Queries.Birds.GetAll;
using Application.Validators.BirdValidators;
using Domain.Models.Animals;
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
        internal readonly BirdValidator _birdValidator;

        public BirdController(IMediator mediator, BirdValidator birdValidator)
        {
            _mediator = mediator;
            _birdValidator = birdValidator;
        }

        // Get all birds from database
        [HttpGet]
        [Route("getAllBirds")]
        [ProducesResponseType(typeof(List<Bird>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllBirds(string? sortByColor = null)
        {
            try
            {
                return Ok(await _mediator.Send(new GetAllBirdsQuery(sortByColor)));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        // Get a bird by Id
        [HttpGet]
        [Route("getBirdById/{birdId}")]
        [ProducesResponseType(typeof(Bird), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBirdById(Guid birdId)
        {
            try
            {
                var getBirdResult = await _mediator.Send(new GetBirdByIdQuery(birdId));

                if (getBirdResult == null)
                    return NotFound($"Bird with ID {birdId} not found");

                return Ok(getBirdResult);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        // Create a new bird 
        [HttpPost]
        [Route("addNewBird")]
        [Authorize]
        [ProducesResponseType(typeof(Bird), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBird([FromBody] BirdDto newBird)
        {
            var validatorResult = _birdValidator.Validate(newBird);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage));
            }

            try
            {
                return Ok(await _mediator.Send(new AddBirdCommand(newBird)));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        // Update a specific bird
        [HttpPut]
        [Route("updateBird/{birdId}")]
        [Authorize]
        [ProducesResponseType(typeof(Bird), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBirdById([FromBody] BirdDto updatedBird, Guid birdId)
        {
            var validatorResult = _birdValidator.Validate(updatedBird);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage));
            }

            try
            {
                var updatedBirdResult = await _mediator.Send(new UpdateBirdByIdCommand(updatedBird, birdId));

                if (updatedBirdResult == null)
                    return NotFound($"Bird with ID {birdId} not found");

                return Ok(updatedBirdResult);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        // Delete bird by id
        [HttpDelete]
        [Route("deleteBird/{birdId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Bird), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBirdById(Guid birdId)
        {
            try
            {
                var birdToDelete = await _mediator.Send(new DeleteBirdByIdCommand(birdId));

                if (birdToDelete == null)
                    return NotFound($"Bird with ID {birdId} not found");

                return Ok(birdToDelete);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
