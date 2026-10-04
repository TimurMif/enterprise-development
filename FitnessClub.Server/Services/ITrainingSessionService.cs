using FitnessClub.Domain.Models;

namespace FitnessClub.Server.Services;

/// <summary>
/// Интерфейс сервиса для тренировок
/// </summary>
public interface ITrainingSessionService
{
    List<TrainingSession> GetAll();
    TrainingSession? GetById(int id);
    TrainingSession? Create(TrainingSession session);
    bool Delete(int id);
    bool Update(int id, TrainingSession updatedSession);
}