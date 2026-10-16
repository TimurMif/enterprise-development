using System.ComponentModel.DataAnnotations;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

public record TrainerEditDto
{
    [Required(ErrorMessage = "Номер паспорта обязателен")]
    public required string PassportNumber { get; init; }

    [Required(ErrorMessage = "Имя обязательно")]
    public required string FirstName { get; init; }

    [Required(ErrorMessage = "Фамилия обязательна")]
    public required string LastName { get; init; }

    public string? MiddleName { get; init; }

    [EnumDataType(typeof(Gender))] 
    public Gender Gender { get; init; }

    public DateOnly DateOfBirth { get; init; }

    [Required(ErrorMessage = "Номер телефона обязателен")]
    [Phone(ErrorMessage = "Некорректный формат телефона")]
    public required string PhoneNumber { get; init; }

    [Range(0, 60, ErrorMessage = "Стаж должен быть от 0 до 60 лет")]
    public int ExperienceYears { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "Некорректный ID специализации")]
    public int SpecializationId { get; init; }
}