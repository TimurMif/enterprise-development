using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// Объект Dto для редактирования параметров зала тренировок
/// </summary>
public record GymHallEditDto
{
    /// <summary>
    /// Название зала
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// Вместимость зала
    /// </summary>
    public int Capacity { get; init; }
}