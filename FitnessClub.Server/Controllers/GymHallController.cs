using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Server.Controllers;

/// <summary>
/// Контроллер для залов фитнес-клуба
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class GymHallsController : ControllerBase
{
    private readonly IGymHallService _gymHallService;
    private readonly ILogger<GymHallsController> _logger;

    /// <summary>
    /// Инициализирует новый экземпляр контроллера залов
    /// </summary>
    public GymHallsController(IGymHallService gymHallService, ILogger<GymHallsController> logger)
    {
        _gymHallService = gymHallService;
        _logger = logger;
    }

    /// <summary>
    /// Список всех залов
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<GymHallDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<GymHallDto>>> GetAll()
    {
        _logger.LogInformation("Запрос на получение списка всех залов.");
        try
        {
            var result = await _gymHallService.GetAllAsync();
            _logger.LogInformation("Успешно получено залов: {Count}.", result.Count);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении списка залов.");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Информация о зале по его Id
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(GymHallDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<GymHallDto>> GetById(int id)
    {
        _logger.LogInformation("Запрос информации о зале с ID {Id}.", id);
        try
        {
            var hall = await _gymHallService.GetByIdAsync(id);
            if (hall == null)
            {
                _logger.LogWarning("Зал с ID {Id} не найден.", id);
                return NotFound();
            }
            _logger.LogInformation("Успешно получен зал с ID {Id}.", id);
            return Ok(hall);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении зала с ID {Id}.", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Создает новую запись зала
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(GymHallDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<GymHallDto>> Create([FromBody] GymHallEditDto hallDto)
    {
        _logger.LogInformation("Попытка создания нового зала {Name}.", hallDto.Name);
        try
        {
            var created = await _gymHallService.CreateAsync(hallDto);
            _logger.LogInformation("Успешно создан зал с ID {Id}.", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании зала.");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновляет данные существующего зала по его Id
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update(int id, [FromBody] GymHallEditDto hallDto)
    {
        _logger.LogInformation("Попытка обновления зала с ID {Id}.", id);
        try
        {
            var success = await _gymHallService.UpdateAsync(id, hallDto);
            if (!success)
            {
                _logger.LogWarning("Зал с ID {Id} не найден для обновления.", id);
                return NotFound();
            }
            _logger.LogInformation("Успешно обновлен зал с ID {Id}.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении зала с ID {Id}.", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удаляет зал по его Id вместе с его тренировками
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Попытка удаления зала с ID {Id}.", id);
        try
        {
            var success = await _gymHallService.DeleteAsync(id);
            if (!success)
            {
                _logger.LogWarning("Зал с ID {Id} не найден для удаления.", id);
                return NotFound();
            }
            _logger.LogInformation("Успешно удален зал с ID {Id}.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении зала с ID {Id}.", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}