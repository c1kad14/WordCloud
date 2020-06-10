using Microsoft.EntityFrameworkCore;

using WordCloud.Entities;

namespace WordCloud.Context
{
    //apps data context
    public class WordDataContext : DbContext
    {
        public WordDataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Word> Words { get; set; }
    }
}