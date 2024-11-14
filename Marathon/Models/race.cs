namespace Marathon.Models;

public class RaceCollection
{
    public Race[] races { get; set; }
}


public class Race
{
    public int id { get; set; }
    
    public string race_name { get; set; }
}

