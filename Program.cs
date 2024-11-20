using LibraryManagement.Managers;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;

internal class Program
{
    static void Main(string[] args)
    {
        ReaderManager readerManager = new ReaderManager();
        string command;

        do
        {
            Console.WriteLine("Введите команду (add - добавить читателя, list - список читателей, remove - удалить читателя, exit - выход):");
            command = Console.ReadLine().ToLower();

            switch (command)
            {
                case "add":

                    AddReader(readerManager);
                    break;

                case "list":
                    ListReaders(readerManager);
                    break;

                case "remove":
                    RemoveReader(readerManager);
                    break;

                case "exit":
                    Console.WriteLine("Выход из программы.");
                    break;

                default:
                    Console.WriteLine("Неверная команда. Пожалуйста, попробуйте снова.");
                    break;
            }
        } while (command != "exit");
    }

    private static void AddReader(ReaderManager readerManager)
    {
        Console.WriteLine("Введите имя читателя:");
        string name = Console.ReadLine();

        Console.WriteLine("Введите электронную почту читателя:");
        string email = Console.ReadLine();

        try
        {
            readerManager.AddReader(new Reader(null, name, email));
            Console.WriteLine("Читатель успешно добавлен.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }

    private static void ListReaders(ReaderManager readerManager)
    {
        List<Reader> allReaders = readerManager.GetAllReaders();

        if (allReaders.Count == 0)
        {
            Console.WriteLine("Список читателей пуст.");
            return;
        }

        Console.WriteLine("Список читателей:");
        foreach (var r in allReaders)
        {
            Console.WriteLine(r); // Используем метод ToString()  
        }
    }

    private static void RemoveReader(ReaderManager readerManager)
    {
        Console.WriteLine("Введите ID читателя для удаления:");
        string readerId = Console.ReadLine();

        try
        {
            readerManager.RemoveReader(readerId);
            Console.WriteLine("Читатель успешно удалён.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}
