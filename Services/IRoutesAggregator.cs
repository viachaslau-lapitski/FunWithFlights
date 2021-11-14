using FunWithFlights.Helpers;

namespace FunWithFlights.Services
{
    public interface IRoutesAggregator
    {
        Task<IEnumerable<Data.Route>> GetRoutes(RouteResourceParameters parameters);
    }
}
