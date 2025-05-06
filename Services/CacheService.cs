using System.Text.Json;

namespace Services;

public class CacheService(ICacheRepository cacheRepository)
    : ICacheService
{
    public async Task<string?> GetAsync(string cacheKey)
    {
        return await cacheRepository.GetAsync(cacheKey);
    }

    public async Task SetAsync(string key, object value, TimeSpan expiration)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        var serializedValue = JsonSerializer.Serialize(value, options );
        await cacheRepository.SetAsync(key, serializedValue, expiration);
    }
}