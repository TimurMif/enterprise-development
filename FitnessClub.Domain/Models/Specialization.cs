namespace FitnessClub.Domain.Models;

/// <summary>
///     Справочник специализаций тренеров
/// </summary>
public class Specialization
{
    /// <summary>
    ///     Уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    ///     Название специализации
    /// </summary>
    public required string Name { get; set; }
}