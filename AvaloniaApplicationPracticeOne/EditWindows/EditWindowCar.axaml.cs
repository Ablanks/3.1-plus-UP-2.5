using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplicationPracticeOne.Models;

namespace AvaloniaApplicationPracticeOne;

public partial class EditWindowCar : Window
{
    private TextBox loginTBox;
    private TextBox passwordTBox;
    private TextBox nameTBox;
    private Car editUser;
    
    public EditWindowCar()
    {
        InitializeComponent();

#if DEBUG
        this.AttachDevTools();
#endif
    }

    // Дополнительный конструктор, который в качестве параметра принимает объект пользователя,
    // который нужно отредактировать
    public EditWindowCar(Car editUser)
    {
        InitializeComponent();
        
        loginTBox = this.FindControl<TextBox>("NameTBox");
        passwordTBox = this.FindControl<TextBox>("StateTBox");
        nameTBox = this.FindControl<TextBox>("CountTBox");
        RoleCBox = this.FindControl<ComboBox>("RoleCBox");
        RoleCBox.Items = Service.GetDbContext().TypeCar.ToList();
        this.editUser = editUser;
        
        loginTBox.Text = editUser.Name;
        passwordTBox.Text = editUser.StateNumber;
        RoleCBox.SelectedItem = editUser.IdTypeCarNavigation;
        
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    // Данный метод срабатыват при нажатии на кнопку Зарегистироваться
    private void EditBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Проверяем, чтобы все поля были заполнены
        if (!string.IsNullOrWhiteSpace(loginTBox.Text) &&
            !string.IsNullOrWhiteSpace(passwordTBox.Text) &&
            !string.IsNullOrWhiteSpace(nameTBox.Text))
        {
            // Перезаписываем поля у изменяемого объекта
            editUser.Name = loginTBox.Text;
            editUser.StateNumber = passwordTBox.Text;
            editUser.CountPassengers = Convert.ToInt32(nameTBox.Text);
            editUser.IdTypeCarNavigation = RoleCBox.SelectedItem as TypeCar;

            // Сохраним изменения
            Service.GetDbContext().SaveChanges();
            Close();
        }
        else
        {
            var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow("Ошибка", "Одно из полей не заполнено");
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