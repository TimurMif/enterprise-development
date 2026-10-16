using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

public interface IClientService
{
    /// <summary>
    /// Возвращает список всех клиентов
    /// </summary>
    Task<List<ClientDto>> GetAllAsync();

    /// <summary>
    /// Возвращает клиента по его Id
    /// </summary>
    Task<ClientDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создает нового клиента
    /// </summary>
    Task<ClientDto> CreateAsync(ClientEditDto dto);

    /// <summary>
    /// Обновляет данные существующего клиента
    /// </summary>
    Task<bool> UpdateAsync(int id, ClientEditDto dto);

    /// <summary>
    /// Удаляет клиента по его Id
    /// </summary>
    Task<bool> DeleteAsync(int id);
}