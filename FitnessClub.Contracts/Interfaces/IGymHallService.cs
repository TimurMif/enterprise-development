using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Интерфейс для сервиса залов
/// </summary>
public interface IGymHallService
{
    /// <summary>
    /// Возвращает список всех залов
    /// </summary>
    public Task<List<GymHallDto>> GetAllAsync();

    /// <summary>
    /// Возвращает зал по его Id
    /// </summary>
    public Task<GymHallDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создает новый зал
    /// </summary>
    public Task<GymHallDto> CreateAsync(GymHallEditDto dto);

    /// <summary>
    /// Обновляет данные существующего зала
    /// </summary>
    public Task<bool> UpdateAsync(int id, GymHallEditDto dto);

    /// <summary>
    /// Удаляет зал по его Id вместе с сопутствующими сессиями
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}