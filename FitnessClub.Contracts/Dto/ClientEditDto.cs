using System.ComponentModel.DataAnnotations;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// Объект Dto для редактирования клиента
/// </summary>
public record ClientEditDto
{
    /// <summary>
    /// Номер паспорта
    /// </summary>
    [Required(ErrorMessage = "Номер паспорта обязателен")]
    public required string PassportNumber { get; init; }
    
    /// <summary>
    /// Имя
    /// </summary>
    [Required(ErrorMessage = "Имя обязательно")]
    public required string FirstName { get; init; }
    
    /// <summary>
    /// Фамилия
    /// </summary>
    [Required(ErrorMessage = "Фамилия обязательна")]
    public required string LastName { get; init; }
    
    /// <summary>
    /// Отчество
    /// </summary>
    public string? MiddleName { get; init; }

    /// <summary>
    /// Пол клиента
    /// </summary>
    [EnumDataType(typeof(Gender), ErrorMessage = "Некорректное значение пола")]
    public Gender Gender { get; init; }
    
    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateOnly DateOfBirth { get; init; }
    
    /// <summary>
    /// Номер телефона
    /// </summary>
    [Required(ErrorMessage = "Номер телефона обязателен")]
    [Phone(ErrorMessage = "Некорректный формат телефона")]
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