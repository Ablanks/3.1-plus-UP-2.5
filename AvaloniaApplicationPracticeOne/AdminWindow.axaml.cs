using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplicationPracticeOne.Models;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplicationPracticeOne;

public partial class AdminWindow : Window
{
    private DataGrid usersDGrid;
    private TextBox searchTBox;
    
    public AdminWindow()
    {
        InitializeComponent();
        
        usersDGrid = this.FindControl<DataGrid>("UsersDGrid");
        searchTBox = this.FindControl<TextBox>("SearchTBox");
        
        // Передаем данные в DataGrid
        usersDGrid.Items = Service.GetDbContext().Users.Include(q=>q.IdRoleNavigation).ToList();
        
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
        new MainWindow().Show();
        Close();
    }

    // Данный метод срабатывает, когда пользователь нажимает на кнопку Найти
    private void SearchBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Проверяем, есть ли данные в поле для ввода
        // Если поле пустое, выводим список всех пользователей
        if (string.IsNullOrWhiteSpace(searchTBox.Text))
        {
            usersDGrid.Items = Service.GetDbContext().Users.Include(q=>q.IdRoleNavigation).ToList();
        }
        // Если в поле есть данные, фильтруем массив данных, ищем совпадения
        else
        {
            usersDGrid.Items = Service.GetDbContext().Users
                .Where(q => q.Login.ToLower().Contains(searchTBox.Text.ToLower()) 
                            || q.Name.ToLower().Contains(searchTBox.Text.ToLower()) 
                            || q.Surname.ToLower().Contains(searchTBox.Text.ToLower())).ToList();   
        }
    }
    
    
    // Данный метод срабатывает, когда пользователь нажимает на кнопку Удалить
    private void DeleteBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Получим выбранный элемент в DataGrid, приведем его к типу User
        User? selectedUser = usersDGrid.SelectedItem as User;
        // Если элемент выбран
        if (selectedUser != null)
        {
            // Удалим этот элемент(Пользователя)
            Service.GetDbContext().Users.Remove(selectedUser);
            // Сохраним изменения
            Service.GetDbContext().SaveChanges();
            // Перезаполним список пользователей
            usersDGrid.Items = Service.GetDbContext().Users.Include(q=>q.IdRoleNavigation).ToList();
        }
    }

    // Данный метод срабатывает, когда пользователь нажимает на кнопку Изменить
    private async void EditBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Получим выбранный элемент в DataGrid, приведем его к типу User
        User? selectedUser = usersDGrid.SelectedItem as User;
        // Если элемент выбран
        if (selectedUser != null)
        {
            // Откроем окно редактирования, передадим в него пользователя
            await new EditWindow(selectedUser).ShowDialog(this);
            // Перезаполним список пользователей
            usersDGrid.Items = Service.GetDbContext().Users.Include(q=>q.IdRoleNavigation).ToList();
        }
    }
}