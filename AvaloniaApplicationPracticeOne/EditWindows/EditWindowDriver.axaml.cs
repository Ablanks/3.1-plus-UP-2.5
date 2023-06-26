using System;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplicationPracticeOne.Models;

namespace AvaloniaApplicationPracticeOne;

public partial class EditWindowDriver : Window
{
    private TextBox nameTBox;
    private TextBox surnameTBox;
    private DatePicker birthdateDPicker;
    private Driver editUser;
    
    public EditWindowDriver()
    {
        InitializeComponent();

#if DEBUG
        this.AttachDevTools();
#endif
    }

    // Дополнительный конструктор, который в качестве параметра принимает объект пользователя,
    // который нужно отредактировать
    public EditWindowDriver(Driver editUser)
    {
        InitializeComponent();
        
        nameTBox = this.FindControl<TextBox>("NameTBox");
        surnameTBox = this.FindControl<TextBox>("StateTBox");
        birthdateDPicker = this.FindControl<DatePicker>("BirthdateDPicker");;
        this.editUser = editUser;
        
        nameTBox.Text = editUser.FirstName;
        surnameTBox.Text = editUser.LastName;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    // Данный метод срабатыват при нажатии на кнопку Зарегистироваться
    private void EditBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Проверяем, чтобы все поля были заполнены
        if (!string.IsNullOrWhiteSpace(surnameTBox.Text) &&
            !string.IsNullOrWhiteSpace(nameTBox.Text))
        {
            // Перезаписываем поля у изменяемого объекта
            editUser.FirstName = nameTBox.Text;
            editUser.LastName = surnameTBox.Text;
            editUser.Birthdate =
                birthdateDPicker.SelectedDate.Value.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
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