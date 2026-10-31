using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;
using FitnessClub.Application.Mapping;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис для работы с тренировочными сессиями фитнес-клуба
/// </summary>
public class TrainingSessionService(
    IRepository<TrainingSession> sessionRepository, 
    IRepository<Client> clientRepository,
    IRepository<Trainer> trainerRepository,
    IRepository<GymHall> gymHallRepository
    ) : ITrainingSessionService
{
    /// <summary>
    /// Возвращает список всех тренировочных сессий
    /// </summary>
    public async Task<List<TrainingSessionDto>> GetAllAsync()
    {
        var sessions = await sessionRepository.GetAllAsync();
        return sessions.Select(s => s.ToDto()).ToList();
    }

    /// <summary>
    /// Возвращает информацию о тренировке по ее Id
    /// </summary>
    public async Task<TrainingSessionDto?> GetByIdAsync(int id)
    {
        var session = await sessionRepository.GetByIdAsync(id);
        return session == null ? null : session.ToDto();
    }

    /// <summary>
    /// Создает новую тренировочную сессию
    /// </summary>
    public async Task<TrainingSessionDto?> CreateAsync(TrainingSessionEditDto dto)
    {
        var session = await BuildSessionAsync(dto);
        if (session == null) return null;

        var created = await sessionRepository.AddAsync(session);
        return created.ToDto();
    }

    /// <summary>
    /// Обновляет данные существующей тренировки по ее Id
    /// </summary>
    public async Task<bool> UpdateAsync(int id, TrainingSessionEditDto updatedDto)
    {
        var session = await BuildSessionAsync(updatedDto);
        if (session == null) return false;

        session.Id = id;
        return await sessionRepository.UpdateAsync(session);
    }

    /// <summary>
    /// Удаляет тренировочную сессию по ее Id
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        return sessionRepository.DeleteAsync(id);
    }

    /// <summary>
    /// Собирает доменную модель сессии, находя клиента, тренера и зал по Id.
    /// </summary>
    private async Task<TrainingSession?> BuildSessionAsync(TrainingSessionEditDto dto)
    {
        var client = await clientRepository.GetByIdAsync(dto.ClientId);
        var trainer = await trainerRepository.GetByIdAsync(dto.TrainerId);
        var gymHall = await gymHallRepository.GetByIdAsync(dto.GymHallId);

        if (client == null || trainer == null || gymHall == null) return null;

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