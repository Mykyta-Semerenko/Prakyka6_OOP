
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public int Year { get; set; }
}

class Program
{
    private const string FileName = "books.json";

    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        List<Book> books = LoadBooks();

        while (true)
        {
            Console.WriteLine("\n=== КАТАЛОГ КНИГ ===");
            Console.WriteLine("1. Додати книгу");
            Console.WriteLine("2. Показати всі книги");
            Console.WriteLine("3. Знайти книгу");
            Console.WriteLine("4. Видалити книгу");
            Console.WriteLine("5. Зберегти каталог");
            Console.WriteLine("0. Вихід");
            Console.Write("Ваш вибір: ");

            switch (Console.ReadLine())
            {
                case "1":
                    AddBook(books);
                    break;
                case "2":
                    ShowBooks(books);
                    break;
                case "3":
                    SearchBook(books);
                    break;
                case "4":
                    DeleteBook(books);
                    break;
                case "5":
                    SaveBooks(books);
                    break;
                case "0":
                    SaveBooks(books);
                    Console.WriteLine("До побачення!");
                    return;
                default:
                    Console.WriteLine("Невірний пункт меню.");
                    break;
            }
        }
    }

    static List<Book> LoadBooks()
    {
        try
        {
            if (!File.Exists(FileName))
                return new List<Book>();

            string json = File.ReadAllText(FileName, Encoding.UTF8);

            return JsonSerializer.Deserialize<List<Book>>(json)
                   ?? new List<Book>();
        }
        catch (JsonException)
        {
            Console.WriteLine("Некоректний JSON у файлі каталогу.");
            return new List<Book>();
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Помилка читання: {ex.Message}");
            return new List<Book>();
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Немає доступу до файлу.");
            return new List<Book>();
        }
    }

    static void SaveBooks(List<Book> books)
    {
        try
        {
            string json = JsonSerializer.Serialize(books, Options);

            File.WriteAllText(FileName, json, Encoding.UTF8);

            Console.WriteLine("Каталог збережено.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Помилка збереження: {ex.Message}");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Немає дозволу на запис файлу.");
        }
    }

    static void AddBook(List<Book> books)
    {
        Console.Write("Назва книги: ");
        string title = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Автор: ");
        string author = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Рік видання: ");

        if (!int.TryParse(Console.ReadLine(), out int year) || year < 0)
        {
            Console.WriteLine("Некоректний рік.");
            return;
        }

        if (string.IsNullOrWhiteSpace(title) ||
            string.IsNullOrWhiteSpace(author))
        {
            Console.WriteLine("Назва й автор обов'язкові.");
            return;
        }

        int nextId = books.Count == 0 ? 1 : books.Max(b => b.Id) + 1;

        books.Add(new Book
        {
            Id = nextId,
            Title = title,
            Author = author,
            Year = year
        });

        SaveBooks(books);
    }

    static void ShowBooks(List<Book> books)
    {
        if (books.Count == 0)
        {
            Console.WriteLine("Каталог порожній.");
            return;
        }

        foreach (Book book in books)
        {
            Console.WriteLine(
                $"{book.Id}. {book.Title} — {book.Author}, {book.Year}");
        }
    }

    static void SearchBook(List<Book> books)
    {
        Console.Write("Назва або її частина: ");
        string query = Console.ReadLine()?.Trim() ?? "";

        var results = books.Where(b =>
            b.Title.Contains(query, StringComparison.OrdinalIgnoreCase));

        foreach (Book book in results)
        {
            Console.WriteLine(
                $"{book.Id}. {book.Title} — {book.Author}, {book.Year}");
        }

        if (!results.Any())
            Console.WriteLine("Книг не знайдено.");
    }

    static void DeleteBook(List<Book> books)
    {
        Console.Write("ID книги для видалення: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID має бути цілим числом.");
            return;
        }

        Book? book = books.FirstOrDefault(b => b.Id == id);

        if (book == null)
        {
            Console.WriteLine("Книгу не знайдено.");
            return;
        }

        books.Remove(book);
        SaveBooks(books);
        Console.WriteLine("Книгу видалено.");
    }
}
