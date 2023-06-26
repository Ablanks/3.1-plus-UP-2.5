using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;


namespace AvaloniaApplicationPracticeOne;

public partial class AdminTableSelectWindow : Window
{
    public AdminTableSelectWindow()
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
        new AdminWindowTypeCar().Show();
        Close();
    }
    private void Table2Btn_OnClick(object? sender, RoutedEventArgs e)
    {
        new AdminWindowCar().Show();
        Close();
    }
    private void Table3Btn_OnClick(object? sender, RoutedEventArgs e)
    {
        new AdminWindowDriver().Show();
        Close();
    }
    private void Table4Btn_OnClick(object? sender, RoutedEventArgs e)
    {
        new AdminWindowRightsCategory().Show();
        Close();
    }
    private void Table5Btn_OnClick(object? sender, RoutedEventArgs e)
    {
        new AdminWindowDriverRightsCategory().Show();
        Close();
    }
    private void Table6Btn_OnClick(object? sender, RoutedEventArgs e)
    {
        new AdminWindowItinerary().Show();
        Close();
    }
    
    private void Table7Btn_OnClick(object? sender, RoutedEventArgs e)
    {
        new AdminWindowRoute().Show();
        Close();
    }
    private void BackBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        new MainWindow().Show();
        // Закрываем текущее окно
        Close();
    }
}