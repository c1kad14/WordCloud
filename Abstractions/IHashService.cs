namespace WordCloud.Abstractions
{
    //interface for hashing service
    public interface IHashService
    {
         string Hash(string salt);
    }
}