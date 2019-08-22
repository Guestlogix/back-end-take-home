using System.ComponentModel.DataAnnotations;

public class Airline{
    public string Name{
        get;
        set;
    }

    public string DigitCode2{
        get;
        set;
    }

    [Key]
    public string DigitCode3{
        get;
        set;
    }

    public string Country{
        get;
        set;
    }
}