using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfflineSyncMobileApp.Interfaces
{
    public interface IRestService<T, U>
        where T : class
        where U : class
    {

        Task<IRestResponse<IEnumerable<U>>> GetAll(string uri);

        Task<IRestResponse<U>> Get(int id, string uri);

        Task<IRestResponse<U>> Post(T content, string uri);


    }
}
