using System.Collections.Generic;
using System.Threading.Tasks;

namespace OzeContract.Databases
{
    public interface IMongoContext
    {
        IMongoContext Set(string database, string collection);
        Task<List<T>> GetAll<T>();
        Task<T> GetSingle<T>(string objectId) where T : new();
        Task Add<T>(T item);
        Task Remove<T>(string objectId);
        Task Update<T>(string objectId,T device);
    }
}