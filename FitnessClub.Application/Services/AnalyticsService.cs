using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис аналитических запросов по данным фитнес-клуба
/// </summary>
public class AnalyticsService(IRepository<Trainer> trainerRepository,
    IRepository<Client> clientRepository,
    IRepository<TrainingSession> sessionRepository) : IAnalyticsService
{
    /// <summary>
    /// Возвращает список тренеров со стажем не менее 5 лет
    /// </summary>
    public async Task<List<TrainerDto>> GetTrainersWithAtLeast5YearsExperienceAsync()
    {
        var trainers = await trainerRepository.GetAllAsync();

        var result = trainers
            .Where(t => t.ExperienceYears >= 5)
            .Select(t => t.ToDto())
            .ToList();
        return result;
    }

    /// <summary>
    /// Проверяет доступность указанного зала в заданное время
    /// </summary>
    public async Task<bool> IsHallAvailableAsync(int hallId, DateTime checkTime)
    {
        var sessions = await sessionRepository.GetAllAsync();

        var isOccupied = sessions.Any(s =>
            s.GymHallId == hallId &&
            s.DateTime <= checkTime &&
            s.DateTime.Add(s.Duration) > checkTime);

        return !isOccupied;
    }

    /// <summary>
    /// Возвращает список клиентов с просроченными абонементами на указанную дату
    /// </summary>
    public async Task<List<ClientDto>> GetClientsWithExpiredSubscriptionAsync(DateOnly referenceDate)
    {
        var clients = await clientRepository.GetAllAsync();

        var result = clients
            .Where(c => c.SubscriptionEndDate < referenceDate)
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ThenBy(c => c.MiddleName)
            .Select(c => c.ToDto())
            .ToList();

        return result;
    }

    /// <summary>
    /// Возвращает список тренировок в указанном зале за определенный месяц и год
    /// </summary>
    public async Task<List<TrainingSessionDto>> GetSessionsForMonthInHallAsync(int hallId, int year, int month)
    {
        var sessions = await sessionRepository.GetAllAsync();

        var result = sessions
            .Where(s => s.GymHallId == hallId
                        && s.DateTime.Year == year
                        && s.DateTime.Month == month)
            .Select(s => s.ToDto())
            .ToList();

        return result;
    }

    /// <summary>
    /// Возвращает список топ-5 самых популярных тренеров
    /// </summary>
    public async Task<List<TrainerDto>> GetTop5TrainersAsync()
    {
        var sessions = await sessionRepository.GetAllAsync();
        var allTrainers = await trainerRepository.GetAllAsync();

        var topTrainerIds = sessions
            .GroupBy(s => s.TrainerId)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .ToList();

        var trainers = topTrainerIds
            .Select(id => allTrainers.FirstOrDefault(t => t.Id == id))
            .OfType<Trainer>()
            .Take(5)
            .Select(t => t.ToDto())
            .ToList();

        return trainers;
    }
}