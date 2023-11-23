﻿using Application.Commands.Dogs;
using Application.Commands.Dogs.UpdateDog;
using Application.Commands.Dogs.DeleteDog;
using Application.Dtos;
using Application.Queries.Dogs.GetAll;
using Application.Queries.Dogs.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.DogController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogController : ControllerBase
    {
        internal readonly IMediator _mediator;
        public DogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Get all dogs from database
        [HttpGet]
        [Route("getAllDogs")]
        public async Task<IActionResult> GetAllDogs()
        {
            return Ok(await _mediator.Send(new GetAllDogsQuery()));
            //return Ok("GET ALL DOGS");
        }

        // Get a dog by Id
        [HttpGet]
        [Route("getDogById/{dogId}")]
        public async Task<IActionResult> GetDogById(Guid dogId)
        {
            return Ok(await _mediator.Send(new GetDogByIdQuery(dogId)));
        }

        // Create a new dog 
        [HttpPost]
        [Route("addNewDog")]
        public async Task<IActionResult> AddDog([FromBody] DogDto newDog, IValidator<AddDogCommand> validator)
        {
            var addDogCommand = new AddDogCommand(newDog);

            var validatorResult = await validator.ValidateAsync(addDogCommand);

            if(!validatorResult.IsValid)
            {
                return ValidationProblem(validatorResult.ToString());
            }

            await _mediator.Send(addDogCommand);

            return Ok(addDogCommand);
        }

        // Update a specific dog
        [HttpPut]
        [Route("updateDog/{dogId}")]
        public async Task<IActionResult> UpdateDogById([FromBody] DogDto updatedDog, Guid dogId, IValidator<UpdateDogByIdCommand> validator)
        {
            var updateDogCommand = new UpdateDogByIdCommand(updatedDog, dogId);

            var validatorResult = await validator.ValidateAsync(updateDogCommand);

            if (!validatorResult.IsValid)
            {
                return ValidationProblem(validatorResult.ToString());
            }

            await _mediator.Send(updateDogCommand);

            return Ok(updateDogCommand);
        }

        // Delete dog by id
        [HttpDelete]
        [Route("deleteDog/{dogId}")]
        public async Task<IActionResult> DeleteDogById(Guid dogId)
        {
            return Ok(await _mediator.Send(new DeleteDogByIdCommand(dogId)));
        }

    }
}
