﻿using Application.Commands.Birds;
using Application.Dtos;
using Application.Queries.Birds;
using Application.Queries.Birds.GetAll;
using Domain.Models;
using MediatR;
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
            var bird = await _mediator.Send(new GetBirdByIdQuery(birdId));
            return bird == null ? NotFound() : Ok(bird);
            //return Ok(await _mediator.Send(new GetBirdByIdQuery(birdId)));
        }

        // Create a new bird 
        [HttpPost]
        [Route("addNewBird")]
        public async Task<IActionResult> AddBird([FromBody] BirdDto newBird)
        {
            return Ok(await _mediator.Send(new AddBirdCommand(newBird)));
        }

        // Update a specific bird
        [HttpPut]
        [Route("updateBird/{birdId}")]
        public async Task<IActionResult> UpdateBirdById([FromBody] BirdDto updatedBird, Guid birdId)
        {
            return Ok(await _mediator.Send(new UpdateBirdByIdCommand(updatedBird, birdId)));
        }

        // Delete bird by id
        [HttpDelete]
        [Route("deleteBird/{birdId}")]
        public async Task<IActionResult> DeleteBirdById(Guid birdId)
        {
            return Ok(await _mediator.Send(new DeleteBirdByIdCommand(birdId)));
        }
    }
}
