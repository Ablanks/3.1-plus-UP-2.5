using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplicationPracticeOne.Models;

namespace AvaloniaApplicationPracticeOne;

public partial class AddWindowCar : Window
{
    private TextBox loginTBox;
    private TextBox passwordTBox;
    private TextBox nameTBox;

    public AddWindowCar()
    {
        InitializeComponent();

        loginTBox = this.FindControl<TextBox>("NameTBox");
        passwordTBox = this.FindControl<TextBox>("StateTBox");
        nameTBox = this.FindControl<TextBox>("CountTBox");
        RoleCBox = this.FindControl<ComboBox>("RoleCBox");
        RoleCBox.Items = Service.GetDbContext().TypeCar.ToList();
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
        // Проверяем, чтобы все поля были заполнены
        if (!string.IsNullOrWhiteSpace(loginTBox.Text) &&
            !string.IsNullOrWhiteSpace(passwordTBox.Text))
        {
            // Создадим экземпляр класса User
            var newUser = new Car()
            {
                Id = Service.GetDbContext().Car.Max(q=>q.Id) + 1,
                Name = loginTBox.Text,
                StateNumber = passwordTBox.Text,
                CountPassengers = Convert.ToInt32(nameTBox.Text),
                IdTypeCarNavigation = RoleCBox.SelectedItem as TypeCar
            };
            // Добавим нового пользователя
            Service.GetDbContext().Car.Add(newUser);
            // Сохраним изменения
            Service.GetDbContext().SaveChanges();
            
            // Вернемся на окно авторизации
            new AdminWindowCar().Show();
            Close();
        }
    }
    
    // Данный метод срабатывает при нажатии на кнопку Назад
    // Вернет пользователя на окно авторизации
    private void BackBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Открываем окно MainWindow
        new AdminWindowCar().Show();
        // Закрываем текущее окно
        Close();
    }
}