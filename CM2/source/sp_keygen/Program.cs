using System.Text;

class Program
{
    static void Main()
    {
        Console.Write("Enter Name: ");
        string? name = Console.ReadLine();
        //Check
        if (string.IsNullOrEmpty(name) || name.Length <= 4 || name.Length >= 60 || !IsAscii(name))
            Console.Write("Invalid Name");
        else
        {
            CalcSerial(name);
        }
        Console.ReadKey();
    }

    public static void CalcSerial(string name)
    {
        const string keyDatabase = "1AG4T3CX8ZF7R95Q";
        StringBuilder sb = new();

        sb.Append(keyDatabase[name[0] % 0x10]);
        sb.Append(keyDatabase[name[1] % 0x10]);
        sb.Append('-');
        sb.Append($"{GetAscii(name):X8}");
        sb.Append('-');
        sb.Append(keyDatabase[name[^2] % 0x10]);
        sb.Append(keyDatabase[name[^1] % 0x10]);

        Console.WriteLine(sb.ToString());
    }

    //public static bool Check()
    //{
    //    return true;
    //}

    public static bool IsAscii(string str)
    {
        foreach (char c in str)
        {
            if (c > 127)
                return false;
        }
        return true;
    }

    public static int GetAscii(string str)
    {
        int ascii = 0;
        foreach (char c in str)
        {
            ascii += c;
        }
        return ascii;
    }
}
