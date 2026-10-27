using System;
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

        string json = """
                      {
                          "Id": 2,
                          "Name": "Клавіатура",
                          "Price": 999,
                          "Quantity": 64
                      }
                      """;

        Product? product = JsonSerializer.Deserialize<Product>(json);

        if (product != null)
        {
            Console.WriteLine($"ID: {product.Id}");
            Console.WriteLine($"Назва: {product.Name}");
            Console.WriteLine($"Ціна: {product.Price}");
            Console.WriteLine($"Кількість: {product.Quantity}");
        }
    }
}