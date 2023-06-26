using System.Linq;
using System.Net.Mime;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplicationPracticeOne.Models;
using Microsoft.EntityFrameworkCore;

namespace AvaloniaApplicationPracticeOne;

public partial class MainWindow : Window
{
    // В данные поля мы будем сохранять наши элементы
    private TextBox loginTBox;
    private TextBox passwordTBox;
    public MainWindow()
    {
        InitializeComponent();
        
        // Находим элемент по названию и сохраняем в соответствующее поле
        loginTBox = this.FindControl<TextBox>("LoginTBox");
        passwordTBox = this.FindControl<TextBox>("PasswordTBox");
        
    }

    // Данный метод срабатывает при нажатии на кнопку "AuthBtn"
    // При нажатии на данную кнопку мы будем брать данные из полей, которые ввел пользователь
    // и искать похожего в БД 
    private void AuthBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Проверим, чтобы в полях LoginTBox и PasswordTBox были данные
        if (!string.IsNullOrWhiteSpace(loginTBox.Text) && !string.IsNullOrWhiteSpace(passwordTBox.Text))
        {
            // Попытаемся найти пользователя в БД с указанными логином и паролем
            var userAuth = Service.GetDbContext().Users
                .FirstOrDefault(u => u.Login == loginTBox.Text && u.Password == passwordTBox.Text);
            // Если мы находим пользователя с такими данными и его роль "Пользователь"
            if (userAuth != null && userAuth.IdRole == 1)
            {
                // Откроем окно
                new UserTableSelectWindow().Show();
                // Закрыть текущеем окно
                Close();
            } 
            // Если мы находим пользователя с такими данными и его роль "Администратор"
            else if (userAuth != null && userAuth.IdRole == 2)
            {
                // Откроем окно
                new AdminTableSelectWindow().Show();
                // Закрыть текущеем окно
                Close();
            }
        }
    }

    // Данный метод срабатывает при нажатии на кнопку "RegBtn"
    // При нажатии на данную кнопку мы будем открывать окно авторизации и закрывать текущее
    private void RegBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Создаем экзепляр класса RegistrationWindow(создаем новое окно),
        // отображаем его с помощью метода Show()
        new RegistrationWindow().Show();
        // Закроем текущее окно MainWindow
        Close();
    }
}