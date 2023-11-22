using Domain.Extensions;
using MongoDB.Driver;

namespace Infrastructure.Persistence;

public class MongoContext : IMongoContext
{
    private IMongoDatabase db { get; set; }
    private MongoClient mongoClient { get; set; }
    public MongoContext(IMongoSettings settings)
    {
        mongoClient = new MongoClient(settings.ConnectionString);
        db = mongoClient.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<TDocument> GetCollection<TDocument>(string name)
    {
        return db.GetCollection<TDocument>(name.ToSnakeCase());
    }
}
