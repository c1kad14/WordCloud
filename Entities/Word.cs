using WordCloud.Abstractions;

namespace WordCloud.Entities
{
    //entity describes word occurence
    public class Word : IEntity
    {
        public int Count { get; set; }
        public string Id { get; set; }
        public string Value { get; set; }
    }
}