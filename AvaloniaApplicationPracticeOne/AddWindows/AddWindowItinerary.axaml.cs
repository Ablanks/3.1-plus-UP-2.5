using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplicationPracticeOne.Models;

namespace AvaloniaApplicationPracticeOne;

public partial class AddWindowItinerary : Window
{
    private TextBox nameTBox;

    
    public AddWindowItinerary()
    {
        InitializeComponent();

        nameTBox = this.FindControl<TextBox>("NameTBox");

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
        if (!string.IsNullOrWhiteSpace(nameTBox.Text))
        {
            // Создадим экземпляр класса User
            var newUser = new Itinerary()
            {
                Id = Service.GetDbContext().Itineraries.Max(q=>q.Id) + 1,
                Name = nameTBox.Text,

            };
            // Добавим нового пользователя
            Service.GetDbContext().Itineraries.Add(newUser);
            // Сохраним изменения
            Service.GetDbContext().SaveChanges();
            
            // Вернемся на окно авторизации
            new AdminWindowItinerary().Show();
            Close();
        }
    }

    // Данный метод срабатывает при нажатии на кнопку Назад
    // Вернет пользователя на окно авторизации
    private void BackBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Открываем окно MainWindow
        new AdminWindowItinerary().Show();
        // Закрываем текущее окно
        Close();
    }
}