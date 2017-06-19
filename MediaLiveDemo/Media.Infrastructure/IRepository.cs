using System.Collections.Generic;
using System.Threading.Tasks;

namespace Media.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        int Delete(string id);
        //int Delete(T entity);
        //Task<int> DeleteAsync(T entity);
        int Insert(T entity);
        //Task<int> InsertAsync(T entity);
        int Insert(IEnumerable<T> entities);
        int Update(T entity);
        //Task<int> UpdateAsync(T entity);
        int Count();
        //Task<int> CountAsync();
        IEnumerable<T> QueryAll();
        //Task<IEnumerable<T>> QueryAllAsync();
        T GetEntity(string id);
    }
}
