using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий залов в базе данных
/// </summary>
public class GymHallEfCoreRepository(FitnessClubDbContext context) : IRepository<GymHall>
{
    /// <summary>
    /// Возвращает список всех залов
    /// </summary>
    public async Task<List<GymHall>> GetAllAsync() => await context.GymHalls.ToListAsync();

    /// <summary>
    /// Возвращает зал по его Id
    /// </summary>
    public async Task<GymHall?> GetByIdAsync(int id) => await context.GymHalls.FirstOrDefaultAsync(g => g.Id == id);

    /// <summary>
    /// Добавляет новый зал в базу данных
    /// </summary>
    public async Task<GymHall> AddAsync(GymHall entity)
    {
        var result = await context.GymHalls.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Обновляет данные существующего зала
    /// </summary>
    public async Task<bool> UpdateAsync(GymHall entity)
    {
        var existing = await context.GymHalls.FirstOrDefaultAsync(g => g.Id == entity.Id);
        if (existing == null) return false;
        context.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Удаляет зал по его Id
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.GymHalls.FirstOrDefaultAsync(g => g.Id == id);
        if (entity == null) return false;
        context.GymHalls.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}