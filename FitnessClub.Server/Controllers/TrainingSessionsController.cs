using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Server.Controllers;

/// <summary>
/// Контроллер для занятий в фитнес-клубе
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TrainingSessionsController(ILogger<TrainingSessionsController> logger,
    ITrainingSessionService sessionService) : ControllerBase
{
    /// <summary>
    /// Список всех тренировок
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<TrainingSessionDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<TrainingSessionDto>>> GetAll()
    {
        logger.LogInformation("Запрос на получение списка всех тренировок.");
        try
        {
            var result = await sessionService.GetAllAsync();
            logger.LogInformation("Успешно получено тренировок: {Count}.", result.Count);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка тренировок.");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Информация о тренировке по Id
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TrainingSessionDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TrainingSessionDto>> GetById(int id)
    {
        logger.LogInformation("Запрос информации о тренировке с ID {Id}.", id);
        try
        {
            var session = await sessionService.GetByIdAsync(id);
            if (session == null)
            {
                logger.LogWarning("Тренировка с ID {Id} не найдена.", id);
                return NotFound();
            }
            logger.LogInformation("Успешно получена тренировка с ID {Id}.", id);
            return Ok(session);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении тренировки с ID {Id}.", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Создает новую запись тренировки
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(TrainingSessionDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TrainingSessionDto>> Create([FromBody] TrainingSessionEditDto sessionDto)
    {
        logger.LogInformation("Попытка создания новой тренировки для клиента ID {ClientId}, тренера ID {TrainerId} в зале ID {HallId}.",
            sessionDto.ClientId, sessionDto.TrainerId, sessionDto.GymHallId);
        try
        {
            var created = await sessionService.CreateAsync(sessionDto);
            if (created == null)
            {
                logger.LogWarning("Не удалось создать тренировку: неверный ID клиента, тренера или зала.");
                return BadRequest("Неверный ID клиента, тренера или зала");
            }
            logger.LogInformation("Успешно создана тренировка с ID {Id}.", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании тренировки.");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновляет данные существующего занятия по его Id
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update(int id, [FromBody] TrainingSessionEditDto sessionDto)
    {
        logger.LogInformation("Попытка обновления тренировки с ID {Id}.", id);
        try
        {
            var success = await sessionService.UpdateAsync(id, sessionDto);
            if (!success)
            {
                logger.LogWarning("Тренировка с ID {Id} не найдена или связанные сущности недействительны.", id);
                return NotFound();
            }
            logger.LogInformation("Успешно обновлена тренировка с ID {Id}.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении тренировки с ID {Id}.", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удаляет тренировку по Id
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(int id)
    {
        logger.LogInformation("Попытка удаления тренировки с ID {Id}.", id);
        try
        {
            var success = await sessionService.DeleteAsync(id);
            if (!success)
            {
                logger.LogWarning("Тренировка с ID {Id} не найдена для удаления.", id);
                return NotFound();
            }
            logger.LogInformation("Успешно удалена тренировка с ID {Id}.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении тренировки с ID {Id}.", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}