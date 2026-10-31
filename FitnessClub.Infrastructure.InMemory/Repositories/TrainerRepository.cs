using FitnessClub.Domain.Context;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

public class TrainerRepository(FitnessClubContext context) : IRepository<Trainer>
{
    /// <summary>
    /// Возвращает список всех тренеров
    /// </summary>
    public Task<List<Trainer>> GetAllAsync()
    {
        return Task.FromResult(context.Trainers.ToList());
    }

    /// <summary>
    /// Возвращает тренера по его Id
    /// </summary>
    public Task<Trainer?> GetByIdAsync(int id)
    {
        return Task.FromResult(context.Trainers.FirstOrDefault(t => t.Id == id));
    }

    /// <summary>
    /// Добавляет нового тренера
    /// </summary>
    public Task<Trainer> AddAsync(Trainer entity)
    {
        entity.Id = context.Trainers.Select(c => c.Id).DefaultIfEmpty(0).Max() + 1;
        context.Trainers.Add(entity);

        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновляет данные существующего тренера
    /// </summary>
    public Task<bool> UpdateAsync(Trainer entity)
    {
        var existing = context.Trainers.FirstOrDefault(t => t.Id == entity.Id);
        if (existing == null) return Task.FromResult(false);

        existing.PassportNumber = entity.PassportNumber;
        existing.FirstName = entity.FirstName;
        existing.LastName = entity.LastName;
        existing.MiddleName = entity.MiddleName;
        existing.Gender = entity.Gender;
        existing.DateOfBirth = entity.DateOfBirth;
        existing.PhoneNumber = entity.PhoneNumber;
        existing.ExperienceYears = entity.ExperienceYears;
        existing.SpecializationId = entity.SpecializationId;
        existing.Specialization = entity.Specialization;

        return Task.FromResult(true);
    }

    /// <summary>
    /// Удаляет тренера по его Id
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var entity = context.Trainers.FirstOrDefault(t => t.Id == id);
        if (entity == null) return Task.FromResult(false);

        context.TrainingSessions.RemoveAll(s => s.TrainerId == id);
        context.Trainers.Remove(entity);
        
        return Task.FromResult(true);
    }
}