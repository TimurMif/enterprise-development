using FitnessClub.Domain.Context;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

public class TrainingSessionRepository : IRepository<TrainingSession>
{
    private readonly FitnessClubContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр репозитория тренировочных сессий
    /// </summary>
    public TrainingSessionRepository(FitnessClubContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Возвращает список всех тренировочных сессий
    /// </summary>
    public Task<List<TrainingSession>> GetAllAsync()
    {
        return Task.FromResult(_context.TrainingSessions);
    }

    /// <summary>
    /// Возвращает тренировку по ее Id
    /// </summary>
    public Task<TrainingSession?> GetByIdAsync(int id)
    {
        return Task.FromResult(_context.TrainingSessions.FirstOrDefault(s => s.Id == id));
    }

    /// <summary>
    /// Добавляет новую тренировочную сессию
    /// </summary>
    public Task AddAsync(TrainingSession entity)
    {
        _context.TrainingSessions.Add(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Обновляет данные существующей тренировки
    /// </summary>
    public Task<bool> Update(TrainingSession entity)
    {
        var existing = _context.TrainingSessions.FirstOrDefault(s => s.Id == entity.Id);
        if (existing == null) return Task.FromResult(false);

        existing.ClientId = entity.ClientId;
        existing.Client = entity.Client;
        existing.TrainerId = entity.TrainerId;
        existing.Trainer = entity.Trainer;
        existing.GymHallId = entity.GymHallId;
        existing.GymHall = entity.GymHall;
        existing.DateTime = entity.DateTime;
        existing.Duration = entity.Duration;
        existing.IsTrial = entity.IsTrial;

        return Task.FromResult(true);
    }

    /// <summary>
    /// Удаляет тренировочную сессию по ее Id
    /// </summary>
    public Task<bool> Delete(int id)
    {
        var entity = _context.TrainingSessions.FirstOrDefault(s => s.Id == id);
        if (entity == null) return Task.FromResult(false);

        _context.TrainingSessions.Remove(entity);
        
        return Task.FromResult(true);
    }
}