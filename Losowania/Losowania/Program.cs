using System;

class Program
{
    static void Main(string[] args)
    {
        string[] kolory = { "Zielony", "Czerwony", "Niebieski", "Żółty", "Fioletowy", "Pomarańczowy", "Różowy", "Indygo" };
        Random random = new Random();

        Console.WriteLine("========== Z POWTÓRZENIAMI ==========\n");

        for (int i = 0; i < 20; i++)
        {
            string[] wylosowany = Enumerable.Range(0,4).Select(_ => kolory[random.Next(kolory.Length)]).ToArray();
            Console.WriteLine("Wylosowane kolory: " + string.Join(",", wylosowany));
        }

        Console.WriteLine("\n========== BEZ POWTÓRZEŃ ==========\n");

        for (int i = 0; i < 20; i++)
        {
            string[] wylosowany = kolory.OrderBy(x => random.Next()).Take(4).ToArray();
            Console.WriteLine("Wylosowane kolory: " + string.Join(",", wylosowany));
        }
    }
}