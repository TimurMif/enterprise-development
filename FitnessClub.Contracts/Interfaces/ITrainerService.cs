using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

public interface ITrainerService
{
    /// <summary>
    /// Возвращает список всех тренеров
    /// </summary>
    Task<List<TrainerDto>> GetAllAsync();

    /// <summary>
    /// Возвращает тренера по его Id
    /// </summary>
    Task<TrainerDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создает нового тренера
    /// </summary>
    Task<TrainerDto?> CreateAsync(TrainerEditDto dto);

    /// <summary>
    /// Обновляет данные существующего тренера
    /// </summary>
    Task<bool> UpdateAsync(int id, TrainerEditDto dto);

    /// <summary>
    /// Удаляет тренера по его id
    /// </summary>
    Task<bool> DeleteAsync(int id);
}