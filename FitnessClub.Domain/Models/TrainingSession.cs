using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Domain.Models;

/// <summary>
/// Представляет запись клиента на занятие с тренером в зале.
/// </summary>
[Table("training_sessions")]
public class TrainingSession
{
    /// <summary>
    /// Уникальный идентификатор занятия
    /// </summary>
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор записанного клиента
    /// </summary>
    [Column("client_id")]
    public int ClientId { get; set; }

    /// <summary>
    /// Навигационное свойство клиента
    /// </summary>
    public Client? Client { get; set; }

    /// <summary>
    /// Идентификатор проводящего занятие тренера
    /// </summary>
    [Column("trainer_id")]
    public int TrainerId { get; set; }

    /// <summary>
    /// Навигационное свойство тренера
    /// </summary>
    public Trainer? Trainer { get; set; }

    /// <summary>
    /// Идентификатор зала, в котором проходит занятие
    /// </summary>
    [Column("gym_hall_id")]
    public int GymHallId { get; set; }

    /// <summary>
    /// Навигационное свойство спортивного зала
    /// </summary>
    public GymHall? GymHall { get; set; }

    /// <summary>
    /// Дата и время проведения занятия
    /// </summary>
    [Column("date_start")]
    public DateTime DateTime { get; set; }

    /// <summary>
    /// Продолжительность занятия
    /// </summary>
    [Column("duration")]
    public TimeSpan Duration { get; set; } = TimeSpan.FromHours(1);

    /// <summary>
    /// Является ли занятие пробным
    /// </summary>
    [Column("trial")]
    public bool IsTrial { get; set; }
}