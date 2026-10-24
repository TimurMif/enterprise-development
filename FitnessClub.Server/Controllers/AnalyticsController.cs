using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Server.Controllers;

/// <summary>
/// Контроллер для выполнения аналитических запросов фитнес-клуба
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;
    private readonly ILogger<AnalyticsController> _logger;

    public AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger)
    {
        _analyticsService = analyticsService;
        _logger = logger;
    }

    /// <summary>
    /// Тренеры со стажем не менее 5 лет
    /// </summary>
    [HttpGet("experienced-trainers")]
    [ProducesResponseType(typeof(List<TrainerDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<TrainerDto>>> GetExperiencedTrainers()
    {
        _logger.LogInformation("Запрос на получение списка тренеров со стажем не менее 5 лет.");
        try
        {
            var result = await _analyticsService.GetTrainersWithAtLeast5YearsExperienceAsync();
            _logger.LogInformation("Успешно получено тренеров со стажем: {Count}.", result.Count);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении списка опытных тренеров.");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Проверяет доступность зала для записи на указанный момент времени
    /// </summary>
    [HttpGet("hall-availability")]
    [ProducesResponseType(typeof(bool), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<bool>> CheckHallAvailability([FromQuery] int hallId, [FromQuery] DateTime checkTime)
    {
        _logger.LogInformation("Проверка доступности зала с ID {HallId} на время {CheckTime}.", hallId, checkTime);
        try
        {
            var result = await _analyticsService.IsHallAvailableAsync(hallId, checkTime);
            _logger.LogInformation("Статус доступности зала с ID {HallId} на {CheckTime}: {IsAvailable}.", hallId, checkTime, result);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при проверке доступности зала {HallId} на время {CheckTime}.", hallId, checkTime);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Список клиентов с просроченным абонементом на заданную дату
    /// </summary>
    [HttpGet("expired-subscriptions")]
    [ProducesResponseType(typeof(List<ClientDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<ClientDto>>> GetExpiredSubscriptions([FromQuery] DateOnly? date)
    {
        var targetDate = date ?? DateOnly.FromDateTime(DateTime.Today);
        _logger.LogInformation("Запрос клиентов с просроченными абонементами на дату {TargetDate}.", targetDate);
        try
        {
            var result = await _analyticsService.GetClientsWithExpiredSubscriptionAsync(targetDate);
            _logger.LogInformation("Найдено клиентов с просроченными абонементами: {Count}.", result.Count);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении списка клиентов с просроченными абонементами.");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Список тренировочных сессий в выбранном зале за определенный месяц и год
    /// </summary>
    [HttpGet("sessions-by-hall-month")]
    [ProducesResponseType(typeof(List<TrainingSessionDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<TrainingSessionDto>>> GetSessionsForMonthInHall(
        [FromQuery] int hallId, [FromQuery] int year, [FromQuery] int month)
    {
        _logger.LogInformation("Запрос тренировок для зала с ID {HallId} за {Month}/{Year}.", hallId, month, year);
        try
        {
            var result = await _analyticsService.GetSessionsForMonthInHallAsync(hallId, year, month);
            _logger.LogInformation("Успешно получено тренировок для зала {HallId}: {Count}.", hallId, result.Count);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении сессий для зала {HallId} за {Month}/{Year}.", hallId, month, year);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Топ-5 популярных тренеров
    /// </summary>
    [HttpGet("top-5-trainers")]
    [ProducesResponseType(typeof(List<TrainerDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<TrainerDto>>> GetTopTrainers()
    {
        _logger.LogInformation("Запрос топ-5 популярных тренеров.");
        try
        {
            var result = await _analyticsService.GetTop5TrainersAsync();
            _logger.LogInformation("Успешно получено топ тренеров: {Count}.", result.Count);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении топ-5 тренеров.");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}