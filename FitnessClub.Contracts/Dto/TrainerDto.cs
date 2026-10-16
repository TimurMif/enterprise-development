using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

public record TrainerDto
{
    public int Id { get; init; }
    public required string PassportNumber { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? MiddleName { get; init; }
    public Gender Gender { get; init; }
    public DateOnly DateOfBirth { get; init; }
    public required string PhoneNumber { get; init; }
    public int ExperienceYears { get; init; }
    public int SpecializationId { get; init; }
    public required SpecializationDto Specialization { get; init; }
}