using System;
using System.Runtime.InteropServices;
using System.Text;

namespace sp_keygen
{
    internal class Program
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool GetCurrentHwProfile(IntPtr fProfile);

        [StructLayout(LayoutKind.Sequential)]
        class HWProfile
        {
            public Int32 dwDockInfo;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 39)]
            public string szHwProfileGuid;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szHwProfileName;
        }

        static void Main()
        {
            Console.Write("Name: ");
            string? name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Please input name.");
                return;
            }
            HWProfile hwProfile = new();
            IntPtr hwProfilePtr = Marshal.AllocHGlobal(Marshal.SizeOf(hwProfile));
            Marshal.StructureToPtr(hwProfile, hwProfilePtr, false);
            GetCurrentHwProfile(hwProfilePtr);
            hwProfile = Marshal.PtrToStructure<HWProfile>(hwProfilePtr);
            Marshal.FreeHGlobal(hwProfilePtr);
            string guid = hwProfile.szHwProfileGuid.Replace("-", "").Replace("{", "").Replace("}", "");
            string base64 = name + Convert.ToBase64String(Encoding.UTF8.GetBytes(guid));
            byte[] hash = System.Security.Cryptography.SHA1.HashData(Encoding.UTF8.GetBytes(base64));
            byte[] halfHash = new byte[10];
            Array.Copy(hash, 10, halfHash, 0, 10);
            string hexHash = BitConverter.ToString(halfHash).Replace("-", "");
            int sum = 0;
            foreach (char c in name)
                sum += c;
            Console.WriteLine($"GUID: {guid}");
            Console.WriteLine($"Base64: {base64}");
            Console.WriteLine($"Hash: {hexHash}");
            Console.WriteLine($"Serial: {hexHash}{sum * (name.Length + 5)}");
            Console.ReadKey();
        }
    }
}
