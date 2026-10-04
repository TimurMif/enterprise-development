using FitnessClub.Domain.Context;
using FitnessClub.Domain.Models;

namespace FitnessClub.Server.Services;

/// <summary>
/// Сервис для операций с клиентами
/// </summary>
public class ClientService : IClientService
{
    private readonly FitnessClubContext _context;

    public ClientService(FitnessClubContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Возвращает список всех клиентов
    /// </summary>
    public List<Client> GetAll()
    {
        return _context.Clients;
    }

    /// <summary>
    /// Возвращает клиента по Id
    /// </summary>
    public Client? GetById(int id)
    {
        return _context.Clients.FirstOrDefault(c => c.Id == id);
    }

    /// <summary>
    /// Добавляет нового клиента
    /// </summary>
    public Client Create(Client client)
    {
        client.Id = _context.Clients.Any() ? _context.Clients.Max(c => c.Id) + 1 : 1;
        _context.Clients.Add(client);
        return client;
    }

    /// <summary>
    /// Обновляет сведения о клиенте по его Id
    /// </summary>
    public bool Update(int id, Client updatedClient)
    {
        var client = _context.Clients.FirstOrDefault(c => c.Id == id);
        if (client == null) return false;

        client.PassportNumber = updatedClient.PassportNumber;
        client.FirstName = updatedClient.FirstName;
        client.LastName = updatedClient.LastName;
        client.MiddleName = updatedClient.MiddleName;
        client.Gender = updatedClient.Gender;
        client.DateOfBirth = updatedClient.DateOfBirth;
        client.PhoneNumber = updatedClient.PhoneNumber;
        client.SubscriptionStartDate = updatedClient.SubscriptionStartDate;
        client.SubscriptionEndDate = updatedClient.SubscriptionEndDate;

        return true;
    }

    /// <summary>
    /// Удаляет клиента из коллекции по его Id
    /// </summary>
    public bool Delete(int id)
    {
        var client = _context.Clients.FirstOrDefault(c => c.Id == id);
        if (client == null) return false;
        
        _context.TrainingSessions.RemoveAll(s => s.ClientId == id);

        _context.Clients.Remove(client);
        return true;
    }
}