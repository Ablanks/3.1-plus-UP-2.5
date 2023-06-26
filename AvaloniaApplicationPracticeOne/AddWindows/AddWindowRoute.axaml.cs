using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplicationPracticeOne.Models;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplicationPracticeOne;

public partial class AddWindowRoute : Window
{

    private TextBox nameTBox;

    public AddWindowRoute()
    {
        InitializeComponent();

        nameTBox = this.FindControl<TextBox>("NameTBox");
        RoleCBox = this.FindControl<ComboBox>("RoleCBox");
        RoleCBox2 = this.FindControl<ComboBox>("RoleCBox2");
        RoleCBox3 = this.FindControl<ComboBox>("RoleCBox3");
        RoleCBox.Items = Service.GetDbContext().Driver.ToList();
        RoleCBox2.Items = Service.GetDbContext().Car.ToList();
        RoleCBox3.Items = Service.GetDbContext().Itineraries.ToList();

#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    // Данный метод срабатыват при нажатии на кнопку Зарегистироваться
    private void RegBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Создадим экземпляр класса User
            var newUser = new Route()
            {
                Id = Service.GetDbContext().Routes.Max(q=>q.Id) + 1,
                IdDriverNavigation = RoleCBox.SelectedItem as Driver,
                IdCarNavigation = RoleCBox2.SelectedItem as Car,
                IdItineraryNavigation = RoleCBox3.SelectedItem as Itinerary,
                NumberPassengers = Convert.ToInt32(nameTBox.Text)
            };
            // Добавим нового пользователя
            Service.GetDbContext().Routes.Add(newUser);
            // Сохраним изменения
            Service.GetDbContext().SaveChanges();
            
            // Вернемся на окно авторизации
            new AdminWindowRoute().Show();
            Close();
    }

    // Данный метод срабатывает при нажатии на кнопку Назад
    // Вернет пользователя на окно авторизации
    private void BackBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Открываем окно MainWindow
        new AdminWindowRoute().Show();
        // Закрываем текущее окно
        Close();
    }
}