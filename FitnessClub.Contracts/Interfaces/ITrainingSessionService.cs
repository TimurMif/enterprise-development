using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

public interface ITrainingSessionService
{
    /// <summary>
    /// Возвращает список всех тренировочных сессий
    /// </summary>
    Task<List<TrainingSessionDto>> GetAllAsync();

    /// <summary>
    /// Возвращает тренировку по ее Id
    /// </summary>
    Task<TrainingSessionDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создает новую тренировочную сессию
    /// </summary>
    Task<TrainingSessionDto?> CreateAsync(TrainingSessionEditDto dto);

    /// <summary>
    /// Обновляет данные существующей тренировки
    /// </summary>
    Task<bool> UpdateAsync(int id, TrainingSessionEditDto dto);

    /// <summary>
    /// Удаляет тренировочную сессию по ее Id
    /// </summary>
    Task<bool> DeleteAsync(int id);
}