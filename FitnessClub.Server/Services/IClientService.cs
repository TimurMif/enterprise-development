using FitnessClub.Domain.Models;

namespace FitnessClub.Server.Services;

/// <summary>
/// Интерфейс сервиса клиентов
/// </summary>
public interface IClientService
{
    List<Client> GetAll();
    Client? GetById(int id);
    Client Create(Client client);
    bool Update(int id, Client client);
    bool Delete(int id);
}