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

public partial class AdminWindowRoute : Window
{
    private DataGrid usersDGrid;
    private TextBox searchTBox;
    
    public AdminWindowRoute()
    {
        InitializeComponent();

        usersDGrid = this.FindControl<DataGrid>("UsersDGrid");
        searchTBox = this.FindControl<TextBox>("SearchTBox");
        
        // Передаем данные в DataGrid
        usersDGrid.Items = Service.GetDbContext().Routes
            .Include(q=>q.IdDriverNavigation)
            .Include(q => q.IdCarNavigation)
            .Include(q => q.IdCarNavigation.IdTypeCarNavigation)
            .Include(q => q.IdItineraryNavigation).ToList();

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
        new AdminTableSelectWindow().Show();
        Close();
    }
    
    private void DeleteBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Получим выбранный элемент в DataGrid, приведем его к типу User
         var selectedCar = usersDGrid.SelectedItem as Route;

        // Если элемент выбран
        if (selectedCar != null)
        {
            var carEntry = Service.GetDbContext().Entry(selectedCar);

            // Удалим связь для выбранного элемента
            carEntry.Reference(q => q.IdCarNavigation).CurrentValue = null;
            carEntry.Reference(q => q.IdDriverNavigation).CurrentValue = null;
            carEntry.Reference(q => q.IdItineraryNavigation).CurrentValue = null;
            
            // Удалим этот элемент (Пользователя)
            Service.GetDbContext().Routes.Remove(selectedCar);

            // Сохраним изменения
            Service.GetDbContext().SaveChanges();

            // Перезаполним список пользователей
            usersDGrid.Items = Service.GetDbContext().Routes.ToList();
        }
    }
    private async void EditBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Получим выбранный элемент в DataGrid, приведем его к типу User
        Route? selectedUser = usersDGrid.SelectedItem as Route;
        // Если элемент выбран
        if (selectedUser != null)
        {
            // Откроем окно редактирования, передадим в него пользователя
            await new EditWindowRoute(selectedUser).ShowDialog(this);
            // Перезаполним список пользователей
            usersDGrid.Items = Service.GetDbContext().Routes.ToList();
        }
    }
    private void AddBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        new AddWindowRoute().Show();
        Close();
    }
    
    private void SearchTBox_OnKeyUp(object? sender, KeyEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(searchTBox.Text))
        {
            usersDGrid.Items = Service.GetDbContext().Routes.ToList();
        }
        // Если в поле есть данные, фильтруем массив данных, ищем совпадения
        else
        {
            usersDGrid.Items = Service.GetDbContext().Routes
                .Where(q=>q.IdDriverNavigation.FirstName.ToLower().Contains(searchTBox.Text.ToLower()) 
                          || q.IdDriverNavigation.LastName.ToLower().Contains(searchTBox.Text.ToLower())
                          || q.IdCarNavigation.IdTypeCarNavigation.Name.ToLower().Contains(searchTBox.Text.ToLower())
                          || q.IdCarNavigation.Name.ToLower().Contains(searchTBox.Text.ToLower())
                          || q.IdItineraryNavigation.Name.ToLower().Contains(searchTBox.Text.ToLower())).ToList();
        }
    }
}