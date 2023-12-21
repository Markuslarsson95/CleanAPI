using Application.Commands.Birds;
using Application.Dtos;
using Application.Exceptions;
using Application.Queries.Birds;
using Application.Queries.Birds.GetAll;
using Application.Validators.BirdValidators;
using Domain.Models.Animals;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
                Log.Error(e, "An unexpected argument exception occurred.");
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

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
                {
                    Log.Warning($"Bird with Id {birdId} not found.");
                    throw new EntityNotFoundException("Bird", birdId);
                }

                Log.Information("Bird found: {@getBirdResult}", getBirdResult);
                return Ok(getBirdResult);
            }
            catch (EntityNotFoundException ex)
            {
                Log.Error(ex, $"Bird not found with Id {birdId}");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        [Route("addNewBird")]
        [Authorize]
        [ProducesResponseType(typeof(Bird), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBird([FromBody] BirdDto newBird)
        {
            try
            {
                Log.Information("Validating BirdDto");
                var validatorResult = _birdValidator.Validate(newBird);

                if (!validatorResult.IsValid)
                {
                    Log.Warning("Validation of BirdDto failed");
                    var validationErrors = validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage);
                    throw new ValidationErrorException(newBird, validationErrors);
                }

                return Ok(await _mediator.Send(new AddBirdCommand(newBird)));
            }
            catch (ValidationErrorException ex)
            {
                Log.Error(ex, "Validation error when adding bird");
                return BadRequest(ex.ValidationErrors);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred while processing AddBirdCommand");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
            }
        }

        [HttpPut]
        [Route("updateBird/{birdId}")]
        [Authorize]
        [ProducesResponseType(typeof(Bird), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBirdById([FromBody] BirdDto updatedBird, Guid birdId)
        {
            try
            {
                var validatorResult = _birdValidator.Validate(updatedBird);

                if (!validatorResult.IsValid)
                {
                    Log.Warning("Validation of BirdDto failed");
                    var validationErrors = validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage);
                    throw new ValidationErrorException(updatedBird, validationErrors);
                }

                var updatedBirdResult = await _mediator.Send(new UpdateBirdByIdCommand(updatedBird, birdId));

                if (updatedBirdResult == null)
                {
                    Log.Warning($"No bird found to update with ID {birdId}");
                    throw new EntityNotFoundException("Bird", birdId);
                }

                Log.Information("Successfully updated bird, updated bird: {@updatedBirdResult}", updatedBirdResult);
                return Ok(updatedBirdResult);
            }
            catch (EntityNotFoundException ex)
            {
                Log.Error(ex, $"Bird not found with Id {birdId} during update");
                return NotFound(ex.Message);
            }
            catch (ValidationErrorException ex)
            {
                Log.Error(ex, "Validation error when updating bird");
                return BadRequest(ex.ValidationErrors);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An unexpected error occurred while updating bird with ID {birdId}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
            }
        }

        [HttpDelete]
        [Route("deleteBird/{birdId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Bird), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBirdById(Guid birdId)
        {
            try
            {
                Log.Information($"Deleting bird with ID {birdId}");
                var birdToDelete = await _mediator.Send(new DeleteBirdByIdCommand(birdId));

                if (birdToDelete == null)
                {
                    Log.Warning($"No bird found to remove with ID {birdId}");
                    throw new EntityNotFoundException("Bird", birdId);
                }

                Log.Information("Successfully removed bird, removed bird: {@birdToDelete}", birdToDelete);
                return Ok(birdToDelete);
            }
            catch (EntityNotFoundException ex)
            {
                Log.Error(ex, $"Bird not found with Id {birdId} during remove");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
    }
}
