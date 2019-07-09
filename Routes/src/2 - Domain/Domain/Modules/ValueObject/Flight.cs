namespace Domain.Modules.ValueObject
{
    public class Flight
    {
        public AirLine AirLine { get; set; }
        public AirPort AirPort { get; set; }
        public Distance Distance { get; set; }
        public int Order { get; set; }
    }
}