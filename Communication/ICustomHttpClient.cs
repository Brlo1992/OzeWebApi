using System.Threading.Tasks;

namespace OzeContract.Communication
{
    public interface ICustomHttpClient
    {
        Task<Response<T>> Get<T>(Request request) where T : class;
    }
}