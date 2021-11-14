using FunWithFlights.Helpers;

namespace FunWithFlights.Services
{
    public class RoutesAggregator : IRoutesAggregator
    {
        const string FlightsDataSourcesSection = "FlightsDataSources";

        private readonly IRoutesDataSource _flightDataSource;
        private readonly IEnumerable<FlightDataSourceUri> _dataSourceUris;

        public RoutesAggregator(IRoutesDataSource flightDataSource, IConfiguration configuration)
        {
            _flightDataSource = flightDataSource;
            var stringUrls = configuration
                .GetSection(FlightsDataSourcesSection)
                .Get<IEnumerable<string>>();
            if (stringUrls is default(IEnumerable<string>) || !stringUrls.Any())
            {
                throw new ArgumentException("Configuration for data srource uri wasn't provided");
            }

            _dataSourceUris = stringUrls.Select(x => new FlightDataSourceUri(x));
        }

        public async Task<IEnumerable<Data.Route>> GetRoutes(RouteResourceParameters parameters)
        {		
			var tasks = _dataSourceUris.Select(_flightDataSource.GetData);
			var data = await Task.WhenAll(tasks);
			return data
				.Aggregate(new List<Data.Route>().AsEnumerable(), (first, second) => first.Concat(second))
				.Distinct()
				.Skip(parameters.PageSize * (parameters.PageNumber - 1))
				.Take(parameters.PageSize);
        }
    }
}
