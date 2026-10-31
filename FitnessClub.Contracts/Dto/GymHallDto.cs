namespace FitnessClub.Contracts.Dto;

/// <summary>
/// Класс для Dto зала тренировок
/// </summary>
public record GymHallDto
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; init; }
    /// <summary>
    /// Название зала
    /// </summary>
    public required string Name { get; init; }
    /// <summary>
    /// Вместимость
    /// </summary>
    public int Capacity { get; init; }
}