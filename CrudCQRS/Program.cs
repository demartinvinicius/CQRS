using System.Reflection;
using CrudCQRS.Models;
using Microsoft.EntityFrameworkCore;
using Nudes.Retornator.AspnetCore;
using FluentValidation;
using CrudCQRS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly = Assembly.GetExecutingAssembly();

builder.Services.AddMediatR(assembly);

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

builder.Services
    .AddDbContext<ProductContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddControllers()
    .AddRetornator();

builder.Services.AddErrorTranslator(ErrorHttpTranslatorBuilder.Default);

AssemblyScanner.FindValidatorsInAssembly(assembly, true)
    .ForEach(r => builder.Services.AddTransient(r.InterfaceType, r.ValidatorType));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
