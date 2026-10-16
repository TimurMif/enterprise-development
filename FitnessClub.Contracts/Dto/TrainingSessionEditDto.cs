using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dto;

public record TrainingSessionEditDto
{
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный ID клиента")]
    public int ClientId { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "Некорректный ID тренера")]
    public int TrainerId { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "Некорректный ID зала")]
    public int GymHallId { get; init; }

    public DateTime DateTime { get; init; }

    public TimeSpan Duration { get; init; } = TimeSpan.FromHours(1);

    public bool IsTrial { get; init; }
}