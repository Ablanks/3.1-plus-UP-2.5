using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplicationPracticeOne.Models;

namespace AvaloniaApplicationPracticeOne;

public partial class EditWindowRoute : Window
{

    private TextBox nameTBox;
    private Route editUser;
    
    public EditWindowRoute()
    {
        InitializeComponent();

#if DEBUG
        this.AttachDevTools();
#endif
    }

    // Дополнительный конструктор, который в качестве параметра принимает объект пользователя,
    // который нужно отредактировать
    public EditWindowRoute(Route editUser)
    {
        InitializeComponent();
        
        nameTBox = this.FindControl<TextBox>("NameTBox");
        RoleCBox = this.FindControl<ComboBox>("RoleCBox");
        RoleCBox2 = this.FindControl<ComboBox>("RoleCBox2");
        RoleCBox3 = this.FindControl<ComboBox>("RoleCBox3");
        RoleCBox.Items = Service.GetDbContext().Driver.ToList();
        RoleCBox2.Items = Service.GetDbContext().Car.ToList();
        RoleCBox3.Items = Service.GetDbContext().Itineraries.ToList();
        this.editUser = editUser;
        
        RoleCBox.SelectedItem = editUser.IdDriverNavigation;
        RoleCBox2.SelectedItem = editUser.IdCarNavigation;
        RoleCBox3.SelectedItem = editUser.IdItineraryNavigation;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    // Данный метод срабатыват при нажатии на кнопку Зарегистироваться
    private void EditBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Проверяем, чтобы все поля были заполнены
        if (!string.IsNullOrWhiteSpace(nameTBox.Text))
        {
            // Перезаписываем поля у изменяемого объекта
            editUser.NumberPassengers = Convert.ToInt32(nameTBox.Text);
            editUser.IdDriverNavigation = RoleCBox.SelectedItem as Driver;
            editUser.IdCarNavigation = RoleCBox2.SelectedItem as Car;
            editUser.IdItineraryNavigation = RoleCBox3.SelectedItem as Itinerary;

            // Сохраним изменения
            Service.GetDbContext().SaveChanges();
            Close();
        }
        else
        {
            var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow("Ошибка", "Поле с количеством пассажиров пустое");
            messageBoxStandardWindow.Show();
        }
    }

    // Данный метод срабатывает при нажатии на кнопку Назад
    // Вернет пользователя на окно авторизации
    private void BackBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Закрываем текущее окно
        Close();
    }
}