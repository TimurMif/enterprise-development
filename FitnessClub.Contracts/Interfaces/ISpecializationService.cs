using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

public interface ISpecializationService
{
    /// <summary>
    /// Возвращает список всех специализаций
    /// </summary>
    Task<List<SpecializationDto>> GetAllAsync();

    /// <summary>
    /// Возвращает специализацию по ее Id
    /// </summary>
    Task<SpecializationDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создает новую специализацию
    /// </summary>
    Task<SpecializationDto> CreateAsync(SpecializationEditDto dto);

    /// <summary>
    /// Обновляет данные существующей специализации
    /// </summary>
    Task<bool> UpdateAsync(int id, SpecializationEditDto dto);

    /// <summary>
    /// Удаляет специализацию по ее Id
    /// </summary>
    Task<bool> DeleteAsync(int id);
}