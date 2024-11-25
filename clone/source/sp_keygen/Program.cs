using System.Globalization;
using System.Text;

class Program
{
    static void Main()
    {
        //Check("chenx221", "12345678"); // debug

        Console.Write("Enter name: ");
        string? name = Console.ReadLine();
        if (name == null)
            throw new ArgumentNullException(nameof(name), "Name cannot be null.");
        CalcSerial(name);
        Console.ReadKey();
    }

    public static void CalcSerial(string user)
    {
        if (!CheckStrLen(user, 5, 0))
            throw new ArgumentException("Invalid user string length.");

        uint eax, ecx;
        ushort cx;
        byte cl, ch, dl = 0;
        byte[] userBytes;

        foreach (char c in user[4..])
        {
            dl += (byte)c;
        }
        ecx = (uint)(dl << 24) | (uint)(dl << 16) | (uint)(dl << 8) | dl;
        userBytes = Encoding.ASCII.GetBytes(user[..4]);
        eax = BitConverter.ToUInt32(userBytes, 0);
        ecx ^= eax;
        ecx = BSwap(ecx);
        ecx += 0x3022006;
        ecx = BSwap(ecx);
        ecx -= 0xDEADC0DE;
        ecx = BSwap(ecx);
        cl = (byte)((ecx & 0xFF) + 1);
        ecx = (ecx & 0xFFFFFF00) | cl;
        ch = (byte)(((ecx >> 8) & 0xFF) + 1);
        ecx = (ecx & 0xFFFF00FF) | ((uint)ch << 8);
        ecx = BSwap(ecx);
        cl = (byte)((ecx & 0xFF) - 1);
        ecx = (ecx & 0xFFFFFF00) | cl;
        ch = (byte)(((ecx >> 8) & 0xFF) - 1);
        ecx = (ecx & 0xFFFF00FF) | ((uint)ch << 8);
        ecx = BSwap(ecx);
        ecx ^= 0xEDB88320;
        ecx = BSwap(ecx);
        ecx += 0xD76AA478;
        ecx = BSwap(ecx);
        ecx -= 0xB00BFACE;
        ecx = BSwap(ecx);
        ecx += 0xBADBEEF;
        ecx = BSwap(ecx);
        ecx++;
        ecx = BSwap(ecx);
        ecx--;
        ecx = BSwap(ecx);
        ecx += eax;
        ecx = BSwap(ecx);
        cx = (ushort)((ecx & 0xFFFF) + 1);
        ecx = (ecx & 0xFFFF0000) | cx;
        ecx = BSwap(ecx);
        cx = (ushort)((ecx & 0xFFFF) + 1);
        ecx = (ecx & 0xFFFF0000) | cx;
        ecx = BSwap(ecx); //true serial result

        //开始反向计算Serial
        eax = BSwap(ecx);
        int[] key = [0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF];
        int keyIndex = key.Length - 1;
        byte[] byteArray = new byte[4];
        for (int i = 3; i >= 0; i--)
        {
            byte temp = (byte)(eax & 0xFF);
            temp = (byte)(temp - key[keyIndex--]);
            temp = (byte)(temp ^ key[keyIndex--]);
            byteArray[i] = temp;
            eax >>= 8;
        }
        Console.WriteLine($"Serial: {BitConverter.ToString(byteArray).Replace("-", "")}");
    }

