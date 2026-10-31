using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// Класс для Dto тренеров
/// </summary>
public record TrainerDto
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
    /// Пол тренера
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
    /// Опыт тренера
    /// </summary>
    public int ExperienceYears { get; init; }
    
    /// <summary>
    /// Идентификатор специализации
    /// </summary>
    public int SpecializationId { get; init; }
    
    /// <summary>
    /// Навигационное свойство специализации
    /// </summary>
    public required SpecializationDto Specialization { get; init; }
}