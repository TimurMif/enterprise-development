using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Domain.Models;

/// <summary>
/// Спортивный зал для проведения занятий
/// </summary>
[Table("gym_halls")]
public class GymHall
{
    /// <summary>
    /// Уникальный идентификатор зала
    /// </summary>
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Название зала
    /// </summary>
    [Column("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Вместимость
    /// </summary>
    [Column("capacity")]
    public int Capacity { get; set; }
}