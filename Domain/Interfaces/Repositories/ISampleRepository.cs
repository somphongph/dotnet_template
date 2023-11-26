using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface ISampleRepository : IBaseMongoRepository<Sample>
{
    Task<Sample> GetByCodeAsync(string code);
}
