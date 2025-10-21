using System;
using System.Data.Entity;
using UserRegistrationApp.Models;
using HashPasswords; // Подключаем нашу библиотеку для хеширования

namespace UserRegistrationApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Создание новой учетной записи для пользователя\n");

            Console.Write("Введите имя пользователя: ");
            string firstName = Console.ReadLine();

            Console.Write("Введите фамилию пользователя: ");
            string lastName = Console.ReadLine();

            Console.Write("Введите логин пользователя: ");
            string login = Console.ReadLine();

            Console.Write("Введите пароль пользователя: ");
            string password = Console.ReadLine();

            // 1. Хешируем пароль с помощью метода из нашей библиотеки
            string hashedPassword = Hash.GenerateHash(password);
            Console.WriteLine($"Хешированный пароль пользователя: {hashedPassword}");

            // 2. Сохраняем пользователя в базу данных
            try
            {
                // Создаем экземпляр нашего контекста базы данных
                using (var dbContext = new Entities())
                {
                    // Создаем новый объект пользователя
                    Users newUser = new Users
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Login = login,
                        PasswordHash = hashedPassword,
                        Role = "User" // Можно задать роль по умолчанию
                    };

                    // Добавляем пользователя в контекст
                    dbContext.Users.Add(newUser);

                    // Сохраняем изменения в базе данных
                    dbContext.SaveChanges();
                }

                Console.WriteLine("Учетная запись добавлена");
            }
            catch (Exception ex)
            {
                // Обрабатываем возможные ошибки (например, неуникальный логин)
                Console.WriteLine($"\nПроизошла ошибка при добавлении пользователя: {ex.Message}");
                // Для отладки можно посмотреть внутреннее исключение
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Подробности: {ex.InnerException.Message}");
                }
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}