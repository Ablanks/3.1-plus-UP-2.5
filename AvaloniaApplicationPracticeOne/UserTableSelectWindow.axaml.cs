using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;


namespace AvaloniaApplicationPracticeOne;

public partial class UserTableSelectWindow : Window
{
    public UserTableSelectWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void Table1Btn_OnClick(object? sender, RoutedEventArgs e)
    {
        new UserWindowTypeCar().Show();
        Close();
    }
    private void Table2Btn_OnClick(object? sender, RoutedEventArgs e)
    {
        new UserWindowCar().Show();
        Close();
    }
    private void Table3Btn_OnClick(object? sender, RoutedEventArgs e)
    {
        new UserWindowDriver().Show();
        Close();
    }
    private void Table4Btn_OnClick(object? sender, RoutedEventArgs e)
    {
        new UserWindowRightsCategory().Show();
        Close();
    }
    private void Table5Btn_OnClick(object? sender, RoutedEventArgs e)
    {
        new UserWindowDriverRightsCategory().Show();
        Close();
    }
    private void Table6Btn_OnClick(object? sender, RoutedEventArgs e)
    {
        new UserWindowItinerary().Show();
        Close();
    }
    
    private void Table7Btn_OnClick(object? sender, RoutedEventArgs e)
    {
        new UserWindowRoute().Show();
        Close();
    }
}