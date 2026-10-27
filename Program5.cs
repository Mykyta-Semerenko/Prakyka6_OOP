using System;
using System.IO;
using System.Text;
using System.Text.Json;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

class Program
{
    static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        try
        {
            string json = File.ReadAllText(
                "product.json", Encoding.UTF8);

            Product? product = JsonSerializer.Deserialize<Product>(json);

            if (product == null)
            {
                Console.WriteLine("Дані товару відсутні.");
                return;
            }

            Console.WriteLine($"Назва: {product.Name}");
            Console.WriteLine($"Ціна: {product.Price}");
            Console.WriteLine($"Кількість: {product.Quantity}");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл не знайдено.");
        }
        catch (JsonException)
        {
            Console.WriteLine("Некоректний формат JSON.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Помилка читання файлу: {ex.Message}");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Немає дозволу на читання файлу.");
        }
    }
}