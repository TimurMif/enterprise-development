using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Models;
using FitnessClub.Application.Mapping;

namespace FitnessClub.Application.Services;

/// <summary>
/// Сервис для работы со спортивными залами
/// </summary>
public class GymHallService(IRepository<GymHall> gymHallRepository) : IGymHallService
{
    /// <summary>
    /// Возвращает список всех залов фитнес-клуба
    /// </summary>
    public async Task<List<GymHallDto>> GetAllAsync()
    {
        var halls = await gymHallRepository.GetAllAsync();
        return halls.Select(h=>h.ToDto()).ToList();
    }

    /// <summary>
    /// Возвращает информацию о зале по его Id
    /// </summary>
    public async Task<GymHallDto?> GetByIdAsync(int id)
    {
        var hall = await gymHallRepository.GetByIdAsync(id);
        return hall == null ? null : hall.ToDto();
    }

    /// <summary>
    /// Создает новую запись зала на основе переданных данных
    /// </summary>
    public async Task<GymHallDto> CreateAsync(GymHallEditDto dto)
    {
        var created = await gymHallRepository.AddAsync(MapToEntity(dto));
        return created.ToDto();
    }

    /// <summary>
    /// Обновляет данные существующего зала по его Id
    /// </summary>
    public async Task<bool> UpdateAsync(int id, GymHallEditDto updatedDto)
    {
        var hall = MapToEntity(updatedDto);
        hall.Id = id;

        return await gymHallRepository.UpdateAsync(hall);
    }

    /// <summary>
    /// Удаляет зал по его Id вместе с сопутствующими сессиями
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        return gymHallRepository.DeleteAsync(id);
    }

    /// <summary>
    /// Преобразует DTO в доменную модель зала
    /// </summary>
    private static GymHall MapToEntity(GymHallEditDto dto)
    {
        return new GymHall
        {
            Name = dto.Name,
            Capacity = dto.Capacity
        };
    }
}