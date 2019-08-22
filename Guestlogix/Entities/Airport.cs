using System.ComponentModel.DataAnnotations;

public class Airport{
    public string Name{
        get;
        set;
    }

    public string City{
        get;
        set;
    }

    public string Country{
        get;
        set;
    }

    [Key]
    public string Iata3{
        get;
        set;
    }

    public decimal Latitude{
        get;
        set;
    }

    public decimal Longitude{
        get;
        set;
    }
}