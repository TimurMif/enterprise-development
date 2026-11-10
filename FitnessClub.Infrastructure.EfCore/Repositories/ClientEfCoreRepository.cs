using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий клиентов в базе данных
/// </summary>
public class ClientEfCoreRepository(FitnessClubDbContext context) : IRepository<Client>
{
    /// <summary>
    /// Возвращает список всех клиентов
    /// </summary>
    public async Task<List<Client>> GetAllAsync() => await context.Clients.ToListAsync();
    
    /// <summary>
    /// Возвращает клиента по его Id
    /// </summary>
    public async Task<Client?> GetByIdAsync(int id) => await context.Clients.FirstOrDefaultAsync(c => c.Id == id);
    
    /// <summary>
    /// Добавляет нового клиента в базу данных
    /// </summary>
    public async Task<Client> AddAsync(Client entity)
    {
        var result = await context.Clients.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Обновляет данные существующего клиента
    /// </summary>
    public async Task<bool> UpdateAsync(Client entity)
    {
        var existing = await context.Clients.FirstOrDefaultAsync(c => c.Id == entity.Id);
        if (existing == null) return false;
        context.Clients.Update(entity);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Удаляет клиента по его Id
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        if (entity == null) return false;
        context.Clients.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}