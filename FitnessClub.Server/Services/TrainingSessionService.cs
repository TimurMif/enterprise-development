using FitnessClub.Domain.Context;
using FitnessClub.Domain.Models;

namespace FitnessClub.Server.Services;

/// <summary>
/// Сервис для выполнения операций с тренировками
/// </summary>
public class TrainingSessionService : ITrainingSessionService
{
    private readonly FitnessClubContext _context;

    public TrainingSessionService(FitnessClubContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Возвращает список всех тренировок
    /// </summary>
    public List<TrainingSession> GetAll()
    {
        return _context.TrainingSessions;
    }

    /// <summary>
    /// Возвращает тренировку по Id
    /// </summary>
    public TrainingSession? GetById(int id)
    {
        return _context.TrainingSessions.FirstOrDefault(s => s.Id == id);
    }

    /// <summary>
    /// Создает новую тренировку
    /// </summary>
    public TrainingSession? Create(TrainingSession session)
    {
        var client = _context.Clients.FirstOrDefault(c => c.Id == session.ClientId);
        var trainer = _context.Trainers.FirstOrDefault(t => t.Id == session.TrainerId);
        var gymHall = _context.GymHalls.FirstOrDefault(h => h.Id == session.GymHallId);

        if (client == null || trainer == null || gymHall == null) return null;

        session.Id = _context.TrainingSessions.Any() ? _context.TrainingSessions.Max(s => s.Id) + 1 : 1;
        session.Client = client;
        session.Trainer = trainer;
        session.GymHall = gymHall;

        _context.TrainingSessions.Add(session);
        return session;
    }
    
    /// <summary>
    /// Обновляет сведения о тренировке по ее Id
    /// </summary>
    public bool Update(int id, TrainingSession updatedSession)
    {
        var session = _context.TrainingSessions.FirstOrDefault(s => s.Id == id);
        if (session == null) return false;

        var client = _context.Clients.FirstOrDefault(c => c.Id == updatedSession.ClientId);
        var trainer = _context.Trainers.FirstOrDefault(t => t.Id == updatedSession.TrainerId);
        var gymHall = _context.GymHalls.FirstOrDefault(h => h.Id == updatedSession.GymHallId);

        if (client == null || trainer == null || gymHall == null) return false;

        session.ClientId = updatedSession.ClientId;
        session.Client = client;
        session.TrainerId = updatedSession.TrainerId;
        session.Trainer = trainer;
        session.GymHallId = updatedSession.GymHallId;
        session.GymHall = gymHall;
        session.DateTime = updatedSession.DateTime;
        session.Duration = updatedSession.Duration;
        session.IsTrial = updatedSession.IsTrial;

        return true;
    }

    /// <summary>
    /// Удаляет тренировку по ее Id
    /// </summary>
    public bool Delete(int id)
    {
        var session = _context.TrainingSessions.FirstOrDefault(s => s.Id == id);
        if (session == null) return false;

        _context.TrainingSessions.Remove(session);
        return true;
    }
}