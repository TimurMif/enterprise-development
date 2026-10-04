using FitnessClub.Domain.Data;
using FitnessClub.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton(sp => DataSeeder.Seed());

builder.Services.AddSingleton<IClientService, ClientService>();
builder.Services.AddSingleton<ITrainerService, TrainerService>();
builder.Services.AddSingleton<ITrainingSessionService, TrainingSessionService>();
builder.Services.AddSingleton<IAnalyticsService, AnalyticsService>();

var app = builder.Build();

app.MapControllers();

app.Run();
