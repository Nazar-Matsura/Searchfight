using System.Collections.Generic;
using System.Text;

namespace Searchfight.Application
{
    internal class MergeQuotedService : IMergeQuotedService
    {
        public List<string> Merge(IEnumerable<string> arguments, string quote)
        {
            var result = new List<string>();
            var quotedString = new StringBuilder();
            foreach (var value in arguments)
            {
                var valueBetweenQuotes = new StringBuilder(value);
                if (value.StartsWith(quote))
                {
                    valueBetweenQuotes.Remove(valueBetweenQuotes.Length-1, 1);
                    quotedString.Clear();
                }

                if (value.EndsWith(quote))
                {
                    valueBetweenQuotes.Remove(0, 1);
                    result.Add(valueBetweenQuotes.ToString());
                    quotedString.Clear();
                    continue;
                }

                if (quotedString.Length > 0)
                {
                    quotedString.Append(value);
                }
                else
                {
                    result.Add(value);
                }
            }

            return result;
        }
    }
}