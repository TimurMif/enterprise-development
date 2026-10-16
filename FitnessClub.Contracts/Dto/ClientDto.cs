using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

public record ClientDto
{
    public int Id { get; init; }
    public required string PassportNumber { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? MiddleName { get; init; }
    public Gender Gender { get; init; }
    public DateOnly DateOfBirth { get; init; }
    public required string PhoneNumber { get; init; }
    public DateOnly SubscriptionStartDate { get; init; }
    public DateOnly SubscriptionEndDate { get; init; }
}