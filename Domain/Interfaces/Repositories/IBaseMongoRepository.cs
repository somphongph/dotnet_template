using Domain.Entities;
using MongoDB.Driver;

namespace Domain.Interfaces.Repositories;

public interface IBaseMongoRepository<TDocument>
    where TDocument : BaseMongoEntity
{
    Task<TDocument> GetByIdAsync(string id);
    Task<IEnumerable<TDocument>> GetListAsync(FilterDefinition<TDocument> filter);
    Task<IEnumerable<TDocument>> GetListPaginateAsync(FilterDefinition<TDocument> filter, int page, int limit);
    Task<long> CountAsync(FilterDefinition<TDocument> filter);
    Task AddAsync(TDocument obj);
    Task UpdateAsync(TDocument obj);
    Task DeleteAsync(TDocument obj);
    Task ForceDeleteAsync(TDocument obj);
}
