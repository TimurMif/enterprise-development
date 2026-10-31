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
        return session?.ToDto();
    }

    /// <summary>
    /// Создает новую тренировочную сессию
    /// </summary>
    public async Task<TrainingSessionDto?> CreateAsync(TrainingSessionEditDto dto)
    {
        var client = await clientRepository.GetByIdAsync(dto.ClientId);
        var trainer = await trainerRepository.GetByIdAsync(dto.TrainerId);
        var gymHall = await gymHallRepository.GetByIdAsync(dto.GymHallId);
        if (client == null || trainer == null || gymHall == null)
            return null;

        var created = await sessionRepository.AddAsync(dto.ToEntity(client, trainer, gymHall));
        return created.ToDto();
    }

    /// <summary>
    /// Обновляет данные существующей тренировки по ее Id
    /// </summary>
    public async Task<bool> UpdateAsync(int id, TrainingSessionEditDto updatedDto)
    {
        var client = await clientRepository.GetByIdAsync(updatedDto.ClientId);
        var trainer = await trainerRepository.GetByIdAsync(updatedDto.TrainerId);
        var gymHall = await gymHallRepository.GetByIdAsync(updatedDto.GymHallId);

        if (client == null || trainer == null || gymHall == null)
            return false;

        var session = updatedDto.ToEntity(client, trainer, gymHall);
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
}