using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public double AverageGrade { get; set; }
}

class Program
{
    static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        List<Student> students = new()
        {
            new Student { Id = 1, Name = "Микола", AverageGrade = 65.1 },
            new Student { Id = 2, Name = "Богдан", AverageGrade = 71.8 },
            new Student { Id = 3, Name = "Марія", AverageGrade = 70.3 },
            new Student { Id = 4, Name = "Іван", AverageGrade = 79.2 },
            new Student { Id = 5, Name = "Дарія", AverageGrade = 83.7 }
        };

        string json = JsonSerializer.Serialize(students, options);
        File.WriteAllText("students.json", json, Encoding.UTF8);

        Console.WriteLine("Список студентів збережено.");

        string content = File.ReadAllText("students.json", Encoding.UTF8);

        List<Student> restored =
            JsonSerializer.Deserialize<List<Student>>(content)
            ?? new List<Student>();

        Student? best = restored
            .OrderByDescending(s => s.AverageGrade)
            .FirstOrDefault();

        if (best != null)
        {
            Console.WriteLine($"Найкращий студент: {best.Name}");
            Console.WriteLine($"Середній бал: {best.AverageGrade}");
        }
    }
}