namespace Cinema.Core.Contracts.Common
{
    public interface ICinemaService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int? id);
        Task DeleteByIdAsync(int? id);
        Task<bool> ExistsByIdAsync(int? id);
    }
}
