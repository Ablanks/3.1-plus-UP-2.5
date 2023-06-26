namespace AvaloniaApplicationPracticeOne.Models;

public class DriverRightsCategory
{
    public int IdDriver { get; set; }

    public int IdRightsCategory { get; set; }
    
    public virtual Driver? IdDriverNavigation { get; set; }
    
    public virtual RightCategory? IdRightsCategoryNavigation { get; set; }
}