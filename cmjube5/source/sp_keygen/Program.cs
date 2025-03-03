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
        uint v1 = 0xC4000002;
        uint v2 = ~v1 + 1;
        int times = 0x29A;
        for (int i = 0; i < times; i++)
        {
            v2 = ROR(v2, 1);
            v2--;
        }
        Console.WriteLine($"v2: {v2:X8}"); //0xD

        uint sum1 = 0x281D;
        sum1 = ROR(sum1 - 0x1939, 2);
        Console.WriteLine($"sum1: {sum1:X8}"); //0x3B9

        for (byte n = 1; n < 0x7F; n++) //key[8]
        {
            byte[] key = new byte[0xD];
            byte[] realKey = new byte[0xD];
            key[8] = (byte)(n + 0x5A);
            for (int m = 0; m < 0xD; m++)
            {
                key[m] = (byte)(key[8] - (8 - m));
            }
            uint n1 = BitConverter.ToUInt32(key, 0) - 0xC5C956BE;
            uint n2 = BitConverter.ToUInt32(key, 4) - 0xACC1ED77;
            uint n3 = BitConverter.ToUInt32(key, 9) - 0x6AA6AB87;
            int times2 = 0x3B9;
            for (int m = 0; m < times2; m++)
            {
                n1 = ROR(n1, 3);
                n1--;
                n2 = ROL(n2, 3);
                n2++;
                n3++;
                n3 = ROR(n3, 5);
                n3--;
            }
            n1 -= 0x4A754245; n2 -= 0x4A754245; n3 -= 0x4A754245;
            if (SumBytes(n1) + SumBytes(n2) + SumBytes(n3) + n == (int)sum1)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(n1), 0, realKey, 0, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(n2), 0, realKey, 4, 4);
                realKey[8] = n;
                Buffer.BlockCopy(BitConverter.GetBytes(n3), 0, realKey, 9, 4);
                Console.WriteLine($"key found, n = {n}, key: {Parse2String(realKey)}"); //n = 32
                break;
            }
        }

        //FAKE
        //int length = -1;
        //for (uint i = 1; i <= 100; i++)
        //{
        //    uint sum = 0;
        //    sum += 2 * i;
        //    for (uint j = 0; j < i; j++)
        //    {
        //        sum <<= 2;
        //        sum += 1;
        //        sum <<= 3;
        //        sum += i;
        //    }
        //    Console.WriteLine($"i: {i}, sum: {sum:X8}");
        //    if (sum == 0x318C6318)
        //    {
        //        Console.WriteLine($"满足条件的 i: {i}");
        //        length = (int)i;
        //        break;
        //    }
        //}

        //uint eax = 0xE1E1E7DA;
        //times = 0x7D0;
        //for (uint i = 0; i < times; i++)
        //{
        //    eax = ROR(eax, 8);
        //    eax -= 0x1983;
        //}

        //Console.WriteLine($"eax: {eax:X8}"); //0x5F9
        //Console.WriteLine($"random serial: {GenerateAsciiString((int)eax, length)}");

        //uint ebx = 0x34AAAAAC;
        //times = 0x3081983;
        //for (uint i = 0; i < times; i++)
        //{
        //    ebx = ROL(ebx, 5);
        //    ebx -= 4;
        //    ebx = ROR(ebx, 3);
        //    ebx -= 2;
        //}
        //Console.WriteLine($"ebx: {ebx:X8}"); //0x2E
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

    static int SumBytes(uint value)
    {
        return (byte)(value & 0xFF) +
               (byte)((value >> 8) & 0xFF) +
               (byte)((value >> 16) & 0xFF) +
               (byte)((value >> 24) & 0xFF);
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

    static string GenerateAsciiString(int targetSum, int length)
    {
        if (length <= 0 || targetSum < length * 32 || targetSum > length * 126)
        {
            throw new ArgumentException("无法生成符合要求的字符串，请调整参数。");
        }

        Random rand = new Random();
        char[] chars = new char[length];

        int remainingSum = targetSum;
        for (int i = 0; i < length; i++)
        {
            int minValue = Math.Max(32, remainingSum - (126 * (length - i - 1)));
            int maxValue = Math.Min(126, remainingSum - (32 * (length - i - 1)));
            chars[i] = (char)rand.Next(minValue, maxValue + 1);
            //FAKE
            //if(i==3 && chars[3] % 2 != 0)
            //{
            //    i--;
            //    continue;
            //}
            //if (i == 3 && (chars[i] == ' ' || chars[3] % 2 != 0))
            //{
            //    i--;
            //    continue;
            //}
            remainingSum -= chars[i];
        }

        return new string(chars);
    }

    private static uint GetAsciiSum(string str)
    {
        uint sum = 0;
        foreach (char c in str)
        {
            sum += c;
        }
        return sum;
    }
}
