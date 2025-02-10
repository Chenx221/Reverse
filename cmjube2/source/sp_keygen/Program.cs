using System.Text;

class Program
{
    static void Main()
    {
        Console.Write("Enter Name: ");
        string? name = Console.ReadLine();
        if (string.IsNullOrEmpty(name))
            Console.Write("Invalid Name");
        else
            CalcSerial(name);
        Console.Write("OK");
    }

    public static void CalcSerial(string name)
    {
        byte[] serial = new byte[8 + name.Length];
        Encoding.ASCII.GetBytes("Jube").CopyTo(serial, 0);
        uint sum = 0;
        for (int i = 0; i < name.Length; i++)
        {
            serial[8 + i] = (byte)(name[i] ^ 0x23);
            sum += serial[8 + i];
        }
        BitConverter.GetBytes(sum ^ 0x87654321).CopyTo(serial, 4);
        File.WriteAllBytes("crk2.a", serial);
    }
}
