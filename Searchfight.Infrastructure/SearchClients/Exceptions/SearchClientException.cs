using System;
using System.Net.Http;
using Searchfight.Core.Domain;

namespace Searchfight.Infrastructure.SearchClients.Exceptions
{
    public class SearchClientException : Exception
    {
        private const string DefaultMessage = "Search client failed";

        public HttpResponseMessage FailedResponse { get; }

        public SearchEngine SourceSearchEngine { get; }

        public SearchClientException(
            SearchEngine sourceSearchEngine,
            HttpRequestException httpRequestException = null,
            HttpResponseMessage httpFailedResponse = null)
            :base(DefaultMessage, httpRequestException)
        {
            SourceSearchEngine = sourceSearchEngine;
        }
    }
}