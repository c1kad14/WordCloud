using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using HtmlAgilityPack;

using WordCloud.Abstractions;

namespace WordCloud.Services
{
    //class responsible for retrieving, calculating and sorting words
    public class WordService : IWordService
    {
        //method for getting words list from the url
        public List<string> Get(string url)
        {
            var client = new WebClient();
            var html = client.DownloadString(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            doc.DocumentNode.Descendants().Where(n => n.Name == "script" || n.Name == "style").ToList().ForEach(n => n.Remove());
            var nodes = doc.DocumentNode.SelectNodes("//text()[normalize-space(.) != '']");
            var words = new List<string>();
            foreach (var node in nodes)
            {
                char[] separators = new char[] { ' ', ';', ',', '\r', '\t', '\n' };
                var text = node.InnerText.ToLower().Split(separators, StringSplitOptions.RemoveEmptyEntries);
                words.AddRange(text.Where(t => !t.Contains("&") && t.Length > 1));
            }
            return words;
        }

        //method that calculates, write and sort word occurences
        public Dictionary<string, int> CalculateAndSortToDictionary(List<string> words)
        {
            var wordDictionary = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (!wordDictionary.ContainsKey(word))
                {
                    wordDictionary.Add(word, 0);
                }

                wordDictionary[word]++;
            }

            return wordDictionary.OrderByDescending(x => x.Value).Take(100).ToDictionary(w => w.Key, w => w.Value);
        }
    }
}