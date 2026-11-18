var builder = DistributedApplication.CreateBuilder(args);

const string dataBaseName = "FitnessClub";
var passwordDb = builder.AddParameter("PostgresPassword", true);

var fitnessClubDb = builder.AddPostgres("postgres", password: passwordDb)
    .WithPgAdmin()
    .AddDatabase(dataBaseName);

builder.AddDotnetProject("FitnessClubApi", "../FitnessClub.Server/FitnessClub.Server.csproj")
    .WithReference(fitnessClubDb, "Postgres")
    .WaitFor(fitnessClubDb);
    
builder.Build().Run();