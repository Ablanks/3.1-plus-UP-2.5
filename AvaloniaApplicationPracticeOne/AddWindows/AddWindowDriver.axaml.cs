using System;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplicationPracticeOne.Models;

namespace AvaloniaApplicationPracticeOne;

public partial class AddWindowDriver : Window
{
    private TextBox nameTBox;
    private TextBox surnameTBox;
    private DatePicker birthdateDPicker;
    
    public AddWindowDriver()
    {
        InitializeComponent();

        nameTBox = this.FindControl<TextBox>("NameTBox");
        surnameTBox = this.FindControl<TextBox>("StateTBox");
        birthdateDPicker = this.FindControl<DatePicker>("BirthdateDPicker");
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
        if (!string.IsNullOrWhiteSpace(nameTBox.Text) &&
            !string.IsNullOrWhiteSpace(surnameTBox.Text))
        {
            // Создадим экземпляр класса User
            var newUser = new Driver()
            {
                Id = Service.GetDbContext().Driver.Max(q=>q.Id) + 1,
                FirstName = nameTBox.Text,
                LastName = surnameTBox.Text,
                Birthdate = birthdateDPicker.SelectedDate.Value.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
            };
            // Добавим нового пользователя
            Service.GetDbContext().Driver.Add(newUser);
            // Сохраним изменения
            Service.GetDbContext().SaveChanges();
            
            // Вернемся на окно авторизации
            new AdminWindowDriver().Show();
            Close();
        }
    }

    // Данный метод срабатывает при нажатии на кнопку Назад
    // Вернет пользователя на окно авторизации
    private void BackBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Открываем окно MainWindow
        new AdminWindowDriver().Show();
        // Закрываем текущее окно
        Close();
    }
}