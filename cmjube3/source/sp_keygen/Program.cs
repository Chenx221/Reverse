using System.Text;

class Program
{
    static void Main()
    {
        CalcSerial(CalcSerial2());
        Console.ReadKey();
    }

    public static void CalcSerial(uint eax)
    {
        uint ebx = eax;
        ebx = (ebx & 0xFFFF0000) | ((ebx & 0xFFFF) ^ 0xDEAD);
        ebx ^= 0x43211234;
        ebx = ROL(ebx, 6);
        uint ecx = ~ebx + 1;
        ecx ^= 0x89AAB776;
        ecx ^= 0x3081983;
        ecx = ROL(ecx, 6);
        ecx ^= 0x11223344;

        string serial = Parse2String(BitConverter.GetBytes(eax)) + Parse2String(BitConverter.GetBytes(ecx));
        Console.WriteLine(serial);
    }

    private static string Parse2String(byte[] bytes)
    {
        StringBuilder sb = new();
        foreach (byte b in bytes)
        {
            if (b >= 32 && b <= 126)
                sb.Append((char)b);
            else
                throw new Exception("Something wrong");
        }
        return sb.ToString();
    }

    public static uint CalcSerial2()
    {
        uint v1 = 0x5E11E222, v2 = 0x9AAD6A70;
        uint eax = ~v1 + 1, eax_2 = ~v2 + 1;
        eax ^= 0xCCCCCCCC; eax_2 ^= 0xDDDDDDDD;
        eax = ROR(eax, 6); eax_2 = ROR(eax_2, 7);
        eax -= 0x11111111; eax_2 -= 0x22222222;
        eax = ROR(eax, 6); eax_2 = ROR(eax_2, 7);
        eax ^= 0xA0A0A0A0; eax_2 ^= 0xB0B0B0B0;
        return eax;
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
}
