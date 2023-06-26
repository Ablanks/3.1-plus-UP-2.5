using AvaloniaApplicationPracticeOne.Models;

namespace AvaloniaApplicationPracticeOne;

public class Service
{
    // Переменная хранит экземпляр контекста
    private static AndreyPronContext? _db;

    // Метод, если экземпляр еще не создан, создает и возвращает его
    // Если экземпляр создан, возвращает его
    public static AndreyPronContext  GetDbContext()
    {
        if (_db == null)
        {
            _db = new AndreyPronContext();
        }
        return _db;
    }
}