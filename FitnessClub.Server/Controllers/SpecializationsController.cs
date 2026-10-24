using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Server.Controllers;

/// <summary>
/// Контроллер для специализаций тренеров
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SpecializationsController : ControllerBase
{
    private readonly ISpecializationService _specializationService;
    private readonly ILogger<SpecializationsController> _logger;

    /// <summary>
    /// Инициализирует новый экземпляр контроллера специализаций
    /// </summary>
    public SpecializationsController(
        ISpecializationService specializationService,
        ILogger<SpecializationsController> logger)
    {
        _specializationService = specializationService;
        _logger = logger;
    }

    /// <summary>
    /// Список всех специализаций
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<SpecializationDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<SpecializationDto>>> GetAll()
    {
        _logger.LogInformation("Запрос на получение списка всех специализаций.");
        try
        {
            var result = await _specializationService.GetAllAsync();
            _logger.LogInformation("Успешно получено специализаций: {Count}.", result.Count);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении списка специализаций.");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Информация о специализации по ее Id
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SpecializationDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<SpecializationDto>> GetById(int id)
    {
        _logger.LogInformation("Запрос информации о специализации с ID {Id}.", id);
        try
        {
            var specialization = await _specializationService.GetByIdAsync(id);
            if (specialization == null)
            {
                _logger.LogWarning("Специализация с ID {Id} не найдена.", id);
                return NotFound();
            }
            _logger.LogInformation("Успешно получена специализация с ID {Id}.", id);
            return Ok(specialization);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении специализации с ID {Id}.", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Создает новую запись специализации
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(SpecializationDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<SpecializationDto>> Create([FromBody] SpecializationEditDto specializationDto)
    {
        _logger.LogInformation("Попытка создания новой специализации {Name}.", specializationDto.Name);
        try
        {
            var created = await _specializationService.CreateAsync(specializationDto);
            _logger.LogInformation("Успешно создана специализация с ID {Id}.", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании специализации.");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновляет данные существующей специализации по ее Id
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update(int id, [FromBody] SpecializationEditDto specializationDto)
    {
        _logger.LogInformation("Попытка обновления специализации с ID {Id}.", id);
        try
        {
            var success = await _specializationService.UpdateAsync(id, specializationDto);
            if (!success)
            {
                _logger.LogWarning("Специализация с ID {Id} не найдена для обновления.", id);
                return NotFound();
            }
            _logger.LogInformation("Успешно обновлена специализация с ID {Id}.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении специализации с ID {Id}.", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удаляет специализацию по ее Id
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Попытка удаления специализации с ID {Id}.", id);
        try
        {
            var success = await _specializationService.DeleteAsync(id);
            if (!success)
            {
                _logger.LogWarning("Специализация с ID {Id} не найдена для удаления.", id);
                return NotFound();
            }
            _logger.LogInformation("Успешно удалена специализация с ID {Id}.", id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Специализацию с ID {Id} нельзя удалить: она назначена тренерам.", id);
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении специализации с ID {Id}.", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}