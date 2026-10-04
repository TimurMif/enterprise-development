using FitnessClub.Domain.Context;
using FitnessClub.Domain.Models;

namespace FitnessClub.Server.Services;

/// <summary>
/// Сервис для выполнения операций с тренерами
/// </summary>
public class TrainerService : ITrainerService
{
    private readonly FitnessClubContext _context;

    public TrainerService(FitnessClubContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Возвращает список всех тренеров
    /// </summary>
    public List<Trainer> GetAll()
    {
        return _context.Trainers;
    }

    /// <summary>
    /// Возвращает тренера по Id
    /// </summary>
    public Trainer? GetById(int id)
    {
        return _context.Trainers.FirstOrDefault(t => t.Id == id);
    }

    /// <summary>
    /// Добавляет нового тренера
    /// </summary>
    public Trainer? Create(Trainer trainer)
    {
        var spec = _context.Specializations.FirstOrDefault(s => s.Id == trainer.SpecializationId);
        if (spec == null) return null;

        trainer.Id = _context.Trainers.Any() ? _context.Trainers.Max(t => t.Id) + 1 : 1;
        trainer.Specialization = spec;

        _context.Trainers.Add(trainer);
        return trainer;
    }

    /// <summary>
    /// Обновляет сведения о тренере по его Id
    /// </summary>
    public bool Update(int id, Trainer updatedTrainer)
    {
        var trainer = _context.Trainers.FirstOrDefault(t => t.Id == id);
        if (trainer == null) return false;

        var spec = _context.Specializations.FirstOrDefault(s => s.Id == updatedTrainer.SpecializationId);
        if (spec == null) return false;

        trainer.PassportNumber = updatedTrainer.PassportNumber;
        trainer.FirstName = updatedTrainer.FirstName;
        trainer.LastName = updatedTrainer.LastName;
        trainer.MiddleName = updatedTrainer.MiddleName;
        trainer.Gender = updatedTrainer.Gender;
        trainer.DateOfBirth = updatedTrainer.DateOfBirth;
        trainer.PhoneNumber = updatedTrainer.PhoneNumber;
        trainer.ExperienceYears = updatedTrainer.ExperienceYears;
        trainer.SpecializationId = updatedTrainer.SpecializationId;
        trainer.Specialization = spec;

        return true;
    }

    /// <summary>
    /// Удаляет тренера по его Id
    /// </summary>
    public bool Delete(int id)
    {
        var trainer = _context.Trainers.FirstOrDefault(t => t.Id == id);
        if (trainer == null) return false;
        
        _context.TrainingSessions.RemoveAll(s => s.TrainerId == id);

        _context.Trainers.Remove(trainer);
        return true;
    }
}