using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

public partial class AdminWindowCar : Window
{
    private DataGrid usersDGrid;
    private TextBox searchTBox;
    private ObservableCollection<Car> _entities;
    
    public AdminWindowCar()
    {
        InitializeComponent();

        usersDGrid = this.FindControl<DataGrid>("UsersDGrid");
        searchTBox = this.FindControl<TextBox>("SearchTBox");
        
        // Передаем данные в DataGrid
        usersDGrid.Items = Service.GetDbContext().Car.Include(q=>q.IdTypeCarNavigation).ToList();;

#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private async void EditBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Получим выбранный элемент в DataGrid, приведем его к типу User
        Car? selectedUser = usersDGrid.SelectedItem as Car;
        // Если элемент выбран
        if (selectedUser != null)
        {
            // Откроем окно редактирования, передадим в него пользователя
            await new EditWindowCar(selectedUser).ShowDialog(this);
            // Перезаполним список пользователей
            usersDGrid.Items = Service.GetDbContext().Car.ToList();
        }
    }
   
    
    private void AddBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        new AddWindowCar().Show();
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
        Car selectedCar = usersDGrid.SelectedItem as Car;

        // Если элемент выбран
        if (selectedCar != null)
        {
            // Получим все строки в таблице 'SomeTable', которые ссылаются на выбранный элемент в таблице 'User'
            List<Route> someTables = Service.GetDbContext().Routes.Where(q => q.IdCar == selectedCar.Id).ToList();

            // Удалим связь между таблицами, установив значение свойства 'UserId' в 'null' для всех строк в таблице 'SomeTable
            foreach (var d in someTables)
            {
                var g  = Service.GetDbContext().Entry(d);
                g.Reference(q => q.IdCarNavigation).CurrentValue = null;
            }
            var carEntry = Service.GetDbContext().Entry(selectedCar);

            // Удалим связь для выбранного элемента
            carEntry.Reference(q => q.IdTypeCarNavigation).CurrentValue = null;
            
            // Удалим этот элемент (Пользователя)
            Service.GetDbContext().Car.Remove(selectedCar);

            // Сохраним изменения
            Service.GetDbContext().SaveChanges();

            // Перезаполним список пользователей
            usersDGrid.Items = Service.GetDbContext().Car.Include(q => q.IdTypeCarNavigation).ToList();
        }
    }

    private void SearchTBox_OnKeyUp(object? sender, KeyEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(searchTBox.Text))
        {
            usersDGrid.Items = Service.GetDbContext().Car.ToList();
        }
        // Если в поле есть данные, фильтруем массив данных, ищем совпадения
        else
        {
            usersDGrid.Items = Service.GetDbContext().Car
                .Where(q=>q.IdTypeCarNavigation.Name.ToLower().Contains(searchTBox.Text.ToLower()) 
                       || q.Name.ToLower().Contains(searchTBox.Text.ToLower()) 
                       || q.StateNumber.ToLower().Contains(searchTBox.Text.ToLower())).ToList();}
    }
}