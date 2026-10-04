using FitnessClub.Domain.Context;
using FitnessClub.Domain.Models;

namespace FitnessClub.Server.Services;

/// <summary>
/// Сервис для аналитических запросов
/// </summary>
public class AnalyticsService : IAnalyticsService
{
    private readonly FitnessClubContext _context;

    public AnalyticsService(FitnessClubContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Выводит информацию о тренерах со стажем работы не менее 5 лет
    /// </summary>
    public List<Trainer> GetTrainersWithAtLeast5YearsExperience()
    {
        return _context.Trainers
            .Where(t => t.ExperienceYears >= 5)
            .ToList();
    }

    /// <summary>
    /// Проверяет, свободен ли выбранный зал в определенный момент
    /// </summary>
    public bool IsHallAvailable(int hallId, DateTime checkTime)
    {
        var isOccupied = _context.TrainingSessions.Any(s =>
            s.GymHallId == hallId &&
            s.DateTime <= checkTime &&
            s.DateTime.Add(s.Duration) > checkTime);

        return !isOccupied;
    }

    /// <summary>
    /// Выводит информацию о клиентах с просроченным абонементом с сортировкой по ФИО
    /// </summary>
    public List<Client> GetClientsWithExpiredSubscription(DateOnly referenceDate)
    {
        return _context.Clients
            .Where(c => c.SubscriptionEndDate < referenceDate)
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ThenBy(c => c.MiddleName)
            .ToList();
    }

    /// <summary>
    /// Выводит информацию о занятиях за указанный месяц в выбранном зале.
    /// </summary>
    public List<TrainingSession> GetSessionsForMonthInHall(int hallId, int year, int month)
    {
        return _context.TrainingSessions
            .Where(s => s.GymHallId == hallId
                        && s.DateTime.Year == year
                        && s.DateTime.Month == month)
            .ToList();
    }

    /// <summary>
    /// Выводит топ-5 популярных тренеров
    /// </summary>
    public List<Trainer> GetTop5Trainers()
    {
        var topTrainerIds = _context.TrainingSessions
            .GroupBy(s => s.TrainerId)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .ToList();

        return topTrainerIds
            .Select(id => _context.Trainers.FirstOrDefault(t => t.Id == id))
            .OfType<Trainer>()
            .Take(5)
            .ToList();
    }
}