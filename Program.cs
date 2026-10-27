using System;
using System.Text;
using System.Text.Encodings.Web;
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

        Product product = new Product
        {
            Id = 1,
            Name = "Ноутбук",
            Price = 35999,
            Quantity = 5
        };

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        string json = JsonSerializer.Serialize(product, options);

        Console.WriteLine(json);
    }
}