using FitnessClub.Domain.Context;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

public class ClientRepository(FitnessClubContext context) : IRepository<Client>
{
    /// <summary>
    /// Возвращает список всех клиентов
    /// </summary>
    public Task<List<Client>> GetAllAsync()
    {
        return Task.FromResult(context.Clients.ToList());
    }

    /// <summary>
    /// Возвращает клиента по его Id
    /// </summary>
    public Task<Client?> GetByIdAsync(int id)
    {
        return Task.FromResult(context.Clients.FirstOrDefault(c => c.Id == id));
    }

    /// <summary>
    /// Добавляет нового клиента
    /// </summary>
    public Task<Client> AddAsync(Client entity)
    {
        entity.Id = context.Clients.Select(c => c.Id).DefaultIfEmpty(0).Max() + 1;
        context.Clients.Add(entity);

        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновляет данные существующего клиента
    /// </summary>
    public Task<bool> UpdateAsync(Client entity)
    {
        var existing = context.Clients.FirstOrDefault(c => c.Id == entity.Id);
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
    public Task<bool> DeleteAsync(int id)
    {
        var entity = context.Clients.FirstOrDefault(c => c.Id == id);
        if (entity == null) return Task.FromResult(false);
        
        context.TrainingSessions.RemoveAll(s => s.ClientId == id);
        context.Clients.Remove(entity);
        
        return Task.FromResult(true);
    }
}