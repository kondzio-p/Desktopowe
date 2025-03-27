using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Random random = new Random();
        string[] Zawodnicy = { "Anna", "Jan", "Adam", "Anna", "Zenon", "Zosia", "Jurek", "Daniel", "Gosia", "Alicja" };


        // Podział na dziewczyny i chłopców 

        var dziewczyny = new List<string>();
        var chlopcy = new List<string>();
        foreach (var zawodnik in Zawodnicy)
        {
            if (zawodnik.EndsWith("a"))
            {
                dziewczyny.Add(zawodnik);
            }
            else
            {
                chlopcy.Add(zawodnik);
            }
        }
        


        if (dziewczyny.Count < 3 || chlopcy.Count < 3)
        {
            Console.WriteLine("Brakuje zawodnikow");
            return;
        }

        // 3 dziewczyny i 3 chłopców
        var reprezentacjaDziewczyny = dziewczyny.OrderBy(x => random.Next()).Take(3).ToList();
        var reprezentacjaChlopcy = chlopcy.OrderBy(x => random.Next()).Take(3).ToList();

        // Konkat chlopakow i dziewczyn
        var reprezentacja = reprezentacjaDziewczyny.Concat(reprezentacjaChlopcy).ToList();


        // Wypisanie reprezentacji pojedynczo -----------------------------------
        /*
            Console.WriteLine("Reprezentacja:");
            foreach (var zawodnik in reprezentacja)
            {
                Console.WriteLine(zawodnik);
            }
        */
        //-----------------------------------------------------------------------

        // Metoda wypisujaca 10 razy wylosowane zestawy reprezentacji - zrob jako funkcja
        void WypiszZestawy()
        {
            for (int i = 0; i < 10; i++)
            {
                reprezentacjaDziewczyny = dziewczyny.OrderBy(x => random.Next()).Take(3).ToList();
                reprezentacjaChlopcy = chlopcy.OrderBy(x => random.Next()).Take(3).ToList();
                reprezentacja = reprezentacjaDziewczyny.Concat(reprezentacjaChlopcy).ToList();
                Console.WriteLine("Reprezentacja " + (i + 1) + ":");
                foreach (var zawodnik in reprezentacja)
                {
                    Console.WriteLine(zawodnik);
                }
                Console.WriteLine("-------------------------------------------------");
            }
        }

        WypiszZestawy();

    }
}