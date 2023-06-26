using System.Linq;
using System.Net.Mime;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.TextInput;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplicationPracticeOne.Models;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplicationPracticeOne;

public partial class AdminWindowDriverRightsCategory : Window
{
    private DataGrid usersDGrid;
    private TextBox searchTBox;
    
    public AdminWindowDriverRightsCategory()
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
    
    private void AddBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        new AddWindowDriverRightsCategory().Show();
        Close();
    }

    // Данный метод срабатывает, когда пользователь нажмет на кнопку Выйти 
    private void LogOutBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        new AdminTableSelectWindow().Show();
        Close();
    }
    
    private void DeleteBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Получим выбранный элемент в DataGrid, приведем его к типу User
        var selectedCar = usersDGrid.SelectedItem as DriverRightsCategory;

        // Если элемент выбран
        if (selectedCar != null)
        {
            var carEntry = Service.GetDbContext().Entry(selectedCar);

            // Удалим связь для выбранного элемента
            carEntry.Reference(q => q.IdRightsCategoryNavigation).CurrentValue = null;
            carEntry.Reference(q => q.IdDriverNavigation).CurrentValue = null;
            
            // Удалим этот элемент (Пользователя)
            Service.GetDbContext().DriverRightsCategories.Remove(selectedCar);

            // Сохраним изменения
            Service.GetDbContext().SaveChanges();

            // Перезаполним список пользователей
            usersDGrid.Items = Service.GetDbContext().DriverRightsCategories.ToList();
        }
    }
    
    private async void EditBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Получим выбранный элемент в DataGrid, приведем его к типу User
        DriverRightsCategory? selectedUser = usersDGrid.SelectedItem as DriverRightsCategory;
        // Если элемент выбран
        if (selectedUser != null)
        {
            // Откроем окно редактирования, передадим в него пользователя
            await new EditWindowDriverRightsCategory(selectedUser).ShowDialog(this);
            // Перезаполним список пользователей
            usersDGrid.Items = Service.GetDbContext().DriverRightsCategories.ToList();
        }
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