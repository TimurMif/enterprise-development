namespace FitnessClub.Domain.Models;

/// <summary>
/// Представляет информацию о тренере
/// </summary>
public class Trainer : Person
{
    /// <summary>
    /// Идентификатор специализации тренера
    /// </summary>
    public int SpecializationId { get; set; }

    /// <summary>
    /// Навигационное свойство специализации
    /// </summary>
    public required Specialization Specialization { get; set; }

    /// <summary>
    /// Стаж работы
    /// </summary>
    public int ExperienceYears { get; set; }
}