using FitnessClub.Domain.Context;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

public class GymHallRepository(FitnessClubContext context) : IRepository<GymHall>
{
    /// <summary>
    /// Возвращает список всех залов
    /// </summary>
    public Task<List<GymHall>> GetAllAsync()
    {
        return Task.FromResult(context.GymHalls.ToList());
    }

    /// <summary>
    /// Возвращает зал по его Id
    /// </summary>
    public Task<GymHall?> GetByIdAsync(int id)
    {
        return Task.FromResult(context.GymHalls.FirstOrDefault(h => h.Id == id));
    }

    /// <summary>
    /// Добавляет новый зал и присваивает ему Id
    /// </summary>
    public Task<GymHall> AddAsync(GymHall entity)
    {
        entity.Id = context.GymHalls.Select(h => h.Id).DefaultIfEmpty(0).Max() + 1;
        context.GymHalls.Add(entity);

        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновляет данные существующего зала
    /// </summary>
    public Task<bool> UpdateAsync(GymHall entity)
    {
        var existing = context.GymHalls.FirstOrDefault(h => h.Id == entity.Id);
        if (existing == null) return Task.FromResult(false);

        existing.Name = entity.Name;
        existing.Capacity = entity.Capacity;

        return Task.FromResult(true);
    }

    /// <summary>
    /// Удаляет зал по его Id вместе с записями на занятия в нём
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var entity = context.GymHalls.FirstOrDefault(h => h.Id == id);
        if (entity == null) return Task.FromResult(false);

        context.TrainingSessions.RemoveAll(s => s.GymHallId == id);
        context.GymHalls.Remove(entity);

        return Task.FromResult(true);
    }
}