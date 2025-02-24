using System.Text;

class Program
{
    static void Main()
    {
        CalcSerial();
        Console.ReadKey();
    }

    public static void CalcSerial()
    {
        uint v1 = 0xFF989A8E, v2 = 0x30003C5B, v3 = 0xD1ADAD9E, v4 = 0xC9C5F07B;
        uint eax = ~v1 + 1, eax_2 = ~v2 + 1, eax_3 = ~v3 + 1, eax_4 = ~v4 + 1;
        string serial = Parse2String(BitConverter.GetBytes(eax));
        string serial_2 = Parse2String(BitConverter.GetBytes(ROR(eax_2 ^ 0xDEADDEAD, 6)));
        string serial_3 = Parse2String(BitConverter.GetBytes(ROL(eax_3 ^ 0x88888888, 7)));
        string serial_4 = Parse2String(BitConverter.GetBytes(ROR(eax_4 ^ 0x12344321, 5)));
        Console.WriteLine($"Arguments:{serial}\nKey:{serial_2 + serial_3 + serial_4}");
    }

    static uint ROL(uint value, int shift)
    {
        shift = shift % 32;
        return (value << shift) | (value >> (32 - shift));
    }

    static uint ROR(uint value, int shift)
    {
        shift = shift % 32;
        return (value >> shift) | (value << (32 - shift));
    }

    private static string Parse2String(byte[] bytes)
    {
        StringBuilder sb = new();
        foreach (byte b in bytes)
        {
            if (b >= 32 && b <= 126)
                sb.Append((char)b);
            else if (b == 0)
                continue;
            else
                throw new Exception("Something wrong");
        }
        return sb.ToString();
    }
}
