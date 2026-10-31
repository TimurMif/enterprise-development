namespace FitnessClub.Contracts.Dto;

/// <summary>
/// Тренер вместе с количеством его занятий
/// </summary>
public record PopularTrainerDto
{
    /// <summary>
    /// Навигационное свойство тренера
    /// </summary>
    public required TrainerDto Trainer { get; init; }

    /// <summary>
    /// Количество занятий у тренера
    /// </summary>
    public int SessionsCount { get; init; }
}