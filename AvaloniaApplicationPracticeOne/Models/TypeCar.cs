namespace AvaloniaApplicationPracticeOne.Models;
using System.Collections.Generic;


public partial class TypeCar
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}