namespace FitnessClub.Contracts.Dto;

/// <summary>
/// Класс для Dto тренировок
/// </summary>
public record TrainingSessionDto
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int ClientId { get; init; }
    
    /// <summary>
    /// Навигационное свойство клиента
    /// </summary>
    public required ClientDto Client { get; init; }
    
    /// <summary>
    /// Идентификатор тренера
    /// </summary>
    public int TrainerId { get; init; }
    
    /// <summary>
    /// Навигационное свойство тренера
    /// </summary>
    public required TrainerDto Trainer { get; init; }
    
    /// <summary>
    /// Идентификатор зала
    /// </summary>
    public int GymHallId { get; init; }
    
    /// <summary>
    /// Навигационное свойство зала
    /// </summary>
    public required GymHallDto GymHall { get; init; }
    
    /// <summary>
    /// Дата и время занятия
    /// </summary>
    public DateTime DateTime { get; init; }
    
    /// <summary>
    /// Дата и время окончания занятия
    /// </summary>
    public TimeSpan Duration { get; init; }
    
    /// <summary>
    /// Пробное занятие или нет
    /// </summary>
    public bool IsTrial { get; init; }
}