using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Context;
using FitnessClub.Domain.Models;

namespace FitnessClub.Application.Services;

public class TrainerService : ITrainerService
{
    private readonly FitnessClubContext _context;

    public TrainerService(FitnessClubContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Возвращает список всех тренеров фитнес-клуба
    /// </summary>
    public Task<List<TrainerDto>> GetAllAsync()
    {
        var dtos = _context.Trainers.Select(MapToDto).ToList();
        return Task.FromResult(dtos);
    }

    /// <summary>
    /// Возвращает информацию о тренере по его Id
    /// </summary>
    public Task<TrainerDto?> GetByIdAsync(int id)
    {
        var trainer = _context.Trainers.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(trainer == null ? null : MapToDto(trainer));
    }

    /// <summary>
    /// Создает новую запись тренера на основе переданных данных
    /// </summary>
    public Task<TrainerDto?> CreateAsync(TrainerEditDto dto)
    {
        var spec = _context.Specializations.FirstOrDefault(s => s.Id == dto.SpecializationId);
        if (spec == null) return Task.FromResult<TrainerDto?>(null);

        var newId = _context.Trainers.Any() ? _context.Trainers.Max(t => t.Id) + 1 : 1;
        var trainer = new Trainer
        {
            Id = newId,
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

        _context.Trainers.Add(trainer);
        return Task.FromResult<TrainerDto?>(MapToDto(trainer));
    }

    /// <summary>
    /// Обновляет данные существующего тренера по его Id
    /// </summary>
    public Task<bool> UpdateAsync(int id, TrainerEditDto updatedDto)
    {
        var trainer = _context.Trainers.FirstOrDefault(t => t.Id == id);
        if (trainer == null) return Task.FromResult(false);

        var spec = _context.Specializations.FirstOrDefault(s => s.Id == updatedDto.SpecializationId);
        if (spec == null) return Task.FromResult(false);

        trainer.PassportNumber = updatedDto.PassportNumber;
        trainer.FirstName = updatedDto.FirstName;
        trainer.LastName = updatedDto.LastName;
        trainer.MiddleName = updatedDto.MiddleName;
        trainer.Gender = updatedDto.Gender;
        trainer.DateOfBirth = updatedDto.DateOfBirth;
        trainer.PhoneNumber = updatedDto.PhoneNumber;
        trainer.ExperienceYears = updatedDto.ExperienceYears;
        trainer.SpecializationId = updatedDto.SpecializationId;
        trainer.Specialization = spec;

        return Task.FromResult(true);
    }

    /// <summary>
    /// Удаляет тренера по его Id вместе с сопутствующими сессиями
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var trainer = _context.Trainers.FirstOrDefault(t => t.Id == id);
        if (trainer == null) return Task.FromResult(false);

        _context.TrainingSessions.RemoveAll(s => s.TrainerId == id);
        _context.Trainers.Remove(trainer);
        return Task.FromResult(true);
    }

    /// <summary>
    /// Преобразует доменную модель тренера в объект передачи данных DTO
    /// </summary>
    private static TrainerDto MapToDto(Trainer t)
    {
        return new TrainerDto
        {
            Id = t.Id,
            PassportNumber = t.PassportNumber,
            FirstName = t.FirstName,
            LastName = t.LastName,
            MiddleName = t.MiddleName,
            Gender = t.Gender,
            DateOfBirth = t.DateOfBirth,
            PhoneNumber = t.PhoneNumber,
            ExperienceYears = t.ExperienceYears,
            SpecializationId = t.SpecializationId,
            Specialization = new SpecializationDto
            {
                Id = t.Specialization.Id,
                Name = t.Specialization.Name
            }
        };
    }
}