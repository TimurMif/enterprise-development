using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Интерфейс сервиса специализаций
/// </summary>
public interface ISpecializationService
{
    /// <summary>
    /// Возвращает список всех специализаций
    /// </summary>
    public Task<List<SpecializationDto>> GetAllAsync();

    /// <summary>
    /// Возвращает специализацию по ее Id
    /// </summary>
    public Task<SpecializationDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создает новую специализацию
    /// </summary>
    public Task<SpecializationDto> CreateAsync(SpecializationEditDto dto);

    /// <summary>
    /// Обновляет данные существующей специализации
    /// </summary>
    public Task<bool> UpdateAsync(int id, SpecializationEditDto dto);

    /// <summary>
    /// Удаляет специализацию по ее Id
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}