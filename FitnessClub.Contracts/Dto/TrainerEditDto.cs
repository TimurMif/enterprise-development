using System.ComponentModel.DataAnnotations;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// Объект Dto для редактирования тренеров
/// </summary>
public record TrainerEditDto
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
    /// Пол тренера
    /// </summary>
    [EnumDataType(typeof(Gender))] 
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
    /// Стаж тренера
    /// </summary>
    [Range(0, 60, ErrorMessage = "Стаж должен быть от 0 до 60 лет")]
    public int ExperienceYears { get; init; }

    /// <summary>
    /// Идентификатор специализации
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Некорректный ID специализации")]
    public int SpecializationId { get; init; }
}