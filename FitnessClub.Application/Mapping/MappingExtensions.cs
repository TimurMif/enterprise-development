using FitnessClub.Contracts.Dto;
using FitnessClub.Domain.Models;

namespace FitnessClub.Application.Mapping;

/// <summary>
/// Методы преобразования доменных моделей в объекты DTO
/// </summary>
public static class MappingExtensions
{
    /// <summary>
    /// Преобразует доменную модель клиента в объект DTO
    /// </summary>
    public static ClientDto ToDto(this Client c)
    {
        return new ClientDto
        {
            Id = c.Id,
            PassportNumber = c.PassportNumber,
            FirstName = c.FirstName,
            LastName = c.LastName,
            MiddleName = c.MiddleName,
            Gender = c.Gender,
            DateOfBirth = c.DateOfBirth,
            PhoneNumber = c.PhoneNumber,
            SubscriptionStartDate = c.SubscriptionStartDate,
            SubscriptionEndDate = c.SubscriptionEndDate
        };
    }

    /// <summary>
    /// Преобразует доменную модель специализации в объект DTO
    /// </summary>
    public static SpecializationDto ToDto(this Specialization s)
    {
        return new SpecializationDto
        {
            Id = s.Id,
            Name = s.Name
        };
    }

    /// <summary>
    /// Преобразует доменную модель тренера в объект DTO
    /// </summary>
    public static TrainerDto ToDto(this Trainer t)
    {
        return new TrainerDto
        {
            Id = t.Id,
            PassportNumber = t.PassportNumber,
            FirstName = t.FirstName,
            LastName = t.LastName,
            MiddleName = t.MiddleName,
            Gender = t.Gender,
            DateOfBirth = t.DateOfBirth,
            PhoneNumber = t.PhoneNumber,
            ExperienceYears = t.ExperienceYears,
            SpecializationId = t.SpecializationId,
            Specialization = t.Specialization.ToDto()
        };
    }

    /// <summary>
    /// Преобразует доменную модель зала в объект DTO
    /// </summary>
    public static GymHallDto ToDto(this GymHall h)
    {
        return new GymHallDto
        {
            Id = h.Id,
            Name = h.Name,
            Capacity = h.Capacity
        };
    }

    /// <summary>
    /// Преобразует доменную модель тренировки в объект DTO
    /// </summary>
    public static TrainingSessionDto ToDto(this TrainingSession s)
    {
        return new TrainingSessionDto
        {
            Id = s.Id,
            ClientId = s.ClientId,
            Client = s.Client.ToDto(),
            TrainerId = s.TrainerId,
            Trainer = s.Trainer.ToDto(),
            GymHallId = s.GymHallId,
            GymHall = s.GymHall.ToDto(),
            DateTime = s.DateTime,
            Duration = s.Duration,
            IsTrial = s.IsTrial
        };
    }
}