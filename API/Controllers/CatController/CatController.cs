using Application.Commands.Cats;
using Application.Dtos;
using Application.Queries.Cats.GetAll;
using Application.Queries.Cats.GetById;
using Domain.Models;
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
        public async Task<IActionResult> AddCat([FromBody] CatDto newCat)
        {
            var response = await _mediator.Send(new AddCatCommand(newCat));

            return Ok(response);
            //return Ok(await _mediator.Send(new AddCatCommand(newCat)));
        }

        // Update a specific cat
        [HttpPut]
        [Route("updateCat/{catId}")]
        public async Task<IActionResult> UpdateCatById([FromBody] CatDto updatedCat, Guid catId)
        {
            return Ok(await _mediator.Send(new UpdateCatByIdCommand(updatedCat, catId)));
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
