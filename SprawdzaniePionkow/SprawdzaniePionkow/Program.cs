using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("\n");

        Random random = new Random();
        string[] kolory = { "Czerwony", "Zielony", "Niebieski", "Żółty" };

        for (int x = 0; x < 20; x++)
        {
            string[] wylosowane = Enumerable.Range(0, 4)
            .Select(_ => kolory[random.Next(kolory.Length)])
            .ToArray();

            string[] wlozone = Enumerable.Range(0, 4)
            .Select(_ => kolory[random.Next(kolory.Length)])
            .ToArray();
        
        
            Console.WriteLine("\n");
            Console.WriteLine("Wylosowane: " + string.Join(", ", wylosowane));
            Console.WriteLine("Włożone: " + string.Join(", ", wlozone));

            bool[] czysa = new bool[4];
            bool[] uzyte = new bool[4];

            for (int i = 0; i < wlozone.Length; i++)
            {
                if (wlozone[i] == wylosowane[i])
                {
                    czysa[i] = true;
                    uzyte[i] = true;
                }
            }

            for (int i = 0; i < wlozone.Length; i++)
            {
                if (czysa[i] != true)
                {
                    for (int j = 0; j < wylosowane.Length; j++)
                    {
                        if (!uzyte[j] && wlozone[i] == wylosowane[j])
                        {
                            czysa[i] = false;
                            uzyte[j] = true;
                            break;
                        }
                    }
                }
            }
            Console.WriteLine("Dostałem: " + string.Join(" : ", czysa));
        }
    }
}
