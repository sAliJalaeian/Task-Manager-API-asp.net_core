using System.Linq.Expressions;
using TaskManager.API.Model.Domain;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.API.Repositories.Interface;
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<List<T>> GetFilteredAsync(Expression<Func<T, bool>>[] filters, int? skip, int? take, params Expression<Func<T, object>>[] includes);
    Task<List<T>> GetAsync(int? skip, int? take, params Expression<Func<T, object>>[] includes);
    Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
    Task<int> InsertAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    void DeleteEntities(List<T> entities);
    Task SaveChangesAsync();
}
