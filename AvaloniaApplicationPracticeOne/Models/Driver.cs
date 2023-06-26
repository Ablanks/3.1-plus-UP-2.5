using System;
using System.Collections.Generic;

namespace AvaloniaApplicationPracticeOne.Models;

public partial class Driver
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;
    
    public string? Birthdate { get; set; }
    
    public virtual ICollection<DriverRightsCategory> DriverRightsCategories { get; set; } = new List<DriverRightsCategory>();
    
    public virtual ICollection<Route> Routes { get; set; } = new List<Route>();
}