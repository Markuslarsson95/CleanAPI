using Application;
using Application.Dtos;
using Domain.Models;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;
using Infrastructure;
using System;
using Application.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation(x =>
    {
        x.ImplicitlyValidateChildProperties = true;

        //Automatic register method to register the validators
        x.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
    });

//builder.Services.AddTransient<IValidator<Cat>, CatValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication().AddInfrastructure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
