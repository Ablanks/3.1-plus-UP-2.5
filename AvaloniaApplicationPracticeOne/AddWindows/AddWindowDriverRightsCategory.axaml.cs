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

public partial class AddWindowDriverRightsCategory : Window
{

    
    public AddWindowDriverRightsCategory()
    {
        InitializeComponent();

        RoleCBox = this.FindControl<ComboBox>("RoleCBox");
        RoleCBox2 = this.FindControl<ComboBox>("RoleCBox2");
        RoleCBox.Items = Service.GetDbContext().Driver.ToList();
        RoleCBox2.Items = Service.GetDbContext().RightsCategory.ToList();
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
        
        var selectedDriver = RoleCBox.SelectedItem as Driver;
        var selectedCategory = RoleCBox2.SelectedItem as RightCategory;
        var dbContext = Service.GetDbContext();
        var existingRecord = dbContext.DriverRightsCategories.FirstOrDefault(q =>
            q.IdDriverNavigation == selectedDriver &&
            q.IdRightsCategoryNavigation == selectedCategory);

        if (existingRecord == null)
        {
            // Создадим экземпляр класса User
            var newUser = new DriverRightsCategory()
            {
                IdDriverNavigation = RoleCBox.SelectedItem as Driver,
                IdRightsCategoryNavigation = RoleCBox2.SelectedItem as RightCategory,
            };



            // Добавим нового пользователя
            Service.GetDbContext().DriverRightsCategories.Add(newUser);
            // Сохраним изменения
            Service.GetDbContext().SaveChanges();

            // Вернемся на окно авторизации
            new AdminWindowDriverRightsCategory().Show();
            Close();
        }
        else
        {
            var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow("Ошибка", "У данного водителя уже есть эта категория прав");
            messageBoxStandardWindow.Show();
        }
        
    }

    // Данный метод срабатывает при нажатии на кнопку Назад
    // Вернет пользователя на окно авторизации
    private void BackBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        // Открываем окно MainWindow
        new AdminWindowDriverRightsCategory().Show();
        // Закрываем текущее окно
        Close();
    }
}