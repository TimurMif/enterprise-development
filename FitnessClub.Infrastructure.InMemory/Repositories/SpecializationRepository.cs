using FitnessClub.Domain.Context;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

public class SpecializationRepository(FitnessClubContext context) : IRepository<Specialization>
{
    /// <summary>
    /// Возвращает список всех специализаций
    /// </summary>
    public Task<List<Specialization>> GetAllAsync()
    {
        return Task.FromResult(context.Specializations.ToList());
    }

    /// <summary>
    /// Возвращает специализацию по ее Id
    /// </summary>
    public Task<Specialization?> GetByIdAsync(int id)
    {
        return Task.FromResult(context.Specializations.FirstOrDefault(s => s.Id == id));
    }

    /// <summary>
    /// Добавляет новую специализацию и присваивает ей Id
    /// </summary>
    public Task<Specialization> AddAsync(Specialization entity)
    {
        entity.Id = context.Specializations.Select(s => s.Id).DefaultIfEmpty(0).Max() + 1;
        context.Specializations.Add(entity);

        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновляет данные существующей специализации
    /// </summary>
    public Task<bool> UpdateAsync(Specialization entity)
    {
        var existing = context.Specializations.FirstOrDefault(s => s.Id == entity.Id);
        if (existing == null) return Task.FromResult(false);

        existing.Name = entity.Name;

        return Task.FromResult(true);
    }

    /// <summary>
    /// Удаляет специализацию по ее Id. Возвращает false, если специализация не найдена или назначена хотя бы одному тренеру
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var entity = context.Specializations.FirstOrDefault(s => s.Id == id);
        if (entity == null) return Task.FromResult(false);

        if (context.Trainers.Any(t => t.SpecializationId == id)) return Task.FromResult(false);

        context.Specializations.Remove(entity);

        return Task.FromResult(true);
    }
}