using System.ComponentModel.DataAnnotations;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

public record ClientEditDto
{
    [Required(ErrorMessage = "Номер паспорта обязателен")]
    public required string PassportNumber { get; init; }

    [Required(ErrorMessage = "Имя обязательно")]
    public required string FirstName { get; init; }

    [Required(ErrorMessage = "Фамилия обязательна")]
    public required string LastName { get; init; }

    public string? MiddleName { get; init; }

    [EnumDataType(typeof(Gender), ErrorMessage = "Некорректное значение пола")]
    public Gender Gender { get; init; }

    public DateOnly DateOfBirth { get; init; }

    [Required(ErrorMessage = "Номер телефона обязателен")]
    [Phone(ErrorMessage = "Некорректный формат телефона")]
    public required string PhoneNumber { get; init; }

    public DateOnly SubscriptionStartDate { get; init; }
    public DateOnly SubscriptionEndDate { get; init; }
}