    public static bool Check(string user, string serial)
    {
        if (!(CheckStrLen(serial, 8, 8) && CheckStrLen(user, 5, 0)))
            return false;

        uint eax, ebx, ecx;
        ushort cx;
        byte cl, ch, dl = 0, bl;
        byte[] userBytes, serialBytes;

        foreach (char c in user[4..])
        {
            dl += (byte)c;
        }
        ecx = (uint)(dl << 24) | (uint)(dl << 16) | (uint)(dl << 8) | dl;
        userBytes = Encoding.ASCII.GetBytes(user[..4]);
        eax = BitConverter.ToUInt32(userBytes, 0);
        ecx ^= eax;
        ecx = BSwap(ecx);
        ecx += 0x3022006;
        ecx = BSwap(ecx);
        ecx -= 0xDEADC0DE;
        ecx = BSwap(ecx);
        cl = (byte)((ecx & 0xFF) + 1);
        ecx = (ecx & 0xFFFFFF00) | cl;
        ch = (byte)(((ecx >> 8) & 0xFF) + 1);
        ecx = (ecx & 0xFFFF00FF) | ((uint)ch << 8);
        ecx = BSwap(ecx);
        cl = (byte)((ecx & 0xFF) - 1);
        ecx = (ecx & 0xFFFFFF00) | cl;
        ch = (byte)(((ecx >> 8) & 0xFF) - 1);
        ecx = (ecx & 0xFFFF00FF) | ((uint)ch << 8);
        ecx = BSwap(ecx);
        ecx ^= 0xEDB88320;
        ecx = BSwap(ecx);
        ecx += 0xD76AA478;
        ecx = BSwap(ecx);
        ecx -= 0xB00BFACE;
        ecx = BSwap(ecx);
        ecx += 0xBADBEEF;
        ecx = BSwap(ecx);
        ecx++;
        ecx = BSwap(ecx);
        ecx--;
        ecx = BSwap(ecx);
        ecx += eax;
        ecx = BSwap(ecx);
        cx = (ushort)((ecx & 0xFFFF) + 1);
        ecx = (ecx & 0xFFFF0000) | cx;
        ecx = BSwap(ecx);
        cx = (ushort)((ecx & 0xFFFF) + 1);
        ecx = (ecx & 0xFFFF0000) | cx;
        ecx = BSwap(ecx); //true serial result

        serialBytes = ParseHstr(serial);
        ebx = (uint)(serialBytes[0] * 0x10 + serialBytes[1]);
        bl = (byte)(ebx & 0xFF);
        bl = (byte)((bl ^ 0x12) + 0x34);
        ebx = bl;
        eax = ebx;
        eax <<= 8;
        ebx = (uint)(serialBytes[2] * 0x10 + serialBytes[3]);
        bl = (byte)(ebx & 0xFF);
        bl = (byte)((bl ^ 0x56) + 0x78);
        ebx = bl;
        eax += ebx;
        eax <<= 8;
        ebx = (uint)(serialBytes[4] * 0x10 + serialBytes[5]);
        bl = (byte)(ebx & 0xFF);
        bl = (byte)((bl ^ 0x90) + 0xAB);
        ebx = bl;
        eax += ebx;
        eax <<= 8;
        ebx = (uint)(serialBytes[6] * 0x10 + serialBytes[7]);
        bl = (byte)(ebx & 0xFF);
        bl = (byte)((bl ^ 0xCD) + 0xEF);
        ebx = bl;
        eax += ebx;
        eax = BSwap(eax); //user input serial result
        return eax == ecx; //ecx<->ebx(crackme)
    }

    public static string ReverseString(string input)
    {
        if (input == null)
            throw new ArgumentNullException(nameof(input), "Input string cannot be null.");
        return new string(input.Reverse().ToArray());
    }

    public static bool CheckStrLen(string input, int min, int max)
    {
        int l = input.Length;
        if (min > 0 && l < min)
            return false;
        if (max > 0 && l > max)
            return false;
        return true;
    }

    public static uint BSwap(uint value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        Array.Reverse(bytes);
        return BitConverter.ToUInt32(bytes, 0);
    }

    public static byte[] ParseHstr(string hexString)
    {
        byte[] bytes = new byte[hexString.Length];
        for (int i = 0; i < hexString.Length; i++)
        {
            if (byte.TryParse(hexString[i].ToString(), NumberStyles.HexNumber, null, out byte value))
                bytes[i] = value;
            else
                throw new FormatException("Invalid hex string.");
        }
        return bytes;
    }
}
