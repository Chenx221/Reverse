using System.Runtime.InteropServices;
using System.Text;

namespace keygen4
{
    class Program
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_INFO
        {
            [MarshalAs(UnmanagedType.Struct)]
            public UnionDUMMYUNIONNAME DUMMYUNIONNAME;
            public uint dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public UIntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public ushort wProcessorLevel;
            public ushort wProcessorRevision;

            [StructLayout(LayoutKind.Explicit)]
            public struct UnionDUMMYUNIONNAME
            {
                [FieldOffset(0)]
                public uint dwOemId;

                [FieldOffset(0)]
                public ProcessorInfo processorInfo;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct ProcessorInfo
            {
                public ushort wProcessorArchitecture;
                public ushort wReserved;
            }
        }

        [DllImport("kernel32.dll")]
        public static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetVolumeInformation(string Volume, StringBuilder VolumeName,
            uint VolumeNameSize, out uint SerialNumber, out uint SerialNumberLength,
            out uint flags, StringBuilder fs, uint fs_size);

        static void Main()
        {
            SYSTEM_INFO sysInfo;
            GetSystemInfo(out sysInfo);

            Console.Write("Enter the drive letter where crackme is located (Example: C): ");
            string driveLetter = Console.ReadLine();
            string rootPath = driveLetter + ":\\";

            uint serialNum;
            StringBuilder volumename = new(256);
            StringBuilder fstype = new(256);

            bool result = GetVolumeInformation(rootPath, volumename,
                (uint)volumename.Capacity - 1, out serialNum, out _,
                out _, fstype, (uint)fstype.Capacity - 1);

            if (!result)
            {
                Console.WriteLine("Failed to retrieve volume information.");
                return;
            }

            Console.Write("Enter name: ");
            string name = Console.ReadLine();

            uint dwProcessorType = sysInfo.dwProcessorType;
            uint dwNumberOfProcessors = sysInfo.dwNumberOfProcessors;
            IntPtr lpMinimumApplicationAddress = sysInfo.lpMinimumApplicationAddress;
            IntPtr lpMaximumApplicationAddress = sysInfo.lpMaximumApplicationAddress;

            string serial = CalcSerial(name, (volumename == null) ? "EB" : volumename.ToString(), fstype.ToString(), serialNum, dwProcessorType, lpMinimumApplicationAddress, lpMaximumApplicationAddress, dwNumberOfProcessors);
            Console.WriteLine("Serial: " + serial);
            Console.ReadKey();
        }

        public static string CalcSerial(string name, string vol, string fs, uint VolumeSerialNumber, uint dwProcessorType, IntPtr lpMinimumApplicationAddress, IntPtr lpMaximumApplicationAddress, uint dwNumberOfProcessors)
        {
            string p1 = ((fs[0] + fs.Last()) * dwNumberOfProcessors).ToString("X");
            string p2 = (name[0] + name[^1]).ToString("X") + (name[1] + name[^2]).ToString("X");
            string p3 = "42";
            string p4 = C4(VolumeSerialNumber, dwNumberOfProcessors);
            string p5 = new(dwProcessorType.ToString("X").Reverse().ToArray());
            string p6 = C6(lpMinimumApplicationAddress, lpMaximumApplicationAddress);
            string p7 = (vol[0] + vol[1]).ToString("X");
            return p1 + p2 + p3 + p4 + p5 + p6 + p7;
        }

        public static string C4(uint VolumeSerialNumber, uint dwNumberOfProcessors)
        {
            string sn = VolumeSerialNumber.ToString();
            StringBuilder sb = new();
            for (int i = 0; i < 5; i++)
            {
                sb.Append(((sn[i] + sn[^1] - '0' - '0') * dwNumberOfProcessors).ToString("X"));
            }
            return sb.ToString();
        }

        public static string C6(IntPtr lpMinimumApplicationAddress, IntPtr lpMaximumApplicationAddress)
        {
            int minAddress = Convert.ToInt32(lpMinimumApplicationAddress.ToString("X")[0..2], 16);
            int maxAddress = Convert.ToInt32((lpMaximumApplicationAddress / 0xFF).ToString("X")[0..2], 16) ;
            int diff = maxAddress - minAddress;
            return diff.ToString("X");
        }

    }
}
