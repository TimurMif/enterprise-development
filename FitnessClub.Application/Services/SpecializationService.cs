using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис для работы со справочником специализаций
/// </summary>
public class SpecializationService : ISpecializationService
{
    private readonly IRepository<Specialization> _specializationRepository;

    /// <summary>
    /// Инициализирует новый экземпляр сервиса специализаций
    /// </summary>
    public SpecializationService(IRepository<Specialization> specializationRepository)
    {
        _specializationRepository = specializationRepository;
    }

    /// <summary>
    /// Возвращает список всех специализаций
    /// </summary>
    public async Task<List<SpecializationDto>> GetAllAsync()
    {
        var specializations = await _specializationRepository.GetAllAsync();
        return specializations.Select(MapToDto).ToList();
    }

    /// <summary>
    /// Возвращает информацию о специализации по ее Id
    /// </summary>
    public async Task<SpecializationDto?> GetByIdAsync(int id)
    {
        var specialization = await _specializationRepository.GetByIdAsync(id);
        return specialization == null ? null : MapToDto(specialization);
    }

    /// <summary>
    /// Преобразует доменную модель специализации в объект DTO
    /// </summary>
    private static SpecializationDto MapToDto(Specialization s)
    {
        return new SpecializationDto
        {
            Id = s.Id,
            Name = s.Name
        };
    }
}