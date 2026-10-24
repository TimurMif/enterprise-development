using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dto;

public record SpecializationEditDto
{
    [Required(ErrorMessage = "Название специализации обязательно")]
    public required string Name { get; init; }
}