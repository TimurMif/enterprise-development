using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Server.Controllers;

/// <summary>
/// Контроллер для клиентов фитнес-клуба
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;
    private readonly ILogger<ClientsController> _logger;

    public ClientsController(IClientService clientService, ILogger<ClientsController> logger)
    {
        _clientService = clientService;
        _logger = logger;
    }

    /// <summary>
    /// Список всех клиентов
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<ClientDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<ClientDto>>> GetAll()
    {
        _logger.LogInformation("Запрос на получение списка всех клиентов.");
        try
        {
            var result = await _clientService.GetAllAsync();
            _logger.LogInformation("Успешно получено клиентов: {Count}.", result.Count);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении списка клиентов.");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Информация о клиенте по его Id
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClientDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ClientDto>> GetById(int id)
    {
        _logger.LogInformation("Запрос информации о клиенте с ID {Id}.", id);
        try
        {
            var client = await _clientService.GetByIdAsync(id);
            if (client == null)
            {
                _logger.LogWarning("Клиент с ID {Id} не найден.", id);
                return NotFound();
            }
            _logger.LogInformation("Успешно получен клиент с ID {Id}.", id);
            return Ok(client);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении клиента с ID {Id}.", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Создает новую запись клиента
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ClientDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ClientDto>> Create([FromBody] ClientEditDto clientDto)
    {
        _logger.LogInformation("Попытка создания нового клиента.");
        try
        {
            var createdClient = await _clientService.CreateAsync(clientDto);
            _logger.LogInformation("Успешно создан клиент с ID {Id}.", createdClient.Id);
            return CreatedAtAction(nameof(GetById), new { id = createdClient.Id }, createdClient);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании клиента.");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновляет данные существующего клиента по его Id
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update(int id, [FromBody] ClientEditDto clientDto)
    {
        _logger.LogInformation("Попытка обновления клиента с ID {Id}.", id);
        try
        {
            var success = await _clientService.UpdateAsync(id, clientDto);
            if (!success)
            {
                _logger.LogWarning("Клиент с ID {Id} не найден для обновления.", id);
                return NotFound();
            }
            _logger.LogInformation("Успешно обновлен клиент с ID {Id}.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении клиента с ID {Id}.", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удаляет клиента по его Id
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Попытка удаления клиента с ID {Id}.", id);
        try
        {
            var success = await _clientService.DeleteAsync(id);
            if (!success)
            {
                _logger.LogWarning("Клиент с ID {Id} не найден для удаления.", id);
                return NotFound();
            }
            _logger.LogInformation("Успешно удален клиент с ID {Id}.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении клиента с ID {Id}.", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}