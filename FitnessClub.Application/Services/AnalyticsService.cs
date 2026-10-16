using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Context;
using FitnessClub.Domain.Models;

namespace FitnessClub.Application.Services;

public class AnalyticsService : IAnalyticsService
{
    private readonly FitnessClubContext _context;

    public AnalyticsService(FitnessClubContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Возвращает список тренеров со стажем не менее 5 лет
    /// </summary>
    public Task<List<TrainerDto>> GetTrainersWithAtLeast5YearsExperienceAsync()
    {
        var result = _context.Trainers
            .Where(t => t.ExperienceYears >= 5)
            .Select(MapToTrainerDto)
            .ToList();
        return Task.FromResult(result);
    }

    /// <summary>
    /// Проверяет доступность указанного зала в заданное время
    /// </summary>
    public Task<bool> IsHallAvailableAsync(int hallId, DateTime checkTime)
    {
        var isOccupied = _context.TrainingSessions.Any(s =>
            s.GymHallId == hallId &&
            s.DateTime <= checkTime &&
            s.DateTime.Add(s.Duration) > checkTime);

        return Task.FromResult(!isOccupied);
    }

    /// <summary>
    /// Возвращает список клиентов с просроченными абонементами на указанную дату
    /// </summary>
    public Task<List<ClientDto>> GetClientsWithExpiredSubscriptionAsync(DateOnly referenceDate)
    {
        var result = _context.Clients
            .Where(c => c.SubscriptionEndDate < referenceDate)
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ThenBy(c => c.MiddleName)
            .Select(MapToClientDto)
            .ToList();

        return Task.FromResult(result);
    }

    /// <summary>
    /// Возвращает список тренировок в указанном зале за определенное время
    /// </summary>
    public Task<List<TrainingSessionDto>> GetSessionsForMonthInHallAsync(int hallId, int year, int month)
    {
        var result = _context.TrainingSessions
            .Where(s => s.GymHallId == hallId
                        && s.DateTime.Year == year
                        && s.DateTime.Month == month)
            .Select(MapToSessionDto)
            .ToList();

        return Task.FromResult(result);
    }

    /// <summary>
    /// Возвращает список топ-5 самых популярных тренеров
    /// </summary>
    public Task<List<TrainerDto>> GetTop5TrainersAsync()
    {
        var topTrainerIds = _context.TrainingSessions
            .GroupBy(s => s.TrainerId)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .ToList();

        var trainers = topTrainerIds
            .Select(id => _context.Trainers.FirstOrDefault(t => t.Id == id))
            .OfType<Trainer>()
            .Take(5)
            .Select(MapToTrainerDto)
            .ToList();

        return Task.FromResult(trainers);
    }

    /// <summary>
    /// Преобразует доменную модель тренера в объект передачи данных DTO
    /// </summary>
    private static TrainerDto MapToTrainerDto(Trainer t)
    {
        return new TrainerDto
        {
            Id = t.Id,
            PassportNumber = t.PassportNumber,
            FirstName = t.FirstName,
            LastName = t.LastName,
            MiddleName = t.MiddleName,
            Gender = t.Gender,
            DateOfBirth = t.DateOfBirth,
            PhoneNumber = t.PhoneNumber,
            ExperienceYears = t.ExperienceYears,
            SpecializationId = t.SpecializationId,
            Specialization = new SpecializationDto { Id = t.Specialization.Id, Name = t.Specialization.Name }
        };
    }

    /// <summary>
    /// Преобразует доменную модель клиента в объект передачи данных DTO
    /// </summary>
    private static ClientDto MapToClientDto(Client c)
    {
        return new ClientDto
        {
            Id = c.Id,
            PassportNumber = c.PassportNumber,
            FirstName = c.FirstName,
            LastName = c.LastName,
            MiddleName = c.MiddleName,
            Gender = c.Gender,
            DateOfBirth = c.DateOfBirth,
            PhoneNumber = c.PhoneNumber,
            SubscriptionStartDate = c.SubscriptionStartDate,
            SubscriptionEndDate = c.SubscriptionEndDate
        };
    }

    /// <summary>
    /// Преобразует доменную модель тренировки в объект передачи данных DTO
    /// </summary>
    private static TrainingSessionDto MapToSessionDto(TrainingSession s)
    {
        return new TrainingSessionDto
        {
            Id = s.Id,
            ClientId = s.ClientId,
            Client = MapToClientDto(s.Client),
            TrainerId = s.TrainerId,
            Trainer = MapToTrainerDto(s.Trainer),
            GymHallId = s.GymHallId,
            GymHall = new GymHallDto { Id = s.GymHall.Id, Name = s.GymHall.Name, Capacity = s.GymHall.Capacity },
            DateTime = s.DateTime,
            Duration = s.Duration,
            IsTrial = s.IsTrial
        };
    }
}