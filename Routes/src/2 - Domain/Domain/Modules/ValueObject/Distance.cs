namespace Domain.Modules.ValueObject
{
    public class Distance
    {
        private const float UnitConversion = 1000.0f;
        public double Meters { get; private set; }
        public double Kilometers { get; private set; }

        public void SetDistanceByMeter(double meters)
        {
            Meters = meters;
            Kilometers = meters / UnitConversion;
        }

        public void SetDistanceByKilometers(double kilometers)
        {
            Meters = kilometers * UnitConversion;
            Kilometers = kilometers;
        }

        public static class DistanceFactory
        {
            
            public static Distance NewDistanceByMeters(double meters)
            {
                var distance = new Distance();
                distance.SetDistanceByMeter(meters);
                return distance;
            }

            public static Distance NewDistanceByKilometers(double kilometers)
            {
                var distance = new Distance();
                distance.SetDistanceByKilometers(kilometers);
                return distance;
            }
        }
    }
}
