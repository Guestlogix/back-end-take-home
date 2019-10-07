using AutoMapper;

namespace RouteSearch.Application.EntityMapper
{
    public class MapperConfig
    {
        public static IMapper Configure()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => {
                cfg.AddProfile<DomainToDTOMapper>();
            });
            
            return new Mapper(mapperConfiguration);
        }
    }
}