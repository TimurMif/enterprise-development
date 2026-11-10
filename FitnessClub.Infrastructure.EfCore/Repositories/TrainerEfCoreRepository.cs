using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий тренеров в базе данных
/// </summary>
public class TrainerEfCoreRepository(FitnessClubDbContext context) : IRepository<Trainer>
{
    /// <summary>
    /// Возвращает список всех тренеров
    /// </summary>
    public async Task<List<Trainer>> GetAllAsync() => 
        await context.Trainers.Include(t => t.Specialization).ToListAsync();

    /// <summary>
    /// Возвращает тренера по его Id
    /// </summary>
    public async Task<Trainer?> GetByIdAsync(int id) => 
        await context.Trainers.Include(t => t.Specialization).FirstOrDefaultAsync(t => t.Id == id);

    /// <summary>
    /// Добавляет нового тренера в базу данных
    /// </summary>
    public async Task<Trainer> AddAsync(Trainer entity)
    {
        var result = await context.Trainers.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Обновляет данные существующего тренера
    /// </summary>
    public async Task<bool> UpdateAsync(Trainer entity)
    {
        var existing = await context.Trainers.FirstOrDefaultAsync(t => t.Id == entity.Id);
        if (existing == null) return false;
        context.Entry(existing).CurrentValues.SetValues(entity);
        existing.Specialization = entity.Specialization;
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Удаляет тренера по его Id
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.Trainers.FirstOrDefaultAsync(t => t.Id == id);
        if (entity == null) return false;
        context.Trainers.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}