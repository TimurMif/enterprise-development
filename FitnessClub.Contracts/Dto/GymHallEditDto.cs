using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dto;

public record GymHallEditDto
{
    public required string Name { get; init; }

    public int Capacity { get; init; }
}