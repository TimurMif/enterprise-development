using FitnessClub.Domain.Models;
using FitnessClub.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Server.Controllers;

/// <summary>
/// Контроллер для тренеров фитнес-клуба
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TrainersController : ControllerBase
{
    private readonly ITrainerService _trainerService;

    public TrainersController(ITrainerService trainerService)
    {
        _trainerService = trainerService;
    }

    /// <summary>
    /// Список всех тренеров
    /// </summary>
    [HttpGet]
    public ActionResult<List<Trainer>> GetAll()
    {
        return Ok(_trainerService.GetAll());
    }

    /// <summary>
    /// Информация о тренере по его Id.
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<Trainer> GetById(int id)
    {
        var trainer = _trainerService.GetById(id);
        if (trainer == null) return NotFound();
        return Ok(trainer);
    }

    /// <summary>
    /// Создает новую запись тренера
    /// </summary>
    [HttpPost]
    public ActionResult<Trainer> Create([FromBody] Trainer trainer)
    {
        var created = _trainerService.Create(trainer);
        if (created == null) return BadRequest("Указана несуществующая специализация");
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Обновляет данные существующего тренера по его Id
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Trainer trainer)
    {
        var success = _trainerService.Update(id, trainer);
        if (!success) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Удаляет тренера по его Id
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var success = _trainerService.Delete(id);
        if (!success) return NotFound();
        return NoContent();
    }
}