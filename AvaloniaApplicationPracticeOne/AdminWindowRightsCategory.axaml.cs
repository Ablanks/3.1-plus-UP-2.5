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

public partial class AdminWindowRightsCategory : Window
{
    private DataGrid usersDGrid;
    private TextBox searchTBox;
    
    public AdminWindowRightsCategory()
    {
        InitializeComponent();

        usersDGrid = this.FindControl<DataGrid>("UsersDGrid");
        searchTBox = this.FindControl<TextBox>("SearchTBox");
        
        // Передаем данные в DataGrid
        usersDGrid.Items = Service.GetDbContext().RightsCategory.ToList();

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

    private void AddBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        new AddWindowRightCategory().Show();
        Close();
    }
    private void DeleteBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Получим выбранный элемент в DataGrid, приведем его к типу User
        RightCategory selectedCar = usersDGrid.SelectedItem as RightCategory;

        // Если элемент выбран
        if (selectedCar != null)
        {
            // Получим все строки в таблице 'SomeTable', которые ссылаются на выбранный элемент в таблице 'User'
            List<DriverRightsCategory> someTables = Service.GetDbContext().DriverRightsCategories.Where(q => q.IdRightsCategory == selectedCar.Id).ToList();

            // Удалим связь между таблицами, установив значение свойства 'UserId' в 'null' для всех строк в таблице 'SomeTable
            foreach (var d in someTables)
            {
                var g  = Service.GetDbContext().Entry(d);
                g.Reference(q => q.IdRightsCategoryNavigation).CurrentValue = null;
            }

            // Удалим этот элемент (Пользователя)
            Service.GetDbContext().RightsCategory.Remove(selectedCar);

            // Сохраним изменения
            Service.GetDbContext().SaveChanges();

            // Перезаполним список пользователей
            usersDGrid.Items = Service.GetDbContext().RightsCategory.ToList();
        }
    }
    
    private async void EditBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Получим выбранный элемент в DataGrid, приведем его к типу User
        RightCategory? selectedUser = usersDGrid.SelectedItem as RightCategory;
        // Если элемент выбран
        if (selectedUser != null)
        {
            // Откроем окно редактирования, передадим в него пользователя
            await new EditWindowRightCategory(selectedUser).ShowDialog(this);
            // Перезаполним список пользователей
            usersDGrid.Items = Service.GetDbContext().RightsCategory.ToList();
        }
    }
    private void SearchTBox_OnKeyUp(object? sender, KeyEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(searchTBox.Text))
        {
            usersDGrid.Items = Service.GetDbContext().RightsCategory.ToList();
        }
        // Если в поле есть данные, фильтруем массив данных, ищем совпадения
        else
        {
            usersDGrid.Items = Service.GetDbContext().RightsCategory
                .Where(q=>q.Name.ToLower().Contains(searchTBox.Text.ToLower())).ToList();}
    }
}