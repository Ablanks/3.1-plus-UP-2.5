using System.Collections.Generic;
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

public partial class AdminWindowDriver : Window
{
    private DataGrid usersDGrid;
    private TextBox searchTBox;
    
    public AdminWindowDriver()
    {
        InitializeComponent();

        usersDGrid = this.FindControl<DataGrid>("UsersDGrid");
        searchTBox = this.FindControl<TextBox>("SearchTBox");
        
        // Передаем данные в DataGrid
        usersDGrid.Items = Service.GetDbContext().Driver.ToList();

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
        new AddWindowDriver().Show();
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
        Driver selectedCar = usersDGrid.SelectedItem as Driver;

        // Если элемент выбран
        if (selectedCar != null)
        {
            // Получим все строки в таблице 'SomeTable', которые ссылаются на выбранный элемент в таблице 'User'
            List<Route> someTables = Service.GetDbContext().Routes.Where(q => q.IdDriver == selectedCar.Id).ToList();

            // Удалим связь между таблицами, установив значение свойства 'UserId' в 'null' для всех строк в таблице 'SomeTable
            foreach (var d in someTables)
            {
                var g  = Service.GetDbContext().Entry(d);
                g.Reference(q => q.IdDriverNavigation).CurrentValue = null;
            }
            List<DriverRightsCategory> someTable = Service.GetDbContext().DriverRightsCategories.Where(q => q.IdDriver == selectedCar.Id).ToList();

            // Удалим связь между таблицами, установив значение свойства 'UserId' в 'null' для всех строк в таблице 'SomeTable
            foreach (var d in someTable)
            {
                var g  = Service.GetDbContext().Entry(d);
                g.Reference(q => q.IdDriverNavigation).CurrentValue = null;
            }

            // Удалим этот элемент (Пользователя)
            Service.GetDbContext().Driver.Remove(selectedCar);

            // Сохраним изменения
            Service.GetDbContext().SaveChanges();

            // Перезаполним список пользователей
            usersDGrid.Items = Service.GetDbContext().Driver.ToList();
        }
    }
    
    private async void EditBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Получим выбранный элемент в DataGrid, приведем его к типу User
        Driver? selectedUser = usersDGrid.SelectedItem as Driver;
        // Если элемент выбран
        if (selectedUser != null)
        {
            // Откроем окно редактирования, передадим в него пользователя
            await new EditWindowDriver(selectedUser).ShowDialog(this);
            // Перезаполним список пользователей
            usersDGrid.Items = Service.GetDbContext().Driver.ToList();
        }
    }
    private void SearchTBox_OnKeyUp(object? sender, KeyEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(searchTBox.Text))
        {
            usersDGrid.Items = Service.GetDbContext().Driver.ToList();
        }
        // Если в поле есть данные, фильтруем массив данных, ищем совпадения
        else
        {
            usersDGrid.Items = Service.GetDbContext().Driver
                .Where(q=>q.FirstName.ToLower().Contains(searchTBox.Text.ToLower()) 
                       || q.LastName.ToLower().Contains(searchTBox.Text.ToLower())).ToList();
            
        }
    }
}