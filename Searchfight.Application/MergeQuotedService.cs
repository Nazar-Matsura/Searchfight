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
                StringBuilder valueBetweenQuotes = null;
                if (value.StartsWith(quote))
                {
                    valueBetweenQuotes = new StringBuilder(value).Remove(0, 1);
                    quotedString.Clear();
                }

                if (value.EndsWith(quote))
                {
                    if(valueBetweenQuotes == null)
                        valueBetweenQuotes = new StringBuilder(" ").Append(value);

                    if(valueBetweenQuotes.Length == 0)
                        continue;
                    
                    valueBetweenQuotes.Remove(valueBetweenQuotes.Length-1, 1);
                    quotedString.Append(valueBetweenQuotes);
                    result.Add(quotedString.ToString());
                    quotedString.Clear();
                    continue;
                }

                if (valueBetweenQuotes != null)
                {
                    quotedString.Append(valueBetweenQuotes);
                    continue;
                }

                if (quotedString.Length > 0)
                {
                    quotedString.Append(' ').Append(value);
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