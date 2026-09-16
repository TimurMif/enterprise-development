namespace FitnessClub.Domain.Models;

/// <summary>
///     Представляет запись клиента на занятие с тренером в зале.
/// </summary>
public class TrainingSession
{
    /// <summary>
    ///     Уникальный идентификатор занятия
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    ///     Идентификатор записанного клиента
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    ///     Навигационное свойство клиента
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    ///     Идентификатор проводящего занятие тренера
    /// </summary>
    public Guid TrainerId { get; set; }

    /// <summary>
    ///     Навигационное свойство тренера
    /// </summary>
    public required Trainer Trainer { get; set; }

    /// <summary>
    ///     Идентификатор зала, в котором проходит занятие
    /// </summary>
    public Guid GymHallId { get; set; }

    /// <summary>
    ///     Навигационное свойство спортивного зала
    /// </summary>
    public required GymHall GymHall { get; set; }

    /// <summary>
    ///     Дата и время проведения занятия
    /// </summary>
    public DateTime DateTime { get; set; }

    /// <summary>
    ///     Продолжительность занятия
    /// </summary>
    public TimeSpan Duration { get; set; } = TimeSpan.FromHours(1);

    /// <summary>
    ///     Является ли занятие пробным
    /// </summary>
    public bool IsTrial { get; set; }
}