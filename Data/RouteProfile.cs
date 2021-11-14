using AutoMapper;
using FunWithFlights.Models;

namespace FunWithFlights.Data
{
    public class RouteProfile : Profile
    {
        public RouteProfile()
        {
            this.CreateMap<Route, RouteDto>();
        }
    }
}
