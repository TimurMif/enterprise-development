using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;
using FitnessClub.Application.Mapping;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис для работы с тренерами фитнес-клуба
/// </summary>
public class TrainerService : ITrainerService
{
    private readonly IRepository<Trainer> _trainerRepository;
    private readonly IRepository<Specialization> _specializationRepository;

    /// <summary>
    /// Инициализирует новый экземпляр сервиса тренеров
    /// </summary>
    public TrainerService(
        IRepository<Trainer> trainerRepository,
        IRepository<Specialization> specializationRepository)
    {
        _trainerRepository = trainerRepository;
        _specializationRepository = specializationRepository;
    }

    /// <summary>
    /// Возвращает список всех тренеров фитнес-клуба
    /// </summary>
    public async Task<List<TrainerDto>> GetAllAsync()
    {
        var trainers = await _trainerRepository.GetAllAsync();
        return trainers.Select(t => t.ToDto()).ToList();
    }

    /// <summary>
    /// Возвращает информацию о тренере по его Id
    /// </summary>
    public async Task<TrainerDto?> GetByIdAsync(int id)
    {
        var trainer = await _trainerRepository.GetByIdAsync(id);
        return trainer == null ? null : trainer.ToDto();
    }

    /// <summary>
    /// Создает новую запись тренера на основе переданных данных
    /// </summary>
    public async Task<TrainerDto?> CreateAsync(TrainerEditDto dto)
    {
        var spec = await _specializationRepository.GetByIdAsync(dto.SpecializationId);
        if (spec == null) return null;

        var created = await _trainerRepository.AddAsync(MapToEntity(dto, spec));
        return created.ToDto();
    }

    /// <summary>
    /// Обновляет данные существующего тренера по его Id
    /// </summary>
    public async Task<bool> UpdateAsync(int id, TrainerEditDto updatedDto)
    {
        var spec = await _specializationRepository.GetByIdAsync(updatedDto.SpecializationId);
        if (spec == null) return false;

        var trainer = MapToEntity(updatedDto, spec);
        trainer.Id = id;

        return await _trainerRepository.UpdateAsync(trainer);
    }

    /// <summary>
    /// Удаляет тренера по его Id вместе с сопутствующими сессиями
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        return _trainerRepository.DeleteAsync(id);
    }

    /// <summary>
    /// Преобразует DTO в доменную модель тренера
    /// </summary>
    private static Trainer MapToEntity(TrainerEditDto dto, Specialization spec)
    {
        return new Trainer
        {
            PassportNumber = dto.PassportNumber,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MiddleName = dto.MiddleName,
            Gender = dto.Gender,
            DateOfBirth = dto.DateOfBirth,
            PhoneNumber = dto.PhoneNumber,
            ExperienceYears = dto.ExperienceYears,
            SpecializationId = spec.Id,
            Specialization = spec
        };
    }
}