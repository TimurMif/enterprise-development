namespace FitnessClub.Domain.Interfaces;

/// <summary>
/// Универсальный интерфейс репозитория
/// </summary>
public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(int id);
}