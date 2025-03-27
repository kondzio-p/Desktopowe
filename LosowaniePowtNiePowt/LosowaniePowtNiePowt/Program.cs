using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Random random = new Random();
        string[] colors = { "Czerwony", "Zielony", "Niebieski", "Żółty", "Pomarańczowy", "Fioletowy", "Różowy", "Brązowy" };
        int count = 5; // Liczba kolorów do wylosowania

        // Losowanie z możliwością powtarzania się kolorów
        var colorsWithDuplicates = colors.OrderBy(c => random.Next()).Take(count).ToArray();
        Console.WriteLine("Losowanie z możliwością powtarzania się kolorów:");
        Console.WriteLine(string.Join(", ", colorsWithDuplicates));

        Console.WriteLine(); // Pusta linia dla czytelności

        // Losowanie bez powtarzania się kolorów
        var colorsWithoutDuplicates = colors.OrderBy(c => random.Next()).Distinct().Take(count).ToArray();
        Console.WriteLine("Losowanie bez powtarzania się kolorów:");
        Console.WriteLine(string.Join(", ", colorsWithoutDuplicates));
    }
}