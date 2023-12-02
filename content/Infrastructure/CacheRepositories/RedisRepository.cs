using System.Text.Json;
using Domain.Interfaces.CacheRepositories;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.CacheRepositories;

public class RedisRepository : IRedisRepository
{
    private readonly IDistributedCache distributedCache;

    private readonly IConfiguration configuration;
    public RedisRepository(IDistributedCache distributedCache, IConfiguration configuration)
    {
        this.distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<T?> GetCacheAsync<T>(string key)
    {
        var result = await distributedCache.GetStringAsync(key);

        if (!string.IsNullOrEmpty(result))
        {
            return JsonSerializer.Deserialize<T>(result);
        }

        return default;
    }

    public async Task<T?> AddCacheShortAsync<T>(string key, T data)
    {
        var expireShort = Convert.ToInt32(configuration.GetValue<string>("RedisSettings:ExpireShort"));

        return await AddCacheAsync(key, data, expireShort);
    }

    public async Task<T?> AddCacheLongAsync<T>(string key, T data)
    {
        var expireLong = Convert.ToInt32(configuration.GetValue<string>("RedisSettings:ExpireLong"));

        return await AddCacheAsync(key, data, expireLong);
    }

    public async Task<T?> AddCacheAsync<T>(string key, T data, double expires)
    {
        var cache = await distributedCache.GetStringAsync(key);

        if (string.IsNullOrEmpty(cache))
        {
            var json = JsonSerializer.Serialize(data);
            var opt = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(expires));

            await distributedCache.SetStringAsync(key, json, opt);

            cache = json;
        }

        return JsonSerializer.Deserialize<T>(cache);
    }

    public async Task<T?> AddCacheSlidingAsync<T>(string key, T data, double expires)
    {
        var cache = await distributedCache.GetStringAsync(key);

        if (string.IsNullOrEmpty(cache))
        {
            var json = JsonSerializer.Serialize(data);
            var opt = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(expires));

            await distributedCache.SetStringAsync(key, json, opt);

            cache = json;
        }

        return JsonSerializer.Deserialize<T>(cache);
    }

    public async Task RemoveCacheAsync(string key)
    {
        await distributedCache.RemoveAsync(key);
    }
}

