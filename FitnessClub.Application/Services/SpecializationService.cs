using FitnessClub.Application.Mapping;
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
    private readonly IRepository<Trainer> _trainerRepository;

    /// <summary>
    /// Инициализирует новый экземпляр сервиса специализаций
    /// </summary>
    public SpecializationService(
        IRepository<Specialization> specializationRepository,
        IRepository<Trainer> trainerRepository)
    {
        _specializationRepository = specializationRepository;
        _trainerRepository = trainerRepository;
    }

    /// <summary>
    /// Возвращает список всех специализаций
    /// </summary>
    public async Task<List<SpecializationDto>> GetAllAsync()
    {
        var specializations = await _specializationRepository.GetAllAsync();
        return specializations.Select(s => s.ToDto()).ToList();
    }

    /// <summary>
    /// Возвращает информацию о специализации по ее Id
    /// </summary>
    public async Task<SpecializationDto?> GetByIdAsync(int id)
    {
        var specialization = await _specializationRepository.GetByIdAsync(id);
        return specialization == null ? null : specialization.ToDto();
    }

    /// <summary>
    /// Создает новую запись специализации на основе переданных данных
    /// </summary>
    public async Task<SpecializationDto> CreateAsync(SpecializationEditDto dto)
    {
        var created = await _specializationRepository.AddAsync(MapToEntity(dto));
        return created.ToDto();
    }

    /// <summary>
    /// Обновляет данные существующей специализации по ее Id
    /// </summary>
    public async Task<bool> UpdateAsync(int id, SpecializationEditDto updatedDto)
    {
        var specialization = MapToEntity(updatedDto);
        specialization.Id = id;

        return await _specializationRepository.UpdateAsync(specialization);
    }

    /// <summary>
    /// Удаляет специализацию по ее Id. Если специализация назначена тренерам, выбрасывает исключение
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var trainers = await _trainerRepository.GetAllAsync();
        if (trainers.Any(t => t.SpecializationId == id))
            throw new InvalidOperationException("Нельзя удалить специализацию, которая назначена тренерам");

        return await _specializationRepository.DeleteAsync(id);
    }

    /// <summary>
    /// Преобразует DTO в доменную модель специализации
    /// </summary>
    private static Specialization MapToEntity(SpecializationEditDto dto)
    {
        return new Specialization
        {
            Name = dto.Name
        };
    }
}