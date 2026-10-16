using FitnessClub.Domain.Context;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

public class ClientRepository : IRepository<Client>
{
    private readonly FitnessClubContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр репозитория клиентов
    /// </summary>
    public ClientRepository(FitnessClubContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Возвращает список всех клиентов
    /// </summary>
    public Task<List<Client>> GetAllAsync()
    {
        return Task.FromResult(_context.Clients);
    }

    /// <summary>
    /// Возвращает клиента по его Id
    /// </summary>
    public Task<Client?> GetByIdAsync(int id)
    {
        return Task.FromResult(_context.Clients.FirstOrDefault(c => c.Id == id));
    }

    /// <summary>
    /// Добавляет нового клиента
    /// </summary>
    public Task AddAsync(Client entity)
    {
        _context.Clients.Add(entity);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Обновляет данные существующего клиента
    /// </summary>
    public Task<bool> Update(Client entity)
    {
        var existing = _context.Clients.FirstOrDefault(c => c.Id == entity.Id);
        if (existing == null) return Task.FromResult(false);
        
        existing.PassportNumber = entity.PassportNumber;
        existing.FirstName = entity.FirstName;
        existing.LastName = entity.LastName;
        existing.MiddleName = entity.MiddleName;
        existing.Gender = entity.Gender;
        existing.DateOfBirth = entity.DateOfBirth;
        existing.PhoneNumber = entity.PhoneNumber;
        existing.SubscriptionStartDate = entity.SubscriptionStartDate;
        existing.SubscriptionEndDate = entity.SubscriptionEndDate;

        return Task.FromResult(true);
    }

    /// <summary>
    /// Удаляет клиента по его Id
    /// </summary>
    public Task<bool> Delete(int id)
    {
        var entity = _context.Clients.FirstOrDefault(c => c.Id == id);
        if (entity == null) return Task.FromResult(false);
        
        _context.TrainingSessions.RemoveAll(s => s.ClientId == id);
        _context.Clients.Remove(entity);
        
        return Task.FromResult(true);
    }
}