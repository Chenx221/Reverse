namespace sp_keygen
{
    internal class Program
    {
        static void Main()
        {
            DateTime now = DateTime.Now;
            int year = now.Year;
            int month = now.Month;
            int day = now.Day;
            Console.WriteLine($"Serial: {(year * 0x7D2) + ((day << 5) - day) + ((month << 2) * 3) + 0xFF3C}");
            Console.ReadKey();
        }
    }
}
