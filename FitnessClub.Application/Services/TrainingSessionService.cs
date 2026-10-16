using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Context;
using FitnessClub.Domain.Models;

namespace FitnessClub.Application.Services;

public class TrainingSessionService : ITrainingSessionService
{
    private readonly FitnessClubContext _context;

    public TrainingSessionService(FitnessClubContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Возвращает список всех тренировочных сессий
    /// </summary>
    public Task<List<TrainingSessionDto>> GetAllAsync()
    {
        var dtos = _context.TrainingSessions.Select(MapToDto).ToList();
        return Task.FromResult(dtos);
    }

    /// <summary>
    /// Возвращает информацию о тренировке по ее Id
    /// </summary>
    public Task<TrainingSessionDto?> GetByIdAsync(int id)
    {
        var session = _context.TrainingSessions.FirstOrDefault(s => s.Id == id);
        return Task.FromResult(session == null ? null : MapToDto(session));
    }

    /// <summary>
    /// Создает новую тренировочную сессию на основе переданных данных
    /// </summary>
    public Task<TrainingSessionDto?> CreateAsync(TrainingSessionEditDto dto)
    {
        var client = _context.Clients.FirstOrDefault(c => c.Id == dto.ClientId);
        var trainer = _context.Trainers.FirstOrDefault(t => t.Id == dto.TrainerId);
        var gymHall = _context.GymHalls.FirstOrDefault(h => h.Id == dto.GymHallId);

        if (client == null || trainer == null || gymHall == null) return Task.FromResult<TrainingSessionDto?>(null);

        var newId = _context.TrainingSessions.Any() ? _context.TrainingSessions.Max(s => s.Id) + 1 : 1;
        var session = new TrainingSession
        {
            Id = newId,
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

        _context.TrainingSessions.Add(session);
        return Task.FromResult<TrainingSessionDto?>(MapToDto(session));
    }

    /// <summary>
    /// Обновляет данные существующей тренировки по ее Id
    /// </summary>
    public Task<bool> UpdateAsync(int id, TrainingSessionEditDto updatedDto)
    {
        var session = _context.TrainingSessions.FirstOrDefault(s => s.Id == id);
        if (session == null) return Task.FromResult(false);

        var client = _context.Clients.FirstOrDefault(c => c.Id == updatedDto.ClientId);
        var trainer = _context.Trainers.FirstOrDefault(t => t.Id == updatedDto.TrainerId);
        var gymHall = _context.GymHalls.FirstOrDefault(h => h.Id == updatedDto.GymHallId);

        if (client == null || trainer == null || gymHall == null) return Task.FromResult(false);

        session.ClientId = updatedDto.ClientId;
        session.Client = client;
        session.TrainerId = updatedDto.TrainerId;
        session.Trainer = trainer;
        session.GymHallId = updatedDto.GymHallId;
        session.GymHall = gymHall;
        session.DateTime = updatedDto.DateTime;
        session.Duration = updatedDto.Duration;
        session.IsTrial = updatedDto.IsTrial;

        return Task.FromResult(true);
    }

    /// <summary>
    /// Удаляет тренировочную сессию по ее Id
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var session = _context.TrainingSessions.FirstOrDefault(s => s.Id == id);
        if (session == null) return Task.FromResult(false);

        _context.TrainingSessions.Remove(session);
        return Task.FromResult(true);
    }

    /// <summary>
    /// Преобразует доменную модель тренировки в объект передачи данных DTO
    /// </summary>
    private static TrainingSessionDto MapToDto(TrainingSession s)
    {
        return new TrainingSessionDto
        {
            Id = s.Id,
            ClientId = s.ClientId,
            Client = new ClientDto
            {
                Id = s.Client.Id,
                PassportNumber = s.Client.PassportNumber,
                FirstName = s.Client.FirstName,
                LastName = s.Client.LastName,
                MiddleName = s.Client.MiddleName,
                Gender = s.Client.Gender,
                DateOfBirth = s.Client.DateOfBirth,
                PhoneNumber = s.Client.PhoneNumber,
                SubscriptionStartDate = s.Client.SubscriptionStartDate,
                SubscriptionEndDate = s.Client.SubscriptionEndDate
            },
            TrainerId = s.TrainerId,
            Trainer = new TrainerDto
            {
                Id = s.Trainer.Id,
                PassportNumber = s.Trainer.PassportNumber,
                FirstName = s.Trainer.FirstName,
                LastName = s.Trainer.LastName,
                MiddleName = s.Trainer.MiddleName,
                Gender = s.Trainer.Gender,
                DateOfBirth = s.Trainer.DateOfBirth,
                PhoneNumber = s.Trainer.PhoneNumber,
                ExperienceYears = s.Trainer.ExperienceYears,
                SpecializationId = s.Trainer.SpecializationId,
                Specialization = new SpecializationDto
                {
                    Id = s.Trainer.Specialization.Id,
                    Name = s.Trainer.Specialization.Name
                }
            },
            GymHallId = s.GymHallId,
            GymHall = new GymHallDto
            {
                Id = s.GymHall.Id,
                Name = s.GymHall.Name,
                Capacity = s.GymHall.Capacity
            },
            DateTime = s.DateTime,
            Duration = s.Duration,
            IsTrial = s.IsTrial
        };
    }
}