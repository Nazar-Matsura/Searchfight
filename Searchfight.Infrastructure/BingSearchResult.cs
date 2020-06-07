namespace Searchfight.Infrastructure
{
    internal class BingSearchResult
    {
        public WebPagesResult WebPages { get; set; }

        internal class WebPagesResult
        {
            public long TotalEstimatedMatches { get; set; }
        }
    }
}