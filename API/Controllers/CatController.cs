using Application.Commands.Cats;
using Application.Dtos;
using Application.Queries.Cats.GetAll;
using Application.Queries.Cats.GetById;
using Application.Validators.CatValidators;
using Domain.Models.Animals;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        // Get all cats from database
        [HttpGet]
        [Route("getAllCats")]
        [ProducesResponseType(typeof(List<Cat>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCats()
        {
            try
            {
                return Ok(await _mediator.Send(new GetAllCatsQuery()));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
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
                    return NotFound($"Cat with ID {catId} not found");

                return Ok(getCatResult);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
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
            var validatorResult = _catValidator.Validate(newCat);

            if (!validatorResult.IsValid)
            {
                return BadRequest(validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage));
            }

            try
            {
                return Ok(await _mediator.Send(new AddCatCommand(newCat)));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
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
            var validatorResult = _catValidator.Validate(updatedCat);

            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors.ConvertAll(errors => errors.ErrorMessage));

            try
            {
                var updatedCatResult = await _mediator.Send(new UpdateCatByIdCommand(updatedCat, catId));

                if (updatedCatResult == null)
                    return NotFound($"Cat with ID {catId} not found");

                return Ok(updatedCatResult);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
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
                var catToDelete = await _mediator.Send(new DeleteCatByIdCommand(catId));

                if (catToDelete == null)
                    return NotFound($"Cat with ID {catId} not found");

                return Ok(catToDelete);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
