namespace Searchfight.Infrastructure.SearchClients.Bing
{
    internal class BingSearchResult : ISearchResult
    {
        public WebPagesResult WebPages { get; set; }

        internal class WebPagesResult
        {
            public long TotalEstimatedMatches { get; set; }
        }

        public long TotalResults => WebPages.TotalEstimatedMatches;
    }
}