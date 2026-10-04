using FitnessClub.Domain.Models;

namespace FitnessClub.Server.Services;

/// <summary>
/// Интерфейс сервиса аналитических запросов
/// </summary>
public interface IAnalyticsService
{
    List<Trainer> GetTrainersWithAtLeast5YearsExperience();
    bool IsHallAvailable(int hallId, DateTime checkTime);
    List<Client> GetClientsWithExpiredSubscription(DateOnly referenceDate);
    List<TrainingSession> GetSessionsForMonthInHall(int hallId, int year, int month);
    List<Trainer> GetTop5Trainers();
}