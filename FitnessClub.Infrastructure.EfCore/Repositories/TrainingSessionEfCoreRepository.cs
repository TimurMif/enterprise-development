using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий тренировок в базе данных
/// </summary>
public class TrainingSessionEfCoreRepository(FitnessClubDbContext context) : IRepository<TrainingSession>
{
    /// <summary>
    /// Возвращает список всех тренировок
    /// </summary>
    public async Task<List<TrainingSession>> GetAllAsync() => 
        await context.TrainingSessions
            .Include(ts => ts.Client)
            .Include(ts => ts.GymHall)
            .Include(ts => ts.Trainer)
                .ThenInclude(t => t!.Specialization)
            .ToListAsync();
    
    /// <summary>
    /// Возвращает тренировку по ее Id
    /// </summary>
    public async Task<TrainingSession?> GetByIdAsync(int id) => 
        await context.TrainingSessions
            .Include(ts => ts.Client)
            .Include(ts => ts.GymHall)
            .Include(ts => ts.Trainer)
                .ThenInclude(t => t!.Specialization)
            .FirstOrDefaultAsync(ts => ts.Id == id);

    /// <summary>
    /// Добавляет новую тренировку в базу данных
    /// </summary>
    public async Task<TrainingSession> AddAsync(TrainingSession entity)
    {
        var result = await context.TrainingSessions.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Обновляет данные существующей тренировки
    /// </summary>
    public async Task<bool> UpdateAsync(TrainingSession entity)
    {
        var existing = await context.TrainingSessions.FirstOrDefaultAsync(ts => ts.Id == entity.Id);
        if (existing == null) return false;
        context.Entry(existing).CurrentValues.SetValues(entity);
        existing.Client = entity.Client;
        existing.Trainer = entity.Trainer;
        existing.GymHall = entity.GymHall;
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Удаляет тренировку по ее Id
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.TrainingSessions.FirstOrDefaultAsync(ts => ts.Id == id);
        if (entity == null) return false;
        context.TrainingSessions.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}