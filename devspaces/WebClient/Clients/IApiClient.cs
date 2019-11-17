using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Clients
{
    public interface IApiClient
    {
        Task<int> GetRandomValue();
    }
}
