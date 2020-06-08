using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Searchfight.Application;
using Searchfight.Core.Domain;

namespace Searchfight.Terminal
{
    public class App
    {
        private readonly ISearchfightService _searchfightService;
        private readonly IMergeQuotedService _mergeQuotedService;
        private const string Quote = "\"";

        public App(ISearchfightService searchfightService, 
            IMergeQuotedService mergeQuotedService)
        {
            _searchfightService = searchfightService;
            _mergeQuotedService = mergeQuotedService;
        }

        public async Task Run(string[] args)
        {
            if (args.Any())
            {
                args = _mergeQuotedService.Merge(args, Quote).ToArray();
                await SearchAndPrintFor(args);
                
                if(!ShouldRepeat())
                    return;
            }

            do
            {
                args = _mergeQuotedService.Merge(GetSearchTerms(), Quote).ToArray();
                await SearchAndPrintFor(args);
            } while (ShouldRepeat());
        }

        private IEnumerable<string> GetSearchTerms()
        {
            Console.WriteLine("Please enter words to search, separated by space:");
            return Console.ReadLine().Split(' ');
        }

        private async Task SearchAndPrintFor(string[] args)
        {
            var results = await _searchfightService.CountSearchTermsResults(args.ToList());
            PrintResults(results);
        }

        private void PrintResults(List<SearchResults> results)
        {
            var groupedBySearchTerm = results
                .GroupBy(r => r.SearchTerm);
            foreach (var group in groupedBySearchTerm)
            {
                var str = group.OrderBy(r => r.SearchEngineName).Select(r => $"{r.SearchEngineName}: {r.ResultsCount}");
                Console.WriteLine($"{group.Key}: {string.Join(' ', str)}");
            }

            var groupedByEngine = results
                .GroupBy(r => r.SearchEngineName)
                .Select(g => new {Engine = g.Key, TopResult = g.OrderByDescending(r => r.ResultsCount).First().SearchTerm});
            foreach (var engineResult in groupedByEngine)
            {
                Console.WriteLine($"{engineResult.Engine} winner: {engineResult.TopResult}");
            }

            var topResult = results.OrderByDescending(r => r.ResultsCount).First().SearchTerm;
            Console.WriteLine($"Total winner: {topResult}");
        }

        private bool ShouldRepeat()
        {
            Console.Write("Continue? (y/n): ");
            var answer = Console.ReadLine();
            return answer.Last() == 'y';
        }
    }
}
