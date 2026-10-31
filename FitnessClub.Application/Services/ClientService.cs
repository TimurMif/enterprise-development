using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;
using FitnessClub.Application.Mapping;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис для работы с клиентами фитнес-клуба
/// </summary>
public class ClientService(IRepository<Client> clientRepository) : IClientService
{
    /// <summary>
    /// Возвращает список всех клиентов фитнес-клуба
    /// </summary>
    public async Task<List<ClientDto>> GetAllAsync()
    {
        var clients = await clientRepository.GetAllAsync();
        return clients.Select(c=>c.ToDto()).ToList();
    }

    /// <summary>
    /// Возвращает информацию о клиенте по его Id
    /// </summary>
    public async Task<ClientDto?> GetByIdAsync(int id)
    {
        var client = await clientRepository.GetByIdAsync(id);
        return client?.ToDto();
    }

    /// <summary>
    /// Создает новую запись клиента на основе переданных данных
    /// </summary>
    public async Task<ClientDto> CreateAsync(ClientEditDto dto)
    {
        var created = await clientRepository.AddAsync(dto.ToEntity());
        return created.ToDto();
    }

    /// <summary>
    /// Обновляет данные существующего клиента по его Id
    /// </summary>
    public async Task<bool> UpdateAsync(int id, ClientEditDto updatedDto)
    {
        var client = updatedDto.ToEntity();
        client.Id = id;

        return await clientRepository.UpdateAsync(client);
    }

    /// <summary>
    /// Удаляет клиента по его Id вместе с сопутствующими сессиями
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        return clientRepository.DeleteAsync(id);
    }
}