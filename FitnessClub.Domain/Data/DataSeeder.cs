using FitnessClub.Domain.Context;
using FitnessClub.Domain.Enums;
using FitnessClub.Domain.Models;

namespace FitnessClub.Domain.Data;

/// <summary>
/// Единый источник тестовых данных для проекта.
/// </summary>
public static class DataSeeder
{
    /// <summary>
    /// Опорная дата, относительно которой построены все данные
    /// </summary>
    public static readonly DateTime ReferenceDate = new(2025, 6, 15, 12, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// Опорная дата для проверки абонементов
    /// </summary>
    public static readonly DateOnly ReferenceToday = DateOnly.FromDateTime(ReferenceDate);

    /// <summary>
    /// Статический набор данных специализаций
    /// </summary>
    public static readonly List<Specialization> Specializations =
    [
        new Specialization { Id = 1,  Name = "Силовой тренинг" },
        new Specialization { Id = 2,  Name = "Кроссфит" },
        new Specialization { Id = 3,  Name = "Пилатес" },
        new Specialization { Id = 4,  Name = "Йога" },
        new Specialization { Id = 5,  Name = "Кардио" },
        new Specialization { Id = 6,  Name = "Единоборства" },
        new Specialization { Id = 7,  Name = "Тяжелая атлетика" },
        new Specialization { Id = 8,  Name = "Стретчинг" },
        new Specialization { Id = 9,  Name = "Реабилитация" },
        new Specialization { Id = 10, Name = "Аэробика" }
    ];

    /// <summary>
    /// Статический набор данных спортивных залов
    /// </summary>
    public static readonly List<GymHall> GymHalls =
    [
        new GymHall { Id = 1,  Name = "Зал Йоги",            Capacity = 15 },
        new GymHall { Id = 2,  Name = "Зал Кроссфита",       Capacity = 30 },
        new GymHall { Id = 3,  Name = "Зал Кардио",          Capacity = 40 },
        new GymHall { Id = 4,  Name = "Зал Силовой",         Capacity = 25 },
        new GymHall { Id = 5,  Name = "Зал Единоборств",     Capacity = 20 },
        new GymHall { Id = 6,  Name = "Зал Пилатеса",        Capacity = 15 },
        new GymHall { Id = 7,  Name = "Зал Аэробики",        Capacity = 35 },
        new GymHall { Id = 8,  Name = "Зал Стретчинга",      Capacity = 12 },
        new GymHall { Id = 9,  Name = "Зал Реабилитации",    Capacity = 10 },
        new GymHall { Id = 10, Name = "Универсальный зал",   Capacity = 50 }
    ];

    /// <summary>
    /// Статический набор данных клиентов.
    /// Первые 5 — с абонементом, истёкшим относительно опорной даты.
    /// Остальные 10 — с действующим.
    /// </summary>
    public static readonly List<Client> Clients =
    [
        new Client { Id = 1,  PassportNumber = "4000 100000", LastName = "Смирнов",   FirstName = "Алексей",   MiddleName = "Иванович",    Gender = Gender.Male,   DateOfBirth = new DateOnly(1985, 1, 1),  PhoneNumber = "+7 900 100-10-20", SubscriptionStartDate = new DateOnly(2024, 6, 1),  SubscriptionEndDate = new DateOnly(2025, 5, 1) },
        new Client { Id = 2,  PassportNumber = "4000 100001", LastName = "Кузнецова", FirstName = "Мария",     MiddleName = "Петровна",    Gender = Gender.Female, DateOfBirth = new DateOnly(1986, 2, 2),  PhoneNumber = "+7 900 101-11-21", SubscriptionStartDate = new DateOnly(2024, 7, 15), SubscriptionEndDate = new DateOnly(2025, 6, 1) },
        new Client { Id = 3,  PassportNumber = "4000 100002", LastName = "Попов",     FirstName = "Дмитрий",   MiddleName = "Сергеевич",   Gender = Gender.Male,   DateOfBirth = new DateOnly(1987, 3, 3),  PhoneNumber = "+7 900 102-12-22", SubscriptionStartDate = new DateOnly(2024, 8, 10), SubscriptionEndDate = new DateOnly(2025, 5, 20) },
        new Client { Id = 4,  PassportNumber = "4000 100003", LastName = "Соколова",  FirstName = "Анна",      MiddleName = "Владимировна", Gender = Gender.Female, DateOfBirth = new DateOnly(1988, 4, 4),  PhoneNumber = "+7 900 103-13-23", SubscriptionStartDate = new DateOnly(2024, 9, 1),  SubscriptionEndDate = new DateOnly(2025, 4, 30) },
        new Client { Id = 5,  PassportNumber = "4000 100004", LastName = "Лебедев",   FirstName = "Никита",    MiddleName = "Андреевич",   Gender = Gender.Male,   DateOfBirth = new DateOnly(1989, 5, 5),  PhoneNumber = "+7 900 104-14-24", SubscriptionStartDate = new DateOnly(2024, 10, 1), SubscriptionEndDate = new DateOnly(2025, 6, 10) },
        new Client { Id = 6,  PassportNumber = "4000 100005", LastName = "Козлов",    FirstName = "Артём",     MiddleName = "Максимович",  Gender = Gender.Male,   DateOfBirth = new DateOnly(1990, 6, 6),  PhoneNumber = "+7 900 105-15-25", SubscriptionStartDate = new DateOnly(2025, 1, 1),  SubscriptionEndDate = new DateOnly(2025, 12, 31) },
        new Client { Id = 7,  PassportNumber = "4000 100006", LastName = "Новикова",  FirstName = "Екатерина", MiddleName = "Дмитриевна",  Gender = Gender.Female, DateOfBirth = new DateOnly(1991, 7, 7),  PhoneNumber = "+7 900 106-16-26", SubscriptionStartDate = new DateOnly(2025, 2, 1),  SubscriptionEndDate = new DateOnly(2026, 2, 1) },
        new Client { Id = 8,  PassportNumber = "4000 100007", LastName = "Морозов",   FirstName = "Егор",      MiddleName = "Алексеевич",  Gender = Gender.Male,   DateOfBirth = new DateOnly(1992, 8, 8),  PhoneNumber = "+7 900 107-17-27", SubscriptionStartDate = new DateOnly(2025, 3, 1),  SubscriptionEndDate = new DateOnly(2026, 3, 1) },
        new Client { Id = 9,  PassportNumber = "4000 100008", LastName = "Петрова",   FirstName = "Ольга",     MiddleName = "Николаевна",  Gender = Gender.Female, DateOfBirth = new DateOnly(1993, 9, 9),  PhoneNumber = "+7 900 108-18-28", SubscriptionStartDate = new DateOnly(2025, 4, 1),  SubscriptionEndDate = new DateOnly(2026, 4, 1) },
        new Client { Id = 10, PassportNumber = "4000 100009", LastName = "Волков",    FirstName = "Илья",      MiddleName = "Романович",   Gender = Gender.Male,   DateOfBirth = new DateOnly(1994, 10, 10), PhoneNumber = "+7 900 109-19-29", SubscriptionStartDate = new DateOnly(2025, 5, 1),  SubscriptionEndDate = new DateOnly(2026, 5, 1) },
        new Client { Id = 11, PassportNumber = "4000 100010", LastName = "Соловьёва", FirstName = "Дарья",     MiddleName = "Кирилловна",  Gender = Gender.Female, DateOfBirth = new DateOnly(1995, 11, 11), PhoneNumber = "+7 900 110-20-30", SubscriptionStartDate = new DateOnly(2024, 12, 1), SubscriptionEndDate = new DateOnly(2025, 12, 1) },
        new Client { Id = 12, PassportNumber = "4000 100011", LastName = "Васильев",  FirstName = "Максим",    MiddleName = "Тимурович",   Gender = Gender.Male,   DateOfBirth = new DateOnly(1996, 12, 12), PhoneNumber = "+7 900 111-21-31", SubscriptionStartDate = new DateOnly(2025, 1, 15), SubscriptionEndDate = new DateOnly(2026, 1, 15) },
        new Client { Id = 13, PassportNumber = "4000 100012", LastName = "Зайцева",   FirstName = "Виктория",  MiddleName = "Андреевна",   Gender = Gender.Female, DateOfBirth = new DateOnly(1997, 1, 13),  PhoneNumber = "+7 900 112-22-32", SubscriptionStartDate = new DateOnly(2025, 2, 15), SubscriptionEndDate = new DateOnly(2026, 2, 15) },
        new Client { Id = 14, PassportNumber = "4000 100013", LastName = "Павлов",    FirstName = "Кирилл",    MiddleName = "Денисович",   Gender = Gender.Male,   DateOfBirth = new DateOnly(1998, 2, 14),  PhoneNumber = "+7 900 113-23-33", SubscriptionStartDate = new DateOnly(2025, 3, 15), SubscriptionEndDate = new DateOnly(2026, 3, 15) },
        new Client { Id = 15, PassportNumber = "4000 100014", LastName = "Семёнова",  FirstName = "Алиса",     MiddleName = "Артёмовна",   Gender = Gender.Female, DateOfBirth = new DateOnly(1999, 3, 15),  PhoneNumber = "+7 900 114-24-34", SubscriptionStartDate = new DateOnly(2025, 4, 15), SubscriptionEndDate = new DateOnly(2026, 4, 15) }
    ];

    /// <summary>
    /// Статический набор данных тренеров.
    /// 7 тренеров со стажем >= 5 лет (позиции 4..10).
    /// </summary>
    public static readonly List<Trainer> Trainers =
    [
        new Trainer { Id = 1,  PassportNumber = "4001 200000", LastName = "Иванов",     FirstName = "Пётр",     MiddleName = "Сергеевич",     Gender = Gender.Male,   DateOfBirth = new DateOnly(1975, 1, 1),  PhoneNumber = "+7 900 200-10-30", ExperienceYears = 1,  SpecializationId = 1 },
        new Trainer { Id = 2,  PassportNumber = "4001 200001", LastName = "Петров",     FirstName = "Иван",     MiddleName = "Алексеевич",    Gender = Gender.Male,   DateOfBirth = new DateOnly(1976, 2, 2),  PhoneNumber = "+7 900 201-11-31", ExperienceYears = 3,  SpecializationId = 2 },
        new Trainer { Id = 3,  PassportNumber = "4001 200002", LastName = "Сидоров",    FirstName = "Николай",  MiddleName = "Михайлович",    Gender = Gender.Male,   DateOfBirth = new DateOnly(1977, 3, 3),  PhoneNumber = "+7 900 202-12-32", ExperienceYears = 4,  SpecializationId = 3 },
        new Trainer { Id = 4,  PassportNumber = "4001 200003", LastName = "Орлова",     FirstName = "Елена",    MiddleName = "Владимировна",  Gender = Gender.Female, DateOfBirth = new DateOnly(1978, 4, 4),  PhoneNumber = "+7 900 203-13-33", ExperienceYears = 5,  SpecializationId = 4 },
        new Trainer { Id = 5,  PassportNumber = "4001 200004", LastName = "Фёдоров",    FirstName = "Андрей",   MiddleName = "Игоревич",      Gender = Gender.Male,   DateOfBirth = new DateOnly(1979, 5, 5),  PhoneNumber = "+7 900 204-14-34", ExperienceYears = 7,  SpecializationId = 5 },
        new Trainer { Id = 6,  PassportNumber = "4001 200005", LastName = "Михайлова",  FirstName = "Татьяна",  MiddleName = "Юрьевна",       Gender = Gender.Female, DateOfBirth = new DateOnly(1980, 6, 6),  PhoneNumber = "+7 900 205-15-35", ExperienceYears = 8,  SpecializationId = 6 },
        new Trainer { Id = 7,  PassportNumber = "4001 200006", LastName = "Белов",      FirstName = "Виктор",   MiddleName = "Станиславович", Gender = Gender.Male,   DateOfBirth = new DateOnly(1981, 7, 7),  PhoneNumber = "+7 900 206-16-36", ExperienceYears = 10, SpecializationId = 7 },
        new Trainer { Id = 8,  PassportNumber = "4001 200007", LastName = "Григорьева", FirstName = "Наталья",  MiddleName = "Олеговна",      Gender = Gender.Female, DateOfBirth = new DateOnly(1982, 8, 8),  PhoneNumber = "+7 900 207-17-37", ExperienceYears = 12, SpecializationId = 8 },
        new Trainer { Id = 9,  PassportNumber = "4001 200008", LastName = "Тихонов",    FirstName = "Роман",    MiddleName = "Валерьевич",    Gender = Gender.Male,   DateOfBirth = new DateOnly(1983, 9, 9),  PhoneNumber = "+7 900 208-18-38", ExperienceYears = 13, SpecializationId = 9 },
        new Trainer { Id = 10, PassportNumber = "4001 200009", LastName = "Егорова",    FirstName = "Ирина",    MiddleName = "Борисовна",     Gender = Gender.Female, DateOfBirth = new DateOnly(1984, 10, 10), PhoneNumber = "+7 900 209-19-39", ExperienceYears = 15, SpecializationId = 10 }
    ];

    /// <summary>
    /// Статический набор данных тренировочных сессий.
    /// </summary>
    public static readonly List<TrainingSession> TrainingSessions =
    [
        // Зал 1 (Йога)
        new TrainingSession { Id = 1,  ClientId = 1,  TrainerId = 10, GymHallId = 1,  DateTime = new DateTime(2025, 6, 5,  10, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = false },
        new TrainingSession { Id = 2,  ClientId = 2,  TrainerId = 10, GymHallId = 1,  DateTime = new DateTime(2025, 6, 10, 10, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = true },
        new TrainingSession { Id = 3,  ClientId = 3,  TrainerId = 9,  GymHallId = 1,  DateTime = new DateTime(2025, 6, 12, 15, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = false },
        new TrainingSession { Id = 4,  ClientId = 4,  TrainerId = 10, GymHallId = 1,  DateTime = new DateTime(2025, 6, 20, 10, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = false },

        // Зал 2 (Кроссфит)
        new TrainingSession { Id = 5,  ClientId = 5,  TrainerId = 10, GymHallId = 2,  DateTime = new DateTime(2025, 6, 6,  11, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = false },
        new TrainingSession { Id = 6,  ClientId = 6,  TrainerId = 9,  GymHallId = 2,  DateTime = new DateTime(2025, 6, 8,  12, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = false },
        new TrainingSession { Id = 7,  ClientId = 7,  TrainerId = 9,  GymHallId = 2,  DateTime = new DateTime(2025, 6, 11, 14, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = true },

        // Зал 3 (Кардио)
        new TrainingSession { Id = 8,  ClientId = 8,  TrainerId = 9,  GymHallId = 3,  DateTime = new DateTime(2025, 6, 13, 9,  0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = false },
        new TrainingSession { Id = 9,  ClientId = 9,  TrainerId = 8,  GymHallId = 3,  DateTime = new DateTime(2025, 6, 14, 16, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = false },
        new TrainingSession { Id = 10, ClientId = 10, TrainerId = 8,  GymHallId = 3,  DateTime = new DateTime(2025, 6, 18, 11, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = false },

        // Зал 4 (Силовой)
        new TrainingSession { Id = 11, ClientId = 11, TrainerId = 8,  GymHallId = 4,  DateTime = new DateTime(2025, 6, 7,  13, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = false },
        new TrainingSession { Id = 12, ClientId = 12, TrainerId = 8,  GymHallId = 4,  DateTime = new DateTime(2025, 6, 19, 10, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = false },

        // Зал 5 (Единоборства)
        new TrainingSession { Id = 13, ClientId = 13, TrainerId = 7,  GymHallId = 5,  DateTime = new DateTime(2025, 6, 9,  15, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = false },
        new TrainingSession { Id = 14, ClientId = 14, TrainerId = 7,  GymHallId = 5,  DateTime = new DateTime(2025, 6, 16, 10, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = true },
        new TrainingSession { Id = 15, ClientId = 15, TrainerId = 7,  GymHallId = 5,  DateTime = new DateTime(2025, 6, 22, 11, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = false },

        // Зал 6 (Пилатес)
        new TrainingSession { Id = 16, ClientId = 1,  TrainerId = 6,  GymHallId = 6,  DateTime = new DateTime(2025, 6, 3,  9,  0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = false },
        new TrainingSession { Id = 17, ClientId = 2,  TrainerId = 6,  GymHallId = 6,  DateTime = new DateTime(2025, 6, 25, 10, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = false },

        // Зал 7 (Аэробика)
        new TrainingSession { Id = 18, ClientId = 3,  TrainerId = 5,  GymHallId = 7,  DateTime = new DateTime(2025, 6, 4,  14, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = false },
        new TrainingSession { Id = 19, ClientId = 4,  TrainerId = 9,  GymHallId = 7,  DateTime = new DateTime(2025, 5, 25, 10, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = false },

        // Зал 8 (Стретчинг)
        new TrainingSession { Id = 20, ClientId = 5,  TrainerId = 4,  GymHallId = 8,  DateTime = new DateTime(2025, 6, 15, 18, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = false },

        // Зал 9 (Реабилитация)
        new TrainingSession { Id = 21, ClientId = 6,  TrainerId = 10, GymHallId = 9,  DateTime = new DateTime(2025, 6, 28, 10, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = false },

        // Зал 10 (Универсальный)
        new TrainingSession { Id = 22, ClientId = 7,  TrainerId = 10, GymHallId = 10, DateTime = new DateTime(2025, 7, 2,  10, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1), IsTrial = false }
    ];


    /// <summary>
    /// Создаёт контекст с заполненными навигационными свойствами.
    /// </summary>
    public static FitnessClubContext Seed()
    {
        var context = new FitnessClubContext();

        context.Specializations.AddRange(Specializations);
        context.GymHalls.AddRange(GymHalls);
        context.Clients.AddRange(Clients);
        context.Trainers.AddRange(Trainers);
        context.TrainingSessions.AddRange(TrainingSessions);
        
        foreach (var trainer in context.Trainers)
        {
            trainer.Specialization = context.Specializations.First(s => s.Id == trainer.SpecializationId);
        }
        
        foreach (var session in context.TrainingSessions)
        {
            session.Client = context.Clients.First(c => c.Id == session.ClientId);
            session.Trainer = context.Trainers.First(t => t.Id == session.TrainerId);
            session.GymHall = context.GymHalls.First(h => h.Id == session.GymHallId);
        }

        return context;
    }
}