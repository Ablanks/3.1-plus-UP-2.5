namespace AvaloniaApplicationPracticeOne.Models;

public class Route
{
    public int Id { get; set; }
    
    public int? IdDriver { get; set; }

    public int? IdCar { get; set; }
    
    public int? IdItinerary { get; set; }
    
    public int NumberPassengers { get; set; }
    
    public virtual Driver? IdDriverNavigation { get; set; }
    
    public virtual Car? IdCarNavigation { get; set; }
    
    public virtual Itinerary? IdItineraryNavigation { get; set; }
}