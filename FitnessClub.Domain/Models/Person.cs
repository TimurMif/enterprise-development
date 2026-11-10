using System.ComponentModel.DataAnnotations.Schema;
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
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Паспорт
    /// </summary>
    [Column("passport_number")]
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    [Column("first_name")]
    public required string FirstName { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    [Column("last_name")]
    public required string LastName { get; set; }

    /// <summary>
    /// Отчество (необязательное)
    /// </summary>
    [Column("middle_name")]
    public string? MiddleName { get; set; }

    /// <summary>
    /// Пол
    /// </summary>
    [Column("gender")]
    public Gender Gender { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    [Column("date_of_birth")]
    public DateOnly DateOfBirth { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    [Column("phone_number")]
    public required string PhoneNumber { get; set; }
}