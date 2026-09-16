using Bogus;
using Bogus.DataSets;
using FitnessClub.Domain.Context;
using FitnessClub.Domain.Enums;
using FitnessClub.Domain.Models;

namespace FitnessClub.Domain.Data;

public static class DataSeeder
{
    public static FitnessClubContext Seed(int seed = 42)
    {
        Randomizer.Seed = new Random(seed);
        var context = new FitnessClubContext();

        // 1. Специализации (10 шт.)
        var specNames = new[]
        {
            "Силовой тренинг", "Кроссфит", "Пилатес", "Йога", "Кардио",
            "Единоборства", "Тяжелая атлетика", "Стретчинг", "Реабилитация", "Аэробика"
        };
        context.Specializations = specNames.Select(name => new Specialization { Name = name }).ToList();

        // 2. Залы (10 шт.)
        var hallFaker = new Faker<GymHall>("ru")
            .RuleFor(h => h.Id, f => Guid.NewGuid())
            .RuleFor(h => h.Name, f => $"Зал {f.Commerce.Color()} {f.IndexGlobal}")
            .RuleFor(h => h.Capacity, f => f.Random.Number(10, 50));
        context.GymHalls = hallFaker.Generate(10);

        // Вспомогательные списки отчеств
        var malePatronymics = new[]
            { "Иванович", "Петрович", "Сергеевич", "Александрович", "Дмитриевич", "Алексеевич" };
        var femalePatronymics = new[]
            { "Ивановна", "Петровна", "Сергеевна", "Александровна", "Дмитриевна", "Алексеевна" };

        // 3. Клиенты (15 шт.)
        var clientFaker = new Faker<Client>("ru")
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.PassportNumber, f => f.Random.Replace("#### ######"))
            .RuleFor(c => c.Gender, f => f.PickRandom<Gender>())
            .RuleFor(c => c.FirstName,
                (f, c) => f.Name.FirstName(c.Gender == Gender.Female ? Name.Gender.Female : Name.Gender.Male))
            .RuleFor(c => c.LastName,
                (f, c) => f.Name.LastName(c.Gender == Gender.Female ? Name.Gender.Female : Name.Gender.Male))
            .RuleFor(c => c.MiddleName,
                (f, c) => f.PickRandom(c.Gender == Gender.Female ? femalePatronymics : malePatronymics))
            .RuleFor(c => c.DateOfBirth, f => DateOnly.FromDateTime(f.Date.Past(30, DateTime.Now.AddYears(-18))))
            .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber("+7 9## ###-##-##"))
            .RuleFor(c => c.SubscriptionStartDate, f => DateOnly.FromDateTime(f.Date.Past()))
            .RuleFor(c => c.SubscriptionEndDate,
                (f, c) => f.Date.BetweenDateOnly(c.SubscriptionStartDate.AddMonths(1),
                    c.SubscriptionStartDate.AddMonths(12)));
        context.Clients = clientFaker.Generate(15);

        // 4. Тренеры (10 шт.)
        var trainerFaker = new Faker<Trainer>("ru")
            .RuleFor(t => t.Id, f => Guid.NewGuid())
            .RuleFor(t => t.PassportNumber, f => f.Random.Replace("#### ######"))
            .RuleFor(t => t.Gender, f => f.PickRandom<Gender>())
            .RuleFor(t => t.FirstName,
                (f, t) => f.Name.FirstName(t.Gender == Gender.Female ? Name.Gender.Female : Name.Gender.Male))
            .RuleFor(t => t.LastName,
                (f, t) => f.Name.LastName(t.Gender == Gender.Female ? Name.Gender.Female : Name.Gender.Male))
            .RuleFor(t => t.MiddleName,
                (f, t) => f.PickRandom(t.Gender == Gender.Female ? femalePatronymics : malePatronymics))
            .RuleFor(t => t.DateOfBirth, f => DateOnly.FromDateTime(f.Date.Past(25, DateTime.Now.AddYears(-22))))
            .RuleFor(t => t.PhoneNumber, f => f.Phone.PhoneNumber("+7 9## ###-##-##"))
            .RuleFor(t => t.ExperienceYears, f => f.Random.Number(1, 15))
            .RuleFor(t => t.Specialization, f => f.PickRandom(context.Specializations))
            .RuleFor(t => t.SpecializationId, (f, t) => t.Specialization.Id);
        context.Trainers = trainerFaker.Generate(10);

        // 5. Занятия (30 шт.)
        var sessionFaker = new Faker<TrainingSession>("ru")
            .RuleFor(s => s.Id, f => Guid.NewGuid())
            .RuleFor(s => s.Client, f => f.PickRandom(context.Clients))
            .RuleFor(s => s.ClientId, (f, s) => s.Client.Id)
            .RuleFor(s => s.Trainer, f => f.PickRandom(context.Trainers))
            .RuleFor(s => s.TrainerId, (f, s) => s.Trainer.Id)
            .RuleFor(s => s.GymHall, f => f.PickRandom(context.GymHalls))
            .RuleFor(s => s.GymHallId, (f, s) => s.GymHall.Id)
            .RuleFor(s => s.DateTime, f => f.Date.Between(DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1)))
            .RuleFor(s => s.Duration, f => TimeSpan.FromHours(1))
            .RuleFor(s => s.IsTrial, f => f.Random.Bool(0.2f));
        context.TrainingSessions = sessionFaker.Generate(30);

        return context;
    }
}