using System;
class Program
{
    static void Main(string[] args)
    {
        TimeZoneInfo PLzone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
        Console.WriteLine(PLzone);
        Console.WriteLine("Czas lokoalny: {0}", DateTime.Now);

        // Zima
        DateTime zimowa = new DateTime(2024, 12, 21, 0, 0, 0);

        //Lato
        DateTime letnia = new DateTime(2024, 6, 29, 0, 0, 0);

        TimeSpan zimoweOffset = PLzone.GetUtcOffset(zimowa);
        TimeSpan letniaOffset = PLzone.GetUtcOffset(letnia);

        Console.WriteLine($"Dla daty {zimowa:yyyy-MM-dd}, offset dla zimy to: {zimoweOffset}");
        Console.WriteLine($"Dla daty {letnia:yyyy-MM-dd}, offset dla lata to: {letniaOffset}");
    }
}