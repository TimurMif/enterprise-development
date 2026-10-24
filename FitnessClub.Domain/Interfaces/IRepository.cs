namespace FitnessClub.Domain.Interfaces;

/// <summary>
/// Универсальный интерфейс репозитория
/// </summary>
public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
}