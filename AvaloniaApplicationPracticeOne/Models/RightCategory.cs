namespace AvaloniaApplicationPracticeOne.Models;
using System.Collections.Generic;

public class RightCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public virtual ICollection<DriverRightsCategory> DriverRightsCategories { get; set; } = new List<DriverRightsCategory>();
}