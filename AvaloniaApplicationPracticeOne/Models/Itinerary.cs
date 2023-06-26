namespace AvaloniaApplicationPracticeOne.Models;
using System.Collections.Generic;

public class Itinerary
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public virtual ICollection<Route> Routes { get; set; } = new List<Route>();

}