using System.Threading.Tasks;

namespace Searchfight.Infrastructure
{
    public interface ISearchClient
    {
        Task<long> CountResults(string searchTerm);
    }
}