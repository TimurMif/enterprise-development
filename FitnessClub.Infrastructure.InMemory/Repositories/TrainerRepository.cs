using FitnessClub.Domain.Context;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

public class TrainerRepository : IRepository<Trainer>
{
    private readonly FitnessClubContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр репозитория тренеров
    /// </summary>
    public TrainerRepository(FitnessClubContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Возвращает список всех тренеров
    /// </summary>
    public Task<List<Trainer>> GetAllAsync()
    {
        return Task.FromResult(_context.Trainers);
    }

    /// <summary>
    /// Возвращает тренера по его Id
    /// </summary>
    public Task<Trainer?> GetByIdAsync(int id)
    {
        return Task.FromResult(_context.Trainers.FirstOrDefault(t => t.Id == id));
    }

    /// <summary>
    /// Добавляет нового тренера
    /// </summary>
    public Task AddAsync(Trainer entity)
    {
        _context.Trainers.Add(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Обновляет данные существующего тренера
    /// </summary>
    public Task<bool> Update(Trainer entity)
    {
        var existing = _context.Trainers.FirstOrDefault(t => t.Id == entity.Id);
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
    public Task<bool> Delete(int id)
    {
        var entity = _context.Trainers.FirstOrDefault(t => t.Id == id);
        if (entity == null) return Task.FromResult(false);

        _context.TrainingSessions.RemoveAll(s => s.TrainerId == id);
        _context.Trainers.Remove(entity);
        
        return Task.FromResult(true);
    }
}