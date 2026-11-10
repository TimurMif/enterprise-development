var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FitnessClub_Application>("fitnessclub-application");

builder.AddProject<Projects.FitnessClub_Contracts>("fitnessclub-contracts");

builder.AddProject<Projects.FitnessClub_Domain>("fitnessclub-domain");

builder.AddProject<Projects.FitnessClub_Infrastructure_EfCore>("fitnessclub-infrastructure-efcore");

builder.AddProject<Projects.FitnessClub_Infrastructure_InMemory>("fitnessclub-infrastructure-inmemory");

builder.AddProject<Projects.FitnessClub_Server>("fitnessclub-server");

builder.AddProject<Projects.FitnessClub_Tests>("fitnessclub-tests");

builder.Build().Run();