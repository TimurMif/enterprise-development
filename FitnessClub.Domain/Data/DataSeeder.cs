using FitnessClub.Domain.Context;
using FitnessClub.Domain.Enums;
using FitnessClub.Domain.Models;

namespace FitnessClub.Domain.Data;

/// <summary>
/// Формирование набора данных фитнес-клуба
/// </summary>
public static class DataSeeder
{
    /// <summary>
    /// Опорная дата, относительно которой построены все данные
    /// </summary>
    public static readonly DateTime ReferenceDate =
        new(2025, 6, 15, 12, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// Опорная дата для проверки абонементов
    /// </summary>
    public static readonly DateOnly ReferenceToday = DateOnly.FromDateTime(ReferenceDate);

    /// <summary>
    /// Создаёт контекст с фиксированным набором данных.
    /// </summary>
    public static FitnessClubContext Seed()
    {
        var context = new FitnessClubContext();

        SeedSpecializations(context);
        SeedGymHalls(context);
        SeedClients(context);
        SeedTrainers(context);
        SeedTrainingSessions(context);

        return context;
    }

    private static void SeedSpecializations(FitnessClubContext context)
    {
        var names = new[]
        {
            "Силовой тренинг", "Кроссфит", "Пилатес", "Йога", "Кардио",
            "Единоборства", "Тяжелая атлетика", "Стретчинг", "Реабилитация", "Аэробика"
        };

        for (var i = 0; i < names.Length; i++)
        {
            context.Specializations.Add(new Specialization
            {
                Id = i + 1,
                Name = names[i]
            });
        }
    }

    private static void SeedGymHalls(FitnessClubContext context)
    {
        var data = new (string Name, int Capacity)[]
        {
            ("Зал Йоги", 15),
            ("Зал Кроссфита", 30),
            ("Зал Кардио", 40),
            ("Зал Силовой", 25),
            ("Зал Единоборств", 20),
            ("Зал Пилатеса", 15),
            ("Зал Аэробики", 35),
            ("Зал Стретчинга", 12),
            ("Зал Реабилитации", 10),
            ("Универсальный зал", 50)
        };

        for (var i = 0; i < data.Length; i++)
        {
            context.GymHalls.Add(new GymHall
            {
                Id = i + 1,
                Name = data[i].Name,
                Capacity = data[i].Capacity
            });
        }
    }

    private static void SeedClients(FitnessClubContext context)
    {
        // Первые 5 с абонементом, истёкшим относительно опорной даты.
        // Остальные 10 с действующим.
        var data = new (string Last, string First, string Middle, Gender Gender,
                        DateOnly Start, DateOnly End)[]
        {
            ("Смирнов",   "Алексей",  "Иванович",    Gender.Male,   new(2024, 6, 1),   new(2025, 5, 1)),
            ("Кузнецова", "Мария",    "Петровна",    Gender.Female, new(2024, 7, 15),  new(2025, 6, 1)),
            ("Попов",     "Дмитрий",  "Сергеевич",   Gender.Male,   new(2024, 8, 10),  new(2025, 5, 20)),
            ("Соколова",  "Анна",     "Владимировна", Gender.Female, new(2024, 9, 1),  new(2025, 4, 30)),
            ("Лебедев",   "Никита",   "Андреевич",   Gender.Male,   new(2024, 10, 1),  new(2025, 6, 10)),
            ("Козлов",    "Артём",    "Максимович",  Gender.Male,   new(2025, 1, 1),   new(2025, 12, 31)),
            ("Новикова",  "Екатерина","Дмитриевна",  Gender.Female, new(2025, 2, 1),   new(2026, 2, 1)),
            ("Морозов",   "Егор",     "Алексеевич",  Gender.Male,   new(2025, 3, 1),   new(2026, 3, 1)),
            ("Петрова",   "Ольга",    "Николаевна",  Gender.Female, new(2025, 4, 1),   new(2026, 4, 1)),
            ("Волков",    "Илья",     "Романович",   Gender.Male,   new(2025, 5, 1),   new(2026, 5, 1)),
            ("Соловьёва", "Дарья",    "Кирилловна",  Gender.Female, new(2024, 12, 1),  new(2025, 12, 1)),
            ("Васильев",  "Максим",   "Тимурович",   Gender.Male,   new(2025, 1, 15),  new(2026, 1, 15)),
            ("Зайцева",   "Виктория", "Андреевна",   Gender.Female, new(2025, 2, 15),  new(2026, 2, 15)),
            ("Павлов",    "Кирилл",   "Денисович",   Gender.Male,   new(2025, 3, 15),  new(2026, 3, 15)),
            ("Семёнова",  "Алиса",    "Артёмовна",   Gender.Female, new(2025, 4, 15),  new(2026, 4, 15))
        };

        for (var i = 0; i < data.Length; i++)
        {
            var d = data[i];
            context.Clients.Add(new Client
            {
                Id = i + 1,
                PassportNumber = $"4000 {100000 + i:D6}",
                LastName = d.Last,
                FirstName = d.First,
                MiddleName = d.Middle,
                Gender = d.Gender,
                DateOfBirth = new DateOnly(1985 + i % 15, 1 + i % 12, 1 + i % 28),
                PhoneNumber = $"+7 900 {100 + i:D3}-{10 + i:D2}-{20 + i:D2}",
                SubscriptionStartDate = d.Start,
                SubscriptionEndDate = d.End
            });
        }
    }

    private static void SeedTrainers(FitnessClubContext context)
    {
        // 7 тренеров со стажем >= 5 (позиции 4..10).
        // 3 тренера со стажем < 5 (позиции 1..3).
        var data = new (string Last, string First, string Middle, Gender Gender,
                        int Experience, int SpecIndex)[]
        {
            ("Иванов",     "Пётр",     "Сергеевич",      Gender.Male,   1,  1),
            ("Петров",     "Иван",     "Алексеевич",     Gender.Male,   3,  2),
            ("Сидоров",    "Николай",  "Михайлович",     Gender.Male,   4,  3),
            ("Орлова",     "Елена",    "Владимировна",   Gender.Female, 5,  4),
            ("Фёдоров",    "Андрей",   "Игоревич",       Gender.Male,   7,  5),
            ("Михайлова",  "Татьяна",  "Юрьевна",        Gender.Female, 8,  6),
            ("Белов",      "Виктор",   "Станиславович",  Gender.Male,   10, 7),
            ("Григорьева", "Наталья",  "Олеговна",       Gender.Female, 12, 8),
            ("Тихонов",    "Роман",    "Валерьевич",     Gender.Male,   13, 9),
            ("Егорова",    "Ирина",    "Борисовна",      Gender.Female, 15, 10)
        };

        for (var i = 0; i < data.Length; i++)
        {
            var d = data[i];
            var spec = context.Specializations.First(s => s.Id == d.SpecIndex);

            context.Trainers.Add(new Trainer
            {
                Id = i + 1,
                PassportNumber = $"4001 {200000 + i:D6}",
                LastName = d.Last,
                FirstName = d.First,
                MiddleName = d.Middle,
                Gender = d.Gender,
                DateOfBirth = new DateOnly(1975 + i, 1 + i % 12, 1 + i % 28),
                PhoneNumber = $"+7 900 {200 + i:D3}-{10 + i:D2}-{30 + i:D2}",
                ExperienceYears = d.Experience,
                SpecializationId = spec.Id,
                Specialization = spec
            });
        }
    }

    private static void SeedTrainingSessions(FitnessClubContext context)
    {
        var data = new (int Hall, int Trainer, int Client, DateTime When, bool IsTrial)[]
        {
            // Зал 1 (Йога)
            (1, 10, 1,  new(2025, 6, 5, 10, 0, 0),  false),
            (1, 10, 2,  new(2025, 6, 10, 10, 0, 0), true),
            (1, 9,  3,  new(2025, 6, 12, 15, 0, 0), false),
            (1, 10, 4,  new(2025, 6, 20, 10, 0, 0), false),

            // Зал 2 (Кроссфит)
            (2, 10, 5,  new(2025, 6, 6, 11, 0, 0),  false),
            (2, 9,  6,  new(2025, 6, 8, 12, 0, 0),  false),
            (2, 9,  7,  new(2025, 6, 11, 14, 0, 0), true),

            // Зал 3 (Кардио)
            (3, 9,  8,  new(2025, 6, 13, 9, 0, 0),  false),
            (3, 8,  9,  new(2025, 6, 14, 16, 0, 0), false),
            (3, 8,  10, new(2025, 6, 18, 11, 0, 0), false),

            // Зал 4 (Силовой)
            (4, 8,  11, new(2025, 6, 7, 13, 0, 0),  false),
            (4, 8,  12, new(2025, 6, 19, 10, 0, 0), false),

            // Зал 5 (Единоборства)
            (5, 7,  13, new(2025, 6, 9, 15, 0, 0),  false),
            (5, 7,  14, new(2025, 6, 16, 10, 0, 0), true),
            (5, 7,  15, new(2025, 6, 22, 11, 0, 0), false),

            // Зал 6 (Пилатес)
            (6, 6,  1,  new(2025, 6, 3, 9, 0, 0),   false),
            (6, 6,  2,  new(2025, 6, 25, 10, 0, 0), false),

            // Зал 7 (Аэробика)
            (7, 5,  3,  new(2025, 6, 4, 14, 0, 0),  false),
            (7, 9,  4,  new(2025, 5, 25, 10, 0, 0), false),

            // Зал 8 (Стретчинг)
            (8, 4,  5,  new(2025, 6, 15, 18, 0, 0), false),

            // Зал 9 (Реабилитация)
            (9, 10, 6,  new(2025, 6, 28, 10, 0, 0), false),

            // Зал 10 (Универсальный)
            (10, 10, 7, new(2025, 7, 2, 10, 0, 0),  false)
        };

        for (var i = 0; i < data.Length; i++)
        {
            var d = data[i];
            var client = context.Clients.First(c => c.Id == d.Client);
            var trainer = context.Trainers.First(t => t.Id == d.Trainer);
            var hall = context.GymHalls.First(h => h.Id == d.Hall);

            context.TrainingSessions.Add(new TrainingSession
            {
                Id = i + 1,
                ClientId = client.Id,
                Client = client,
                TrainerId = trainer.Id,
                Trainer = trainer,
                GymHallId = hall.Id,
                GymHall = hall,
                DateTime = d.When,
                Duration = TimeSpan.FromHours(1),
                IsTrial = d.IsTrial
            });
        }
    }
}