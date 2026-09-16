using FitnessClub.Domain.Models;

namespace FitnessClub.Domain.Context;

/// <summary>
///     Контекст данных фитнес-клуба.
/// </summary>
public class FitnessClubContext
{
    /// <summary>
    ///     Список клиентов клуба.
    /// </summary>
    public List<Client> Clients { get; set; } = [];

    /// <summary>
    ///     Список тренеров клуба.
    /// </summary>
    public List<Trainer> Trainers { get; set; } = [];

    /// <summary>
    ///     Справочник специализаций.
    /// </summary>
    public List<Specialization> Specializations { get; set; } = [];

    /// <summary>
    ///     Список спортивных залов.
    /// </summary>
    public List<GymHall> GymHalls { get; set; } = [];

    /// <summary>
    ///     Список записей на тренировочные занятия.
    /// </summary>
    public List<TrainingSession> TrainingSessions { get; set; } = [];
}