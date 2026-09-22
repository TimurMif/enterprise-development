namespace FitnessClub.Domain.Models;

/// <summary>
/// Справочник специализаций тренеров
/// </summary>
public class Specialization
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название специализации
    /// </summary>
    public required string Name { get; set; }
}