namespace Searchfight.Infrastructure.SearchClients.Google
{
    internal class GoogleSearchResult : ISearchResult
    {
        internal class SearchInformationResult
        {
            public string TotalResults { get; set; }
        }

        public SearchInformationResult SearchInformation { get; set; }

        public long TotalResults => long.Parse(SearchInformation.TotalResults);
    }
}