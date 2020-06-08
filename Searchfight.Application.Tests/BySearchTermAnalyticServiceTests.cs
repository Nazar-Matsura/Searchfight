using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Searchfight.Application.Analytics;
using Searchfight.Core.Domain;

namespace Searchfight.Application.Tests
{
    [TestFixture]
    public class WinnerAnalyticsServiceTests
    {
        private WinnerAnalyticsService _winnerAnalyticsService;

        private static readonly List<List<SearchResults>> _searchResults = new List<List<SearchResults>>
        {
            new List<SearchResults>
            {
                new SearchResults(SearchEngine.Google, ".net", 90_000_000),
                new SearchResults(SearchEngine.Google, "java", 70_000_000),
                new SearchResults(SearchEngine.Google, "python", 190_000_000),
                new SearchResults(SearchEngine.Google, "c++", 190_000_000),

                new SearchResults(SearchEngine.Bing, ".net", 100_000),
                new SearchResults(SearchEngine.Bing, "java", 200_000),
                new SearchResults(SearchEngine.Bing, "python", 300_000),
                new SearchResults(SearchEngine.Bing, "c++", 400_000),
            },
        };

        [SetUp]
        public void SetUp()
        {
            _winnerAnalyticsService = new WinnerAnalyticsService();
        }

        [Test]
        [TestCaseSource(nameof(_searchResults))]
        public void Search_Term_Winner_Has_Maximum_Results(List<SearchResults> searchResults)
        {
            var winner = _winnerAnalyticsService.GetWinnerSearchTerm(searchResults);

            Assert.That(winner, Is.AnyOf("python", "c++"));
        }

        [Test]
        [TestCaseSource(nameof(_searchResults))]
        public void Search_Term_Winner_Is_In_Given_Order(List<SearchResults> searchResults)
        {
            var winner = _winnerAnalyticsService.GetWinnerSearchTerm(searchResults);

            Assert.That(winner, Is.EqualTo("python"));
        }

        [Test]
        [TestCaseSource(nameof(_searchResults))]
        public void Engine_Winners_Have_Max_Results(List<SearchResults> searchResults)
        {
            var winners = _winnerAnalyticsService.GetSearchEngineWinners(searchResults);

            var googleWinner = winners.FirstOrDefault(w => w.SearchEngine == SearchEngine.Google)?.SearchTerm;
            var bingWinner = winners.FirstOrDefault(w => w.SearchEngine == SearchEngine.Bing)?.SearchTerm;

            Assert.That(googleWinner, Is.Not.Null.Or.Not.Empty);
            Assert.That(googleWinner, Is.AnyOf("python", "c++"));

            Assert.That(bingWinner, Is.Not.Null.Or.Not.Empty);
            Assert.That(bingWinner, Is.EqualTo("c++"));
        }
    }
}
