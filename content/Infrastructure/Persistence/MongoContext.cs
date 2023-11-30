using Domain.Extensions;
using MongoDB.Driver;

namespace Infrastructure.Persistence;

public class MongoContext : IMongoContext
{
    private IMongoDatabase Db { get; set; }
    private MongoClient MongoClient { get; set; }
    public MongoContext(IMongoSettings settings)
    {
        MongoClient = new MongoClient(settings.ConnectionString);
        Db = MongoClient.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<TDocument> GetCollection<TDocument>(string name)
    {
        return Db.GetCollection<TDocument>(name.ToSnakeCase());
    }
}
