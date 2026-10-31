using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Интерфейс сервиса для клиентов
/// </summary>
public interface IClientService
{
    /// <summary>
    /// Возвращает список всех клиентов
    /// </summary>
    public Task<List<ClientDto>> GetAllAsync();

    /// <summary>
    /// Возвращает клиента по его Id
    /// </summary>
    public Task<ClientDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создает нового клиента
    /// </summary>
    public Task<ClientDto> CreateAsync(ClientEditDto dto);

    /// <summary>
    /// Обновляет данные существующего клиента
    /// </summary>
    public Task<bool> UpdateAsync(int id, ClientEditDto dto);

    /// <summary>
    /// Удаляет клиента по его Id
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}