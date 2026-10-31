using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;
using FitnessClub.Application.Mapping;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис для работы с тренерами фитнес-клуба
/// </summary>
public class TrainerService(IRepository<Trainer> trainerRepository, IRepository<Specialization> specializationRepository) : ITrainerService
{
    /// <summary>
    /// Возвращает список всех тренеров фитнес-клуба
    /// </summary>
    public async Task<List<TrainerDto>> GetAllAsync()
    {
        var trainers = await trainerRepository.GetAllAsync();
        return trainers.Select(t => t.ToDto()).ToList();
    }

    /// <summary>
    /// Возвращает информацию о тренере по его Id
    /// </summary>
    public async Task<TrainerDto?> GetByIdAsync(int id)
    {
        var trainer = await trainerRepository.GetByIdAsync(id);
        return trainer?.ToDto();
    }

    /// <summary>
    /// Создает новую запись тренера на основе переданных данных
    /// </summary>
    public async Task<TrainerDto?> CreateAsync(TrainerEditDto dto)
    {
        var spec = await specializationRepository.GetByIdAsync(dto.SpecializationId);
        if (spec == null) return null;

        var created = await trainerRepository.AddAsync(dto.ToEntity(spec));
        return created.ToDto();
    }

    /// <summary>
    /// Обновляет данные существующего тренера по его Id
    /// </summary>
    public async Task<bool> UpdateAsync(int id, TrainerEditDto updatedDto)
    {
        var spec = await specializationRepository.GetByIdAsync(updatedDto.SpecializationId);
        if (spec == null) return false;

        var trainer = updatedDto.ToEntity(spec);
        trainer.Id = id;

        return await trainerRepository.UpdateAsync(trainer);
    }

    /// <summary>
    /// Удаляет тренера по его Id вместе с сопутствующими сессиями
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        return trainerRepository.DeleteAsync(id);
    }
}