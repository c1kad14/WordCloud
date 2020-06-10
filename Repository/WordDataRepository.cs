using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using WordCloud.Abstractions;
using WordCloud.Context;
using WordCloud.Entities;

namespace WordCloud.Repository
{
    //repository class for crud operations with word entity
    public class WordDataRepository : IDataRepository<Word>
    {
        protected readonly DbContextOptions _dbContextOptions;
        public WordDataRepository(DbContextOptions dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        //method for adding new entity
        public void Add(Word entity)
        {
            using var context = new WordDataContext(_dbContextOptions);
            context.Words.Add(entity);
            context.SaveChanges();
        }

        //method for retrieving single record based on word value
        public async Task<Word> Get(string word)
        {
            using var context = new WordDataContext(_dbContextOptions);
            return await context.Words.SingleOrDefaultAsync(w => w.Value == word);
        }

        //method for getting all words
        public async Task<List<Word>> Get()
        {
            using var context = new WordDataContext(_dbContextOptions);
            return await context.Words.ToListAsync();
        }

        //methond for updating existing word entity
        public void Update(Word entity)
        {
            using var context = new WordDataContext(_dbContextOptions);
            context.Words.Update(entity);
            context.SaveChanges();
        }
    }
}