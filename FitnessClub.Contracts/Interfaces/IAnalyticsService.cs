using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Интерфейс сервиса для аналитических запросов
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Возвращает список тренеров со стажем не менее 5 лет
    /// </summary>
    public Task<List<TrainerDto>> GetTrainersWithAtLeast5YearsExperienceAsync();

    /// <summary>
    /// Проверяет доступность указанного зала в заданное время
    /// </summary>
    public Task<bool> IsHallAvailableAsync(int hallId, DateTime checkTime);

    /// <summary>
    /// Возвращает список клиентов с просроченными абонементами на указанную дату
    /// </summary>
    public Task<List<ClientDto>> GetClientsWithExpiredSubscriptionAsync(DateOnly referenceDate);

    /// <summary>
    /// Возвращает список тренировок в указанном зале за определенный месяц и год
    /// </summary>
    public Task<List<TrainingSessionDto>> GetSessionsForMonthInHallAsync(int hallId, int year, int month);

    /// <summary>
    /// Возвращает список топ-5 популярных тренеров
    /// </summary>
    public Task<List<TrainerDto>> GetTop5TrainersAsync();
}