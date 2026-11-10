using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Domain.Models;

/// <summary>
/// Представляет информацию о тренере
/// </summary>
[Table("trainers")]
public class Trainer : Person
{
    /// <summary>
    /// Идентификатор специализации тренера
    /// </summary>
    [Column("specialization_id")]
    public int SpecializationId { get; set; }

    /// <summary>
    /// Навигационное свойство специализации
    /// </summary>
    public Specialization? Specialization { get; set; }

    /// <summary>
    /// Стаж работы
    /// </summary>
    [Column("experience_years")]
    public int ExperienceYears { get; set; }
}