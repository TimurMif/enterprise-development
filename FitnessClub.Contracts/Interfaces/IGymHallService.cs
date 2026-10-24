using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

public interface IGymHallService
{
    /// <summary>
    /// Возвращает список всех залов
    /// </summary>
    Task<List<GymHallDto>> GetAllAsync();

    /// <summary>
    /// Возвращает зал по его Id
    /// </summary>
    Task<GymHallDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создает новый зал
    /// </summary>
    Task<GymHallDto> CreateAsync(GymHallEditDto dto);

    /// <summary>
    /// Обновляет данные существующего зала
    /// </summary>
    Task<bool> UpdateAsync(int id, GymHallEditDto dto);

    /// <summary>
    /// Удаляет зал по его Id вместе с сопутствующими сессиями
    /// </summary>
    Task<bool> DeleteAsync(int id);
}