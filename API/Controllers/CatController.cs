using Application.Commands.Cats;
using Application.Dtos;
using Application.Queries.Cats.GetAll;
using Application.Queries.Cats.GetById;
using Domain.Models.Animal;
using FluentValidation;
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

        public CatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all cats from database
        [HttpGet]
        [Route("getAllCats")]
        [ProducesResponseType(typeof(List<Cat>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCats()
        {
            return Ok(await _mediator.Send(new GetAllCatsQuery()));
        }

        // Get a cat by Id
        [HttpGet]
        [Route("getCatById/{catId}")]
        [ProducesResponseType(typeof(Cat), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCatById(Guid catId)
        {
            var getCatResult = await _mediator.Send(new GetCatByIdQuery(catId));

            if (getCatResult == null)
                return NotFound($"Cat with ID {catId} not found");

            return Ok(getCatResult);
        }

        // Create a new cat 
        [HttpPost]
        [Route("addNewCat")]
        [Authorize]
        [ProducesResponseType(typeof(Cat), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCat([FromBody] CatDto newCat, IValidator<AddCatCommand> validator)
        {
            var addCatCommand = new AddCatCommand(newCat);

            var validatorResult = await validator.ValidateAsync(addCatCommand);

            if (!validatorResult.IsValid)
            {
                return ValidationProblem(validatorResult.ToString());
            }

            await _mediator.Send(addCatCommand);

            return Ok(addCatCommand);
        }

        // Update a specific cat
        [HttpPut]
        [Route("updateCat/{catId}")]
        [Authorize]
        [ProducesResponseType(typeof(Cat), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCatById([FromBody] CatDto updatedCat, Guid catId, IValidator<UpdateCatByIdCommand> validator)
        {
            var updateCatCommand = new UpdateCatByIdCommand(updatedCat, catId);
            var updatedCatResult = await _mediator.Send(updateCatCommand);

            if (updatedCatResult == null)
                return NotFound($"Cat with ID {catId} not found");

            var validatorResult = await validator.ValidateAsync(updateCatCommand);

            if (!validatorResult.IsValid)
                return ValidationProblem(validatorResult.ToString());

            return Ok(updateCatCommand);
        }

        // Delete cat by id
        [HttpDelete]
        [Route("deleteCat/{catId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(Cat), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCatById(Guid catId)
        {
            var catToDelete = await _mediator.Send(new DeleteCatByIdCommand(catId));

            if (catToDelete == null)
                return NotFound($"Cat with ID {catId} not found");

            return Ok(catToDelete);
        }
    }
}
