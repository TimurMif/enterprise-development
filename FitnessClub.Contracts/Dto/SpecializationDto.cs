namespace FitnessClub.Contracts.Dto;

public record SpecializationDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
}