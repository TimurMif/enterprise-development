using FitnessClub.Application.Services;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;
using FitnessClub.Infrastructure.EfCore;
using FitnessClub.Infrastructure.EfCore.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddNpgsqlDbContext<FitnessClubDbContext>("Postgres");

builder.Services.AddControllers();
builder.Services.AddOpenApi();

//builder.Services.AddSingleton(sp => DataSeeder.Seed());

builder.Services.AddScoped<IRepository<Client>, ClientEfCoreRepository>();
builder.Services.AddScoped<IRepository<Trainer>, TrainerEfCoreRepository>();
builder.Services.AddScoped<IRepository<TrainingSession>, TrainingSessionEfCoreRepository>();
builder.Services.AddScoped<IRepository<GymHall>, GymHallEfCoreRepository>();
builder.Services.AddScoped<IRepository<Specialization>, SpecializationEfCoreRepository>();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ITrainerService, TrainerService>();
builder.Services.AddScoped<ITrainingSessionService, TrainingSessionService>();
builder.Services.AddScoped<IGymHallService, GymHallService>();
builder.Services.AddScoped<ISpecializationService, SpecializationService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();

app.Run();