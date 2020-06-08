namespace Searchfight.Infrastructure.SearchClients.Google
{
    internal class GoogleSearchResult
    {
        internal class SearchInformationResult
        {
            public string TotalResults { get; set; }
        }

        public SearchInformationResult SearchInformation { get; set; }
    }
}