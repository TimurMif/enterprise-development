namespace FitnessClub.Domain.Models;

/// <summary>
/// Спортивный зал для проведения занятий
/// </summary>
public class GymHall
{
    /// <summary>
    /// Уникальный идентификатор зала
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название зала
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Вместимость
    /// </summary>
    public int Capacity { get; set; }
}