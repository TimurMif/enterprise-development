using FitnessClub.Application.Services;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Data;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;
using FitnessClub.Infrastructure.InMemory.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSingleton(sp => DataSeeder.Seed());

builder.Services.AddSingleton<IRepository<Client>, ClientRepository>();
builder.Services.AddSingleton<IRepository<Trainer>, TrainerRepository>();
builder.Services.AddSingleton<IRepository<TrainingSession>, TrainingSessionRepository>();
builder.Services.AddSingleton<IRepository<GymHall>, GymHallRepository>();
builder.Services.AddSingleton<IRepository<Specialization>, SpecializationRepository>();

builder.Services.AddSingleton<IClientService, ClientService>();
builder.Services.AddSingleton<ITrainerService, TrainerService>();
builder.Services.AddSingleton<ITrainingSessionService, TrainingSessionService>();
builder.Services.AddSingleton<IGymHallService, GymHallService>();
builder.Services.AddSingleton<ISpecializationService, SpecializationService>();
builder.Services.AddSingleton<IAnalyticsService, AnalyticsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();

app.Run();