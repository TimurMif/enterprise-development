namespace FitnessClub.Contracts.Dto;

public record TrainingSessionDto
{
    public int Id { get; init; }
    public int ClientId { get; init; }
    public required ClientDto Client { get; init; }
    public int TrainerId { get; init; }
    public required TrainerDto Trainer { get; init; }
    public int GymHallId { get; init; }
    public required GymHallDto GymHall { get; init; }
    public DateTime DateTime { get; init; }
    public TimeSpan Duration { get; init; }
    public bool IsTrial { get; init; }
}