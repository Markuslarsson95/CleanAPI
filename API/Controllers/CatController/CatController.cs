using Application.Commands.Cats;
using Application.Dtos;
using Application.Queries.Cats.GetAll;
using Application.Queries.Cats.GetById;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.CatController
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
            var cat = await _mediator.Send(new GetCatByIdQuery(catId));
            return cat == null ? NotFound() : Ok(cat);
            //return Ok(await _mediator.Send(new GetCatByIdQuery(catId)));
        }

        // Create a new cat 
        [HttpPost]
        [Route("addNewCat")]
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
        public async Task<IActionResult> UpdateCatById([FromBody] CatDto updatedCat, Guid catId, IValidator<UpdateCatByIdCommand> validator)
        {
            var updateCatCommand = new UpdateCatByIdCommand(updatedCat, catId);

            var validatorResult = await validator.ValidateAsync(updateCatCommand);

            if (!validatorResult.IsValid)
            {
                return ValidationProblem(validatorResult.ToString());
            }

            await _mediator.Send(updateCatCommand);

            return Ok(updateCatCommand);
        }

        // Delete cat by id
        [HttpDelete]
        [Route("deleteCat/{catId}")]
        public async Task<IActionResult> DeleteCatById(Guid catId)
        {
            return Ok(await _mediator.Send(new DeleteCatByIdCommand(catId)));
        }
    }
}
