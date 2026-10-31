namespace FitnessClub.Domain.Interfaces;

/// <summary>
/// Универсальный интерфейс репозитория
/// </summary>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Получение списка всех сущностей типа T
    /// </summary>
    public Task<List<T>> GetAllAsync();
    /// <summary>
    /// Возвращает сущность по ее Id
    /// </summary>
    public Task<T?> GetByIdAsync(int id);
    /// <summary>
    /// Добавление новой сущности
    /// </summary>
    public Task<T> AddAsync(T entity);
    /// <summary>
    /// Обновляет существующую сущность
    /// </summary>
    public Task<bool> UpdateAsync(T entity);
    /// <summary>
    /// Удаляет сущность по ее Id
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}