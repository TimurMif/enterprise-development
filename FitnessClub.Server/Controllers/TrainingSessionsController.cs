using FitnessClub.Domain.Models;
using FitnessClub.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Server.Controllers;

/// <summary>
/// Контроллер для занятий в фитнес-клубе
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TrainingSessionsController : ControllerBase
{
    private readonly ITrainingSessionService _sessionService;

    public TrainingSessionsController(ITrainingSessionService sessionService)
    {
        _sessionService = sessionService;
    }

    /// <summary>
    /// Cписок всех тренировок
    /// </summary>
    [HttpGet]
    public ActionResult<List<TrainingSession>> GetAll()
    {
        return Ok(_sessionService.GetAll());
    }

    /// <summary>
    /// Информация о тренировке по Id
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<TrainingSession> GetById(int id)
    {
        var session = _sessionService.GetById(id);
        if (session == null) return NotFound();
        return Ok(session);
    }

    /// <summary>
    /// Создает новую запись тренировки
    /// </summary>
    [HttpPost]
    public ActionResult<TrainingSession> Create([FromBody] TrainingSession session)
    {
        var created = _sessionService.Create(session);
        if (created == null) return BadRequest("Неверный ID клиента, тренера или зала");
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    
    /// <summary>
    /// Обновляет данные существующей тренировки по ее Id
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] TrainingSession session)
    {
        var success = _sessionService.Update(id, session);
        if (!success) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Удаляет тренировку по Id
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var success = _sessionService.Delete(id);
        if (!success) return NotFound();
        return NoContent();
    }
}