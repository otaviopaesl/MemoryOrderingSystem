using System.Collections.Generic;
using System.Threading.Tasks;

namespace MemoryOrderingSystem.Services.Interfaces
{
    public interface IServiceBase<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T> Get(int id);
        Task<bool> Exists(int id);
        Task Insert(T obj);
        Task Remove(T obj);
        Task Update(T obj);
    }
}
