namespace FitnessClub.Domain.Models;

/// <summary>
///     Спортивный зал для проведения занятий
/// </summary>
public class GymHall
{
    /// <summary>
    ///     Уникальный идентификатор зала
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    ///     Название зала
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Вместимость
    /// </summary>
    public int Capacity { get; set; }
}