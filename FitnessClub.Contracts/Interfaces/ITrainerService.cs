using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Интерфейс сервиса тренеров
/// </summary>
public interface ITrainerService
{
    /// <summary>
    /// Возвращает список всех тренеров
    /// </summary>
    public Task<List<TrainerDto>> GetAllAsync();

    /// <summary>
    /// Возвращает тренера по его Id
    /// </summary>
    public Task<TrainerDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создает нового тренера
    /// </summary>
    public Task<TrainerDto?> CreateAsync(TrainerEditDto dto);

    /// <summary>
    /// Обновляет данные существующего тренера
    /// </summary>
    public Task<bool> UpdateAsync(int id, TrainerEditDto dto);

    /// <summary>
    /// Удаляет тренера по его id
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}