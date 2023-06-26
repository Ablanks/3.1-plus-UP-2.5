using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplicationPracticeOne.Models;

namespace AvaloniaApplicationPracticeOne;

public partial class EditWindowTypeCar : Window
{

    private TextBox nameTBox;
    private TypeCar editUser;
    
    public EditWindowTypeCar()
    {
        InitializeComponent();

#if DEBUG
        this.AttachDevTools();
#endif
    }

    // Дополнительный конструктор, который в качестве параметра принимает объект пользователя,
    // который нужно отредактировать
    public EditWindowTypeCar(TypeCar editUser)
    {
        InitializeComponent();
        
   
        nameTBox = this.FindControl<TextBox>("NameTBox");
        this.editUser = editUser;
        nameTBox.Text = editUser.Name;
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
            editUser.Name = nameTBox.Text;

            // Сохраним изменения
            Service.GetDbContext().SaveChanges();
            Close();
        }
        else
        {
            var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow("Ошибка", "Поле с типом автомобиля пустое");
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