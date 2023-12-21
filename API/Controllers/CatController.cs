using Application.Commands.Cats;
using Application.Dtos;
using Application.Exceptions;
using Application.Queries.Cats.GetAll;
using Application.Queries.Cats.GetById;
using Application.Validators.CatValidators;
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
    public class CatController : ControllerBase
    {
        internal readonly IMediator _mediator;
        internal readonly CatValidator _catValidator;

        public CatController(IMediator mediator, CatValidator catValidator)
        {
            _mediator = mediator;
            _catValidator = catValidator;
        }

        // Get all cats from the database
        [HttpGet]
        [Route("getAllCats")]
        [ProducesResponseType(typeof(List<Cat>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCats(string? sortByBreed = null, int? sortByWeight = null)
        {
            try
            {
                var catListResult = await _mediator.Send(new GetAllCatsQuery(sortByBreed, sortByWeight));

                Log.Information("Cat List found: {@catListResult}", catListResult);
                return Ok(catListResult);
            }
            catch (ArgumentException ex)
            {
                Log.Error(ex, "An unexpected error occurred.");
                return BadRequest(ex.Message);
            }
        }

        // Get a cat by Id
        [HttpGet]
        [Route("getCatById/{catId}")]
        [ProducesResponseType(typeof(Cat), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCatById(Guid catId)
        {
            try
            {
                var getCatResult = await _mediator.Send(new GetCatByIdQuery(catId));

                if (getCatResult == null)
                {
                    Log.Warning($"Cat with Id {catId} not found.");
                    throw new EntityNotFoundException("Cat", catId);
                }

                Log.Information("Cat found: {@getCatResult}", getCatResult);
                return Ok(getCatResult);
            }
            catch (EntityNotFoundException ex)
            {
                Log.Error(ex, $"Cat not found with Id {catId}");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        // Create a new cat
        [HttpPost]
        [Route("addNewCat")]
        [Authorize]
        [ProducesResponseType(typeof(Cat), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCat([FromBody] CatDto newCat)
        {
            try
            {
                Log.Information("Validating CatDto");

                var validatorResult = _catValidator.Validate(newCat);

                if (!validatorResult.IsValid)
                {
                    Log.Warning("Validation of CatDto failed");
                    var validationErrors = validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage);
                    throw new ValidationErrorException(newCat, validationErrors);
                }

                Log.Information("CatDto validation successful. Adding new cat: {@newCat}", newCat);

                return Ok(await _mediator.Send(new AddCatCommand(newCat)));
            }
            catch (ValidationErrorException ex)
            {
                Log.Error(ex, "Validation error when adding cat");
                return BadRequest(ex.ValidationErrors);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred while processing AddCatCommand");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
            }
        }

        // Update a specific cat
        [HttpPut]
        [Route("updateCat/{catId}")]
        [Authorize]
        [ProducesResponseType(typeof(Cat), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCatById([FromBody] CatDto updatedCat, Guid catId)
        {
            try
            {
                Log.Information($"Updating cat with ID {catId}");

                var validatorResult = _catValidator.Validate(updatedCat);

                if (!validatorResult.IsValid)
                {
                    Log.Warning("Validation of CatDto failed");
                    var validationErrors = validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage);
                    throw new ValidationErrorException(updatedCat, validationErrors);
                }

                var updatedCatResult = await _mediator.Send(new UpdateCatByIdCommand(updatedCat, catId));

                if (updatedCatResult == null)
                {
                    Log.Warning($"No cat found to update with ID {catId}");
                    throw new EntityNotFoundException("Cat", catId);
                }

                Log.Information("Successfully updated cat, updated cat: {@updatedCatResult}", updatedCatResult);
                return Ok(updatedCatResult);
            }
            catch (EntityNotFoundException ex)
            {
                Log.Error(ex, $"Cat not found with Id {catId} during update");
                return NotFound(ex.Message);
            }
            catch (ValidationErrorException ex)
            {
                Log.Error(ex, "Validation error when updating cat");
                return BadRequest(ex.ValidationErrors);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An unexpected error occurred while updating cat with ID {catId}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
            }
        }

        // Delete cat by id
        [HttpDelete]
        [Route("deleteCat/{catId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Cat), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCatById(Guid catId)
        {
            try
            {
                Log.Information($"Deleting cat with ID {catId}");
                var catToDelete = await _mediator.Send(new DeleteCatByIdCommand(catId));

                if (catToDelete == null)
                {
                    Log.Warning($"No cat found to remove with ID {catId}");
                    throw new EntityNotFoundException("Cat", catId);
                }

                Log.Information("Successfully removed cat, removed cat: {@catToDelete}", catToDelete);
                return Ok(catToDelete);
            }
            catch (EntityNotFoundException ex)
            {
                Log.Error(ex, $"Cat not found with Id {catId} during remove");
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
