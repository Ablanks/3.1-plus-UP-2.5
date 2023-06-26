namespace AvaloniaApplicationPracticeOne.Models;
using System.Collections.Generic;

public partial class Car
{
    public int Id { get; set; }

    public int? IdTypeCar { get; set; }
    public string Name { get; set; } = null!;
    
    public string StateNumber { get; set; } = null!;
    
    public int CountPassengers { get; set; }
    
    public virtual TypeCar? IdTypeCarNavigation { get; set; }
    
    public virtual ICollection<Route> Routes { get; set; } = new List<Route>();

}