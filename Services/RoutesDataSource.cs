using FunWithFlights.Helpers;
using Newtonsoft.Json;

namespace FunWithFlights.Services
{
    public class RoutesDataSource : IRoutesDataSource
    {
        private readonly ILogger<RoutesAggregator> _logger;
        private readonly HttpClient _client;

        public RoutesDataSource(ILogger<RoutesAggregator> logger, HttpClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<IEnumerable<Data.Route>> GetData(FlightDataSourceUri param)
		{
			using var httpResponse = await _client.GetAsync(param.uri, HttpCompletionOption.ResponseHeadersRead);
			httpResponse.EnsureSuccessStatusCode();
			if (httpResponse.Content is not null && httpResponse.Content?.Headers?.ContentType?.MediaType == "application/json")
			{
				var contentStream = await httpResponse.Content.ReadAsStreamAsync();
				using var streamReader = new StreamReader(contentStream);
				using var jsonReader = new JsonTextReader(streamReader);
				var serializer = new JsonSerializer();

				try
				{
					return serializer.Deserialize<IEnumerable<Data.Route>>(jsonReader) ?? Enumerable.Empty<Data.Route>();
				}
				catch (JsonReaderException ex)
				{
					_logger.LogError($"Invalid JSON. (uri = {param.uri})", ex);
				}
			}
			else
			{
				_logger.LogError($"HTTP Response was invalid and cannot be deserialised. (uri = {param.uri})");
			}

			return Enumerable.Empty<Data.Route>();
		}
	}
}
