using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// Объект Dto для редактирования специализаций
/// </summary>
public record SpecializationEditDto
{
    /// <summary>
    /// Название специализации
    /// </summary>
    [Required(ErrorMessage = "Название специализации обязательно")]
    public required string Name { get; init; }
}