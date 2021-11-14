using FunWithFlights.Helpers;

namespace FunWithFlights.Services
{
    public interface IRoutesDataSource
	{
		Task<IEnumerable<Data.Route>> GetData(FlightDataSourceUri uri);
	}
}
