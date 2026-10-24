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
}