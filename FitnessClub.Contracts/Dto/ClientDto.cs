using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// Класс для Dto Клиента
/// </summary>
public record ClientDto
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; init; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; init; }
    
    /// <summary>
    /// Фамилия
    /// </summary>
    public required string LastName { get; init; }
    
    /// <summary>
    /// Отчество
    /// </summary>
    public string? MiddleName { get; init; }
    
    /// <summary>
    /// Пол клиента
    /// </summary>
    public Gender Gender { get; init; }
    
    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateOnly DateOfBirth { get; init; }
    
    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string PhoneNumber { get; init; }
    
    /// <summary>
    /// Дата начала абонемента
    /// </summary>
    public DateOnly SubscriptionStartDate { get; init; }
    
    /// <summary>
    /// Дата окончания абонемента
    /// </summary>
    public DateOnly SubscriptionEndDate { get; init; }
}