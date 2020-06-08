using System.Collections.Generic;

namespace Searchfight.Application
{
    public interface IMergeQuotedService
    {
        List<string> Merge(IEnumerable<string> arguments, string quote);
    }
}