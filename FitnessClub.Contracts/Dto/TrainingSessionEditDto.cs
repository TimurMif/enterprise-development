using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// Объект Dto для редактирования тренировок
/// </summary>
public record TrainingSessionEditDto
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный ID клиента")]
    public int ClientId { get; init; }
    
    /// <summary>
    /// Идентификатор тренера
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный ID тренера")]
    public int TrainerId { get; init; }
    
    /// <summary>
    /// Идентификатор зала
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный ID зала")]
    public int GymHallId { get; init; }

    /// <summary>
    /// Дата и время начала тренировки
    /// </summary>
    public DateTime DateTime { get; init; }

    /// <summary>
    /// Дата и время окончания тренировки
    /// </summary>
    public TimeSpan Duration { get; init; } = TimeSpan.FromHours(1);

    /// <summary>
    /// Занятие пробное или нет
    /// </summary>
    public bool IsTrial { get; init; }
}