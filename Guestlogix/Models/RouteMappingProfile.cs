using AutoMapper;

public class RouteMappingProfile : Profile{
    public RouteMappingProfile(){
        CreateMap<Route, RouteModel>().ReverseMap();
    }
}