namespace Infrastructure.Persistence;

public interface IMongoSettings
{
    string ConnectionString { get; set; }
    string DatabaseName { get; set; }
}
