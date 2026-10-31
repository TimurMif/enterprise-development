namespace FitnessClub.Contracts.Dto;

/// <summary>
/// Класс для Dto специализаций тренеров
/// </summary>
public record SpecializationDto
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; init; }
    /// <summary>
    /// Название специализации
    /// </summary>
    public required string Name { get; init; }
}