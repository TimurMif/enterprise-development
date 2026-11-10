using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Domain.Models;

/// <summary>
/// Справочник специализаций тренеров
/// </summary>
[Table("specializations")]
public class Specialization
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Название специализации
    /// </summary>
    [Column("name")]
    public required string Name { get; set; }
}