using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Searchfight.Application;
using Searchfight.Core.ViewModels;

namespace Searchfight.Terminal
{
    public class App
    {
        private readonly ISearchfightService _searchfightService;
        private readonly ISearchDataProvider _searchDataProvider;
        private readonly IMergeQuotedService _mergeQuotedService;
        private const string Quote = "\"";

        public App(ISearchDataProvider searchDataProvider, 
            IMergeQuotedService mergeQuotedService,
            ISearchfightService searchfightService)
        {
            _searchDataProvider = searchDataProvider;
            _mergeQuotedService = mergeQuotedService;
            _searchfightService = searchfightService;
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
            try
            {
                var data = await _searchDataProvider.CountSearchTermsResults(args.ToList());
                var report = _searchfightService.GenerateReport(data);
                PrintResults(report);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void PrintResults(GeneralSearchfightViewModel results)
        {
            foreach (var searchTermResults in results.ResultsBySearchTerm)
            {
                var str = searchTermResults.SearchEngineResults.Select(r => $"{r.SearchEngine}: {r.ResultsCount}");
                Console.WriteLine($"{searchTermResults.SearchTerm}: {string.Join(' ', str)}");
            }

            foreach (var engineResult in results.SearchEngineWinners)
            {
                Console.WriteLine($"{engineResult.SearchEngine} winner: {engineResult.SearchTerm}");
            }

            Console.WriteLine($"Total winner: {results.TotalWinner}");
        }

        private bool ShouldRepeat()
        {
            Console.Write("Continue? (y/n): ");
            var answer = Console.ReadLine();
            return answer.Last() == 'y';
        }
    }
}
