using FitnessClub.Domain.Models;
using FitnessClub.Server.Services;
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

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    /// <summary>
    /// Список всех клиентов
    /// </summary>
    [HttpGet]
    public ActionResult<List<Client>> GetAll()
    {
        return Ok(_clientService.GetAll());
    }

    /// <summary>
    /// Информация о клиенте по его Id
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<Client> GetById(int id)
    {
        var client = _clientService.GetById(id);
        if (client == null) return NotFound();
        return Ok(client);
    }

    /// <summary>
    /// Создает новую запись клиента
    /// </summary>
    [HttpPost]
    public ActionResult<Client> Create([FromBody] Client client)
    {
        var createdClient = _clientService.Create(client);
        return CreatedAtAction(nameof(GetById), new { id = createdClient.Id }, createdClient);
    }

    /// <summary>
    /// Обновляет данные существующего клиента по его Id
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Client client)
    {
        var success = _clientService.Update(id, client);
        if (!success) return NotFound();
        return NoContent();
    }

    /// <summary>
    /// Удаляет клиента по его Id
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var success = _clientService.Delete(id);
        if (!success) return NotFound();
        return NoContent();
    }
}