using AutoMapper;
using FunWithFlights.Helpers;
using FunWithFlights.Models;
using FunWithFlights.Services;
using Microsoft.AspNetCore.Mvc;

namespace FunWithFlights.Controllers
{
    [ApiController]
    [Route("api/v1/routes")]
    public class RoutesV1Controller : ControllerBase
    {
        private readonly IRoutesAggregator _aggregator;
        private readonly IMapper _mapper;

        public RoutesV1Controller(IRoutesAggregator aggregator, IMapper mapper)
        {
            _aggregator = aggregator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RouteDto>>> GetRoutes([FromQuery]RouteResourceParameters routeResourcePatrameters)
        {
            var data = await _aggregator.GetRoutes(routeResourcePatrameters);
            var models = _mapper.Map<IEnumerable<RouteDto>>(data);
            return data.Any() ? Ok(models) : NotFound();
        }
    }
}