using AutoMapper;

public class AirlineMappingProfile : Profile{
    public AirlineMappingProfile(){
        CreateMap<Airline, AirlineModel>().ReverseMap();
    }
}