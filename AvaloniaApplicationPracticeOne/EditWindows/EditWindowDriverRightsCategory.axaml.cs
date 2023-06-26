using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplicationPracticeOne.Models;

namespace AvaloniaApplicationPracticeOne;

public partial class EditWindowDriverRightsCategory : Window
{
    private DriverRightsCategory editUser;
    
    public EditWindowDriverRightsCategory()
    {
        InitializeComponent();

#if DEBUG
        this.AttachDevTools();
#endif
    }

    // Дополнительный конструктор, который в качестве параметра принимает объект пользователя,
    // который нужно отредактировать
    public EditWindowDriverRightsCategory(DriverRightsCategory editUser)
    {
        InitializeComponent();
        
        RoleCBox = this.FindControl<ComboBox>("RoleCBox");
        RoleCBox2 = this.FindControl<ComboBox>("RoleCBox2");
        RoleCBox.Items = Service.GetDbContext().Driver.ToList();
        RoleCBox2.Items = Service.GetDbContext().RightsCategory.ToList();
        this.editUser = editUser;
        
        RoleCBox.SelectedItem = editUser.IdDriverNavigation;
        RoleCBox2.SelectedItem = editUser.IdRightsCategoryNavigation;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    // Данный метод срабатыват при нажатии на кнопку Зарегистироваться
    private void EditBtn_OnClick(object? sender, RoutedEventArgs e)
    {

        var selectedDriver = RoleCBox.SelectedItem as Driver;
        var selectedCategory = RoleCBox2.SelectedItem as RightCategory;
        var dbContext = Service.GetDbContext();
        var existingRecord = dbContext.DriverRightsCategories.FirstOrDefault(q =>
            q.IdDriverNavigation == selectedDriver &&
            q.IdRightsCategoryNavigation == selectedCategory);

        if (existingRecord == null)
        {
            // Перезаписываем поля у изменяемого объекта
            editUser.IdDriverNavigation = RoleCBox.SelectedItem as Driver;
            editUser.IdRightsCategoryNavigation = RoleCBox2.SelectedItem as RightCategory;

            // Сохраним изменения
            Service.GetDbContext().SaveChanges();
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
        // Закрываем текущее окно
        Close();
    }
}