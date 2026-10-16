using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Context;
using FitnessClub.Domain.Models;

namespace FitnessClub.Application.Services;

public class ClientService : IClientService
{
    private readonly FitnessClubContext _context;

    public ClientService(FitnessClubContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Возвращает список всех клиентов фитнес-клуба
    /// </summary>
    public Task<List<ClientDto>> GetAllAsync()
    {
        var dtos = _context.Clients.Select(MapToDto).ToList();
        return Task.FromResult(dtos);
    }

    /// <summary>
    /// Возвращает информацию о клиенте по его Id
    /// </summary>
    public Task<ClientDto?> GetByIdAsync(int id)
    {
        var client = _context.Clients.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(client == null ? null : MapToDto(client));
    }

    /// <summary>
    /// Создает новую запись клиента на основе переданных данных
    /// </summary>
    public Task<ClientDto> CreateAsync(ClientEditDto dto)
    {
        var newId = _context.Clients.Any() ? _context.Clients.Max(c => c.Id) + 1 : 1;
        var client = new Client
        {
            Id = newId,
            PassportNumber = dto.PassportNumber,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MiddleName = dto.MiddleName,
            Gender = dto.Gender,
            DateOfBirth = dto.DateOfBirth,
            PhoneNumber = dto.PhoneNumber,
            SubscriptionStartDate = dto.SubscriptionStartDate,
            SubscriptionEndDate = dto.SubscriptionEndDate
        };

        _context.Clients.Add(client);
        return Task.FromResult(MapToDto(client));
    }

    /// <summary>
    /// Обновляет данные существующего клиента по его Id
    /// </summary>
    public Task<bool> UpdateAsync(int id, ClientEditDto updatedDto)
    {
        var client = _context.Clients.FirstOrDefault(c => c.Id == id);
        if (client == null) return Task.FromResult(false);

        client.PassportNumber = updatedDto.PassportNumber;
        client.FirstName = updatedDto.FirstName;
        client.LastName = updatedDto.LastName;
        client.MiddleName = updatedDto.MiddleName;
        client.Gender = updatedDto.Gender;
        client.DateOfBirth = updatedDto.DateOfBirth;
        client.PhoneNumber = updatedDto.PhoneNumber;
        client.SubscriptionStartDate = updatedDto.SubscriptionStartDate;
        client.SubscriptionEndDate = updatedDto.SubscriptionEndDate;

        return Task.FromResult(true);
    }

    /// <summary>
    /// Удаляет клиента по его Id вместе с сопутствующими сессиями
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var client = _context.Clients.FirstOrDefault(c => c.Id == id);
        if (client == null) return Task.FromResult(false);

        _context.TrainingSessions.RemoveAll(s => s.ClientId == id);
        _context.Clients.Remove(client);
        return Task.FromResult(true);
    }

    /// <summary>
    /// Преобразует доменную модель клиента в объект передачи данных DTO
    /// </summary>
    private static ClientDto MapToDto(Client c)
    {
        return new ClientDto
        {
            Id = c.Id,
            PassportNumber = c.PassportNumber,
            FirstName = c.FirstName,
            LastName = c.LastName,
            MiddleName = c.MiddleName,
            Gender = c.Gender,
            DateOfBirth = c.DateOfBirth,
            PhoneNumber = c.PhoneNumber,
            SubscriptionStartDate = c.SubscriptionStartDate,
            SubscriptionEndDate = c.SubscriptionEndDate
        };
    }
}