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
    /// Преобразует объект Dto в доменную модель клиента
    /// </summary>
    public static Client ToEntity(this ClientEditDto dto)
    {
        return new Client
        {
            PassportNumber = dto.PassportNumber,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MiddleName = dto.MiddleName,
            Gender = dto.Gender,
            DateOfBirth = dto.DateOfBirth,
            PhoneNumber = dto.PhoneNumber,
            SubscriptionStartDate = dto.SubscriptionStartDate,
            SubscriptionEndDate = dto.SubscriptionEndDate
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
    /// Преобразует объект Dto в доменную модель специализации
    /// </summary>
    public static Specialization ToEntity(this SpecializationEditDto dto)
    {
        return new Specialization
        {
            Name = dto.Name
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
    /// Преобразует Dto в доменную модель тренера
    /// </summary>
    public static Trainer ToEntity(this TrainerEditDto dto, Specialization spec)
    {
        return new Trainer
        {
            PassportNumber = dto.PassportNumber,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MiddleName = dto.MiddleName,
            Gender = dto.Gender,
            DateOfBirth = dto.DateOfBirth,
            PhoneNumber = dto.PhoneNumber,
            ExperienceYears = dto.ExperienceYears,
            SpecializationId = spec.Id,
            Specialization = spec
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
    /// Преобразует Dto в доменную модель зала
    /// </summary>
    public static GymHall ToEntity(this GymHallEditDto dto)
    {
        return new GymHall
        {
            Name = dto.Name,
            Capacity = dto.Capacity
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

    /// <summary>
    /// Преобразует Dto в доменную модель тренировок
    /// </summary>
    public static TrainingSession ToEntity(this TrainingSessionEditDto dto, Client client, 
        Trainer trainer, GymHall gymHall)
    {
        return new TrainingSession
        {
            ClientId = client.Id,
            Client = client,
            TrainerId = trainer.Id,
            Trainer = trainer,
            GymHallId = gymHall.Id,
            GymHall = gymHall,
            DateTime = dto.DateTime,
            Duration = dto.Duration,
            IsTrial = dto.IsTrial
        };
    }
}