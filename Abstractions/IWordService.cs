using System.Collections.Generic;

namespace WordCloud.Abstractions
{
    //interface for word service for pulling and counting words from the url
    public interface IWordService
    {
         Dictionary<string, int> CalculateAndSortToDictionary(List<string> words);
         List<string> Get(string url);
    }
}