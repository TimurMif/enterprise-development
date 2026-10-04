using FitnessClub.Domain.Models;

namespace FitnessClub.Server.Services;

/// <summary>
/// Интерфейс сервиса для тренеров
/// </summary>
public interface ITrainerService
{
    List<Trainer> GetAll();
    Trainer? GetById(int id);
    Trainer? Create(Trainer trainer);
    bool Update(int id, Trainer trainer);
    bool Delete(int id);
}