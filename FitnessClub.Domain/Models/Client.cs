using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.Domain.Models;

/// <summary>
/// Представляет информацию о клиенте фитнес-клуба
/// </summary>
[Table("clients")]
public class Client : Person
{
    /// <summary>
    /// Дата начала абонемента
    /// </summary>
    [Column("subscription_start_date")]
    public DateOnly SubscriptionStartDate { get; set; }

    /// <summary>
    /// Дата окончания абонемента
    /// </summary>
    [Column("subscription_end_date")]
    public DateOnly SubscriptionEndDate { get; set; }
}