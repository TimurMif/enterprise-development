using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Server.Controllers;

/// <summary>
/// Контроллер для клиентов фитнес-клуба
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController(IClientService clientService, 
    ILogger<ClientsController> logger) : ControllerBase
{
    /// <summary>
    /// Список всех клиентов
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<ClientDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<ClientDto>>> GetAll()
    {
        logger.LogInformation("Запрос на получение списка всех клиентов.");
        try
        {
            var result = await clientService.GetAllAsync();
            logger.LogInformation("Успешно получено клиентов: {Count}.", result.Count);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка клиентов.");
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
        logger.LogInformation("Запрос информации о клиенте с ID {Id}.", id);
        try
        {
            var client = await clientService.GetByIdAsync(id);
            if (client == null)
            {
                logger.LogWarning("Клиент с ID {Id} не найден.", id);
                return NotFound();
            }
            logger.LogInformation("Успешно получен клиент с ID {Id}.", id);
            return Ok(client);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении клиента с ID {Id}.", id);
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
        logger.LogInformation("Попытка создания нового клиента.");
        try
        {
            var createdClient = await clientService.CreateAsync(clientDto);
            logger.LogInformation("Успешно создан клиент с ID {Id}.", createdClient.Id);
            return CreatedAtAction(nameof(GetById), new { id = createdClient.Id }, createdClient);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании клиента.");
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
        logger.LogInformation("Попытка обновления клиента с ID {Id}.", id);
        try
        {
            var success = await clientService.UpdateAsync(id, clientDto);
            if (!success)
            {
                logger.LogWarning("Клиент с ID {Id} не найден для обновления.", id);
                return NotFound();
            }
            logger.LogInformation("Успешно обновлен клиент с ID {Id}.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении клиента с ID {Id}.", id);
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
        logger.LogInformation("Попытка удаления клиента с ID {Id}.", id);
        try
        {
            var success = await clientService.DeleteAsync(id);
            if (!success)
            {
                logger.LogWarning("Клиент с ID {Id} не найден для удаления.", id);
                return NotFound();
            }
            logger.LogInformation("Успешно удален клиент с ID {Id}.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении клиента с ID {Id}.", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}