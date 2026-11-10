using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий специализаций в базе данных
/// </summary>
public class SpecializationEfCoreRepository(FitnessClubDbContext context) : IRepository<Specialization>
{
    /// <summary>
    /// Возвращает список всех специализаций
    /// </summary>
    public async Task<List<Specialization>> GetAllAsync() => await context.Specializations.ToListAsync();
    
    /// <summary>
    /// Возвращает специализацию по ее Id
    /// </summary>
    public async Task<Specialization?> GetByIdAsync(int id) => await context.Specializations.FirstOrDefaultAsync(s => s.Id == id);

    /// <summary>
    /// Добавляет новую специализацию в базу данных
    /// </summary>
    public async Task<Specialization> AddAsync(Specialization entity)
    {
        var result = await context.Specializations.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Обновляет данные существующей специализации
    /// </summary>
    public async Task<bool> UpdateAsync(Specialization entity)
    {
        var existing = await context.Specializations.FirstOrDefaultAsync(s => s.Id == entity.Id);
        if (existing == null) return false;
        context.Specializations.Update(entity);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Удаляет специализацию по ее Id
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.Specializations.FirstOrDefaultAsync(s => s.Id == id);
        if (entity == null) return false;
        context.Specializations.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}