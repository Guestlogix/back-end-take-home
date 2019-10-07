using AutoMapper;
using RouteSearch.Application.DTO;
using RouteSearch.Domain.Entities;

namespace RouteSearch.Application.EntityMapper
{
    public class DomainToDTOMapper : Profile
    {
        public DomainToDTOMapper()
        {
            CreateMap<Airline, AirlineDTO>()
                .ForMember(d => d.Code, opt => opt.MapFrom(src => src.TwoDigitCode));
                
            CreateMap<Route, RouteDTO>();

            CreateMap<FlightRoute, FlightRouteDTO>();

            CreateMap<Airport, AirportDTO>()
                .ForMember(d => d.AirportName, opt => opt.MapFrom(src => src.Name));
        }
    }
}