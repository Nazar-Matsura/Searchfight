using System.Threading.Tasks;

namespace Searchfight.Infrastructure.SearchClients
{
    public interface ISearchClient
    {
        Task<long> CountResults(string searchTerm);
    }
}