using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Server.Controllers;

/// <summary>
/// Контроллер для тренеров фитнес-клуба
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TrainersController : ControllerBase
{
    private readonly ILogger<TrainersController> _logger;
    private readonly ITrainerService _trainerService;

    /// <summary>
    /// Инициализирует новый экземпляр контроллера тренеров
    /// </summary>
    public TrainersController(ITrainerService trainerService, ILogger<TrainersController> logger)
    {
        _trainerService = trainerService;
        _logger = logger;
    }

    /// <summary>
    /// Список всех тренеров
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<TrainerDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<TrainerDto>>> GetAll()
    {
        _logger.LogInformation("Запрос на получение списка всех тренеров.");
        try
        {
            var result = await _trainerService.GetAllAsync();
            _logger.LogInformation("Успешно получено тренеров: {Count}.", result.Count);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении списка тренеров.");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Информация о тренере по его Id
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TrainerDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TrainerDto>> GetById(int id)
    {
        _logger.LogInformation("Запрос информации о тренере с ID {Id}.", id);
        try
        {
            var trainer = await _trainerService.GetByIdAsync(id);
            if (trainer == null)
            {
                _logger.LogWarning("Тренер с ID {Id} не найден.", id);
                return NotFound();
            }
            _logger.LogInformation("Успешно получен тренер с ID {Id}.", id);
            return Ok(trainer);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении тренера с ID {Id}.", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Создает новую запись тренера
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(TrainerDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TrainerDto>> Create([FromBody] TrainerEditDto trainerDto)
    {
        _logger.LogInformation("Попытка создания нового тренера.");
        try
        {
            var created = await _trainerService.CreateAsync(trainerDto);
            if (created == null)
            {
                _logger.LogWarning("Не удалось создать тренера: указана несуществующая специализация с ID {SpecId}.", trainerDto.SpecializationId);
                return BadRequest("Указана несуществующая специализация");
            }
            _logger.LogInformation("Успешно создан тренер с ID {Id}.", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании тренера.");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновляет данные существующего тренера по его Id
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update(int id, [FromBody] TrainerEditDto trainerDto)
    {
        _logger.LogInformation("Попытка обновления тренера с ID {Id}.", id);
        try
        {
            var success = await _trainerService.UpdateAsync(id, trainerDto);
            if (!success)
            {
                _logger.LogWarning("Тренер с ID {Id} не найден или специализация недействительна.", id);
                return NotFound();
            }
            _logger.LogInformation("Успешно обновлен тренер с ID {Id}.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении тренера с ID {Id}.", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удаляет тренера по его Id
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogInformation("Попытка удаления тренера с ID {Id}.", id);
        try
        {
            var success = await _trainerService.DeleteAsync(id);
            if (!success)
            {
                _logger.LogWarning("Тренер с ID {Id} не найден для удаления.", id);
                return NotFound();
            }
            _logger.LogInformation("Успешно удален тренер с ID {Id}.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении тренера с ID {Id}.", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}