using AutoMapper;

public class AirportMappingProfile : Profile{
    public AirportMappingProfile(){
        CreateMap<Airport, AirportModel>().ReverseMap();
    }
}