using MongoDB.Driver;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Repositories;

public class SampleRepository : BaseMongoRepository<Sample>, ISampleRepository
{
    private readonly IMongoCollection<Sample> _collection;

    public SampleRepository(IMongoContext context, IHttpContextAccessor accessor) : base(context, accessor)
    {
        _collection = context.GetCollection<Sample>(nameof(Sample));

        var indexOptions = new CreateIndexOptions();
        var indexKeys = Builders<Sample>.IndexKeys.Ascending(i => i.Code);
        var indexModel = new CreateIndexModel<Sample>(indexKeys, indexOptions);
        _collection.Indexes.CreateOneAsync(indexModel);
    }

    public async Task<Sample> GetByCodeAsync(string code)
    {
        return await _collection
            .Find(x => x.Code == code)
            .FirstOrDefaultAsync();
    }
}
