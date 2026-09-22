using FitnessClub.Domain.Enums;

namespace FitnessClub.Domain.Models;

/// <summary>
/// Базовый класс для физических лиц (Клиент, Тренер)
/// </summary>
public abstract class Person
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Паспорт
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Отчество (необязательное)
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Пол
    /// </summary>
    public Gender Gender { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateOnly DateOfBirth { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string PhoneNumber { get; set; }
}