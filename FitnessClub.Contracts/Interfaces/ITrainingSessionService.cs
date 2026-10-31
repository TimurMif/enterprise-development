using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Интерфейс для сервиса тренировок
/// </summary>
public interface ITrainingSessionService
{
    /// <summary>
    /// Возвращает список всех тренировочных сессий
    /// </summary>
    public Task<List<TrainingSessionDto>> GetAllAsync();

    /// <summary>
    /// Возвращает тренировку по ее Id
    /// </summary>
    public Task<TrainingSessionDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создает новую тренировочную сессию
    /// </summary>
    public Task<TrainingSessionDto?> CreateAsync(TrainingSessionEditDto dto);

    /// <summary>
    /// Обновляет данные существующей тренировки
    /// </summary>
    public Task<bool> UpdateAsync(int id, TrainingSessionEditDto dto);

    /// <summary>
    /// Удаляет тренировочную сессию по ее Id
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}