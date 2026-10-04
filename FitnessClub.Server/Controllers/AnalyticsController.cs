using FitnessClub.Domain.Models;
using FitnessClub.Server.Services;
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

    public AnalyticsController(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    /// <summary>
    /// Тренеры со стажем не менее 5 лет
    /// </summary>
    [HttpGet("experienced-trainers")]
    public ActionResult<List<Trainer>> GetExperiencedTrainers()
    {
        return Ok(_analyticsService.GetTrainersWithAtLeast5YearsExperience());
    }

    /// <summary>
    /// Проверяет доступность зала для записи на указанный момент времени
    /// </summary>
    [HttpGet("hall-availability")]
    public ActionResult<bool> CheckHallAvailability([FromQuery] int hallId, [FromQuery] DateTime checkTime)
    {
        return Ok(_analyticsService.IsHallAvailable(hallId, checkTime));
    }

    /// <summary>
    /// Cписок клиентов с просроченным абонементом на заданную дату
    /// </summary>
    [HttpGet("expired-subscriptions")]
    public ActionResult<List<Client>> GetExpiredSubscriptions([FromQuery] DateOnly? date)
    {
        var targetDate = date ?? DateOnly.FromDateTime(DateTime.Today);
        return Ok(_analyticsService.GetClientsWithExpiredSubscription(targetDate));
    }

    /// <summary>
    /// Cписок тренировочных сессий в выбранном зале за определенный месяц и год
    /// </summary>
    [HttpGet("sessions-by-hall-month")]
    public ActionResult<List<TrainingSession>> GetSessionsForMonthInHall([FromQuery] int hallId, [FromQuery] int year,
        [FromQuery] int month)
    {
        return Ok(_analyticsService.GetSessionsForMonthInHall(hallId, year, month));
    }

    /// <summary>
    /// Топ-5 популярных тренеров.
    /// </summary>
    [HttpGet("top-5-trainers")]
    public ActionResult<List<Trainer>> GetTopTrainers()
    {
        return Ok(_analyticsService.GetTop5Trainers());
    }
}