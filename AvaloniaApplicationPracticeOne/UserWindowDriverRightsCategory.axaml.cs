using System.Linq;
using System.Net.Mime;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.TextInput;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplicationPracticeOne;

public partial class UserWindowDriverRightsCategory : Window
{
    private DataGrid usersDGrid;
    private TextBox searchTBox;
    
    public UserWindowDriverRightsCategory()
    {
        InitializeComponent();

        usersDGrid = this.FindControl<DataGrid>("UsersDGrid");
        searchTBox = this.FindControl<TextBox>("SearchTBox");
        
        // Передаем данные в DataGrid
        usersDGrid.Items = Service.GetDbContext().DriverRightsCategories
            .Include(q=>q.IdDriverNavigation)
            .Include(q => q.IdRightsCategoryNavigation).ToList();

#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    // Данный метод срабатывает, когда пользователь нажмет на кнопку Выйти 
    private void LogOutBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        new UserTableSelectWindow().Show();
        Close();
    }


    private void SearchTBox_OnKeyUp(object? sender, KeyEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(searchTBox.Text))
        {
            usersDGrid.Items = Service.GetDbContext().DriverRightsCategories.ToList();
        }
        // Если в поле есть данные, фильтруем массив данных, ищем совпадения
        else
        {
            usersDGrid.Items = Service.GetDbContext().DriverRightsCategories
                .Where(q=>q.IdDriverNavigation.FirstName.ToLower().Contains(searchTBox.Text.ToLower()) 
                       || q.IdDriverNavigation.LastName.ToLower().Contains(searchTBox.Text.ToLower())).ToList();}
    }
}