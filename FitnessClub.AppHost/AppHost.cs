var builder = DistributedApplication.CreateBuilder(args);

const string dataBaseName = "FitnessClub";
var password = builder.AddParameter("PostgresPassword", true);

var fitnessClubDb = builder.AddPostgres("postgres", password).AddDatabase(dataBaseName);

builder.AddProject<Projects.FitnessClub_Server>("FitnessClubApi")
    .WithReference(fitnessClubDb, "Postgres")
    .WaitFor(fitnessClubDb);
    
builder.Build().Run();