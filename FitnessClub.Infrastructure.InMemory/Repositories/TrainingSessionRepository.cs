using FitnessClub.Domain.Context;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

public class TrainingSessionRepository(FitnessClubContext context) : IRepository<TrainingSession>
{
    /// <summary>
    /// Возвращает список всех тренировочных сессий
    /// </summary>
    public Task<List<TrainingSession>> GetAllAsync()
    {
        return Task.FromResult(context.TrainingSessions.ToList());
    }

    /// <summary>
    /// Возвращает тренировку по ее Id
    /// </summary>
    public Task<TrainingSession?> GetByIdAsync(int id)
    {
        return Task.FromResult(context.TrainingSessions.FirstOrDefault(s => s.Id == id));
    }

    /// <summary>
    /// Добавляет новую тренировочную сессию
    /// </summary>
    public Task<TrainingSession> AddAsync(TrainingSession entity)
    {
        entity.Id = context.TrainingSessions.Select(c => c.Id).DefaultIfEmpty(0).Max() + 1;
        context.TrainingSessions.Add(entity);

        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновляет данные существующей тренировки
    /// </summary>
    public Task<bool> UpdateAsync(TrainingSession entity)
    {
        var existing = context.TrainingSessions.FirstOrDefault(s => s.Id == entity.Id);
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
    public Task<bool> DeleteAsync(int id)
    {
        var entity = context.TrainingSessions.FirstOrDefault(s => s.Id == id);
        if (entity == null) return Task.FromResult(false);

        context.TrainingSessions.Remove(entity);
        
        return Task.FromResult(true);
    }
}