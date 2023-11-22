using Domain.Entities;
using Domain.Enumerables;
using Domain.Extensions;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public abstract class BaseMongoRepository<TDocument> : IBaseMongoRepository<TDocument>
    where TDocument : BaseMongoEntity
{
    private readonly IMongoCollection<TDocument> collection;
    private readonly IHttpContextAccessor accessor;

    protected BaseMongoRepository(IMongoContext context, IHttpContextAccessor accessor)
    {
        collection = context.GetCollection<TDocument>(typeof(TDocument).Name);
        this.accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
    }

    public virtual async Task<TDocument> GetByIdAsync(string id)
    {
        var builder = Builders<TDocument>.Filter;
        var filter = builder.Eq(doc => doc.Id, id);

        return await collection.FindAsync(filter).Result.FirstOrDefaultAsync();
    }

    public virtual async Task<IEnumerable<TDocument>> GetListAsync(FilterDefinition<TDocument> filter)
    {
        return await collection.Find(filter)
            .Sort(Builders<TDocument>.Sort.Descending("createdOn"))
            .ToListAsync();
    }

    public virtual async Task<IEnumerable<TDocument>> GetListPaginateAsync(FilterDefinition<TDocument> filter, int page, int limit)
    {
        return await collection.Find(filter)
            .Skip((page - 1) * limit)
            .Limit(limit)
            .Sort(Builders<TDocument>.Sort.Descending("createdOn"))
            .ToListAsync();
    }

    public virtual async Task<long> CountAsync(FilterDefinition<TDocument> filter)
    {
        return await collection.CountDocumentsAsync(filter);
    }

    public virtual async Task AddAsync(TDocument obj)
    {
        var dtUtcNow = DateTime.UtcNow;
        var userId = accessor.UserId();

        obj.Status = RecordStatus.Active.Code();
        obj.CreatedOn = dtUtcNow;
        obj.CreatedBy = userId;
        obj.UpdatedOn = dtUtcNow;
        obj.UpdatedBy = userId;

        await collection.InsertOneAsync(obj);
    }

    public virtual async Task UpdateAsync(TDocument obj)
    {
        var dtUtcNow = DateTime.UtcNow;
        var userId = accessor.UserId();

        obj.UpdatedOn = dtUtcNow;
        obj.UpdatedBy = userId;

        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, obj.Id);
        await collection.ReplaceOneAsync(filter, obj);
    }

    public virtual async Task DeleteAsync(TDocument obj)
    {
        var dtUtcNow = DateTime.UtcNow;
        var userId = accessor.UserId();

        obj.Status = RecordStatus.Deleted.Code();
        obj.DeletedOn = dtUtcNow;
        obj.DeletedBy = userId;

        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, obj.Id);
        await collection.ReplaceOneAsync(filter, obj);
    }

    public virtual async Task ForceDeleteAsync(TDocument obj)
    {
        var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, obj.Id);
        await collection.DeleteOneAsync(filter);
    }
}
