using System.Collections.Generic;
using System.Threading.Tasks;

namespace WordCloud.Abstractions
{
    //interface for data repository of IEntity
    public interface IDataRepository<TEntity> where TEntity : IEntity
    {
        void Add(TEntity entity);
        Task<TEntity> Get(string word);
        Task<List<TEntity>> Get();
        void Update(TEntity entity);
    }
}