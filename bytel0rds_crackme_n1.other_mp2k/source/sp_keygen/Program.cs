using System.Text.RegularExpressions;
namespace sp_keygen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: sp_keygen.exe <Name> <RegKey>");
                return;
            }
            string Name = args[0];
            string RegKey = args[1];
            string pattern = @"^[1-9]{3}-[1-9]{3}-[1-9]{3}-[A-Z]$";
            if (Name.Length < 5 || RegKey.Length != 13 || !Regex.IsMatch(RegKey, pattern))
            {
                Console.WriteLine("Invalid input");
                return;
            }
            for (int serialNum = 11111; serialNum <= 99999; serialNum++)
            {
                string Serial = serialNum.ToString();
                string pattern2 = @"^[1-9]+$";
                if (!Regex.IsMatch(Serial.ToString(), pattern2))
                    continue;
                char[] charArray = Name.ToCharArray();
                Array.Reverse(charArray);
                int charSum = 0;
                foreach (char c in charArray)
                {
                    charSum += c;
                }
                string csbin = Convert.ToString(charSum, 2);
                char[] csbinArray = csbin.ToCharArray();
                int n1Num = 0;
                foreach (char c in csbinArray)
                {
                    if (c == '1')
                        n1Num++;
                }
                int regSum = 0;
                for (int i = 0; i < RegKey.Length; i++)
                {
                    if (i == 3 || i == 7 || i == 11)
                        continue;
                    else if (i == 12)
                    {
                        regSum += RegKey[i] % 2;
                        continue;
                    }
                    int n = int.Parse(RegKey[i].ToString());
                    if (i < 3)
                        regSum += n * (0xB - (i + 1));
                    else if (i < 7)
                        regSum += n * (0xC - (i + 1));
                    else if (i < 11)
                        regSum += n * (0xD - (i + 1));
                }
                double Sum = Math.Pow(regSum, n1Num);
                long Suml = (long)Sum;
                int mod = (int)(Suml % serialNum);
                if (IsPrime(mod))
                {
                    Console.WriteLine("Valid Serial: " + Serial);
                    break;
                }
            }
        }
        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number <= 3) return true;
            if (number % 2 == 0 || number % 3 == 0) return false;

            for (int i = 5; i * i <= number; i += 6)
            {
                if (number % i == 0 || number % (i + 2) == 0) return false;
            }

            return true;
        }
    }
}
