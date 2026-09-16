namespace FitnessClub.Domain.Models;

/// <summary>
///     Представляет информацию о клиенте фитнес-клуба
/// </summary>
public class Client : Person
{
    /// <summary>
    ///     Дата начала абонемента
    /// </summary>
    public DateOnly SubscriptionStartDate { get; set; }

    /// <summary>
    ///     Дата окончания абонемента
    /// </summary>
    public DateOnly SubscriptionEndDate { get; set; }
}