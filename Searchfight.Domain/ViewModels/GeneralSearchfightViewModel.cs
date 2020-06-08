using System.Collections.Generic;

namespace Searchfight.Core.ViewModels
{
    public class GeneralSearchfightViewModel
    {
        public GeneralSearchfightViewModel(List<SearchTermResults> resultsBySearchTerm,
            List<SearchEngineWinner> searchEngineWinners,
            string totalWinner)
        {
            ResultsBySearchTerm = resultsBySearchTerm;
            SearchEngineWinners = searchEngineWinners;
            TotalWinner = totalWinner;
        }

        public List<SearchTermResults> ResultsBySearchTerm { get; }

        public List<SearchEngineWinner> SearchEngineWinners { get; }

        public string TotalWinner { get; }
    }
}