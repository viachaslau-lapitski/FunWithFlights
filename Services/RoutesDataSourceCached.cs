using FunWithFlights.Helpers;
using Microsoft.Extensions.Caching.Memory;

namespace FunWithFlights.Services
{
    public class RoutesDataSourceCached : RoutesDataSource, IRoutesDataSource
    {
        const string CacheExpirationMinutesSection = "CacheExpirationMinutes";
        
        // default expiration time is 1 day
        const int defaultExpirationMinutes = 1440;
        private readonly IMemoryCache _memoryCache;
        
        
        private readonly int _cacheExpirationMinutes = defaultExpirationMinutes;

        public RoutesDataSourceCached(
            ILogger<RoutesAggregator> logger, 
            HttpClient client, 
            IMemoryCache memoryCache, 
            IConfiguration configuration) : base(logger, client)
        {
            _memoryCache = memoryCache;

            var expiration = configuration
                .GetSection(CacheExpirationMinutesSection)
                .Get<int>();
            if (expiration != default(int))
            {
                _cacheExpirationMinutes = expiration;
            } else {
                logger.LogWarning("Expiration time wasn't provided, default expiration will be used");
            }
        }

        Task<IEnumerable<Data.Route>> IRoutesDataSource.GetData(FlightDataSourceUri param)
        {
            if (_memoryCache.TryGetValue(param.uri, out Task<IEnumerable<Data.Route>> cachedValue))
            {
                return cachedValue;
            }

            var data = GetData(param);
            var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(_cacheExpirationMinutes));
            _memoryCache.Set(param.uri, data, cacheEntryOptions);
            return data;
        }
    }
}
