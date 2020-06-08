using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Searchfight.Application.Analytics;
using Searchfight.Core.Domain;

namespace Searchfight.Application.Tests
{
    [TestFixture]
    public class BySearchTermAnalyticsServiceTests
    {
        private BySearchTermAnalyticsService _bySearchTermAnalyticsService;

        private static readonly List<List<SearchResults>> _searchResults = new List<List<SearchResults>>
        {
            new List<SearchResults>
            {
                new SearchResults(SearchEngine.Google, ".net", 90_000_000),
                new SearchResults(SearchEngine.Bing, "java", 200_000),
                new SearchResults(SearchEngine.Google, "java", 70_000_000),
                new SearchResults(SearchEngine.Google, "c++", 190_000_000),
                new SearchResults(SearchEngine.Bing, "python", 300_000),
                new SearchResults(SearchEngine.Bing, ".net", 100_000),
                new SearchResults(SearchEngine.Bing, "c++", 400_000),
                new SearchResults(SearchEngine.Google, "python", 190_000_000),
            },
        };

        [SetUp]
        public void SetUp()
        {
            _bySearchTermAnalyticsService = new BySearchTermAnalyticsService();
        }

        [Test]
        [TestCaseSource(nameof(_searchResults))]
        public void Search_Engines_Are_In_Same_Order_For_All_Search_Terms(List<SearchResults> searchResults)
        {
            var report = _bySearchTermAnalyticsService.GetReport(searchResults);

            var searchTermsEngines = report.Select(r => r.SearchEngineResults.Select(se => se.SearchEngine)).ToArray();

            Assert.That(searchTermsEngines[0], 
                Is.EqualTo(searchTermsEngines[1])
                    .And.EqualTo(searchTermsEngines[2])
                    .And.EqualTo(searchTermsEngines[3]));
        }
    }
}
