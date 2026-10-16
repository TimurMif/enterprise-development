namespace FitnessClub.Contracts.Dto;

public record GymHallDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public int Capacity { get; init; }
}