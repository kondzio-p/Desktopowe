using System;

class Program
{
    static void Main(string[] args)
    {
        string[] wylosowane = { "Zielony", "Niebieski", "Czerwony", "Żółty" };
        string[] wlozone = { "Niebieski", "Zielony", "Czerwony", "Czerwony" };
        var czysa = new System.Collections.Generic.List<string>();

        for(int i = 0; i < wlozone.Length; i++)
        {
            int idx = Array.IndexOf(wylosowane, wlozone[i]);

            if (idx != -1 && idx == i)
            {
                czysa.Add("true");
            }
            else if (idx != -1)
            {
                czysa.Add("false");
            }
        }
        Console.WriteLine("Dostałem: " + string.Join(", ", czysa));
    }
}