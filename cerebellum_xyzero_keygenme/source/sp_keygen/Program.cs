using System.Runtime.InteropServices;
using System.Text;

namespace sp_keygen
{
    internal class Program
    {
        // P/Invoke declaration for GetComputerNameA
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool GetComputerNameA(StringBuilder lpBuffer, ref uint nSize);

        // P/Invoke declaration for GetUserNameA
        [DllImport("advapi32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool GetUserNameA(StringBuilder lpBuffer, ref uint nSize);

        static void Main()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine() ?? throw new Exception("Not a valid name");

            StringBuilder computerName = new(256);
            StringBuilder userName = new(256);

            uint size1 = (uint)computerName.Capacity;
            uint size2 = (uint)userName.Capacity;
            if (GetComputerNameA(computerName, ref size1) && GetUserNameA(userName, ref size2))
                CalcSerial(name, computerName.ToString(), userName.ToString());
            else
                Console.WriteLine("Something Wrong");
            Console.ReadKey();
        }

        public static void CalcSerial(string name, string computerName, string userName)
        {
            int v8 = 0x1791117;
            int v18 = name.Length;
            int v4 = 0;
            int vc = 0;
            int esi;
            int eax = 0x20;
            string cuu = ReverseString(computerName + userName).ToUpper();
            if (v18 < 4)
                return;
            foreach (char c in name)
            {
                v4 += c + v8++;
            }
            esi = v18 * v4 + v8;
            foreach (char c in cuu)
            {
                vc += (c ^ eax++) * v18;
            }
            // long serial;
            // serial ^=vc;
            // vc+=esi;
            // if(vc == serial)
            Console.WriteLine((vc + esi) ^ vc);
        }

        public static string ReverseString(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input string cannot be null.");
            return new string(input.Reverse().ToArray());
        }


    }
}
