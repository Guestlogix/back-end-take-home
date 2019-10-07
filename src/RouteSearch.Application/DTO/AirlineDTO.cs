namespace RouteSearch.Application.DTO
{
    public class AirlineDTO
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Country { get; private set; }

        protected AirlineDTO() {}
        
        public AirlineDTO(string code, string name, string country)
        {
            Code = code;
            Name = name;
            Country = country;
        }
    }
}