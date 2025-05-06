namespace ServicesAbstraction;

public interface ICacheService
{
    Task<string?> GetAsync(string cacheKey);
    Task SetAsync(string key , object value , TimeSpan expiration);
}