using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace keygen1
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindow(string? lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, StringBuilder? lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        const uint WM_SETTEXT = 0x000C;
        const uint BM_CLICK = 0x00F5;

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) MessageBox.Show("Name不能为空");
            Generate(textBox1.Text);
        }

        private void Generate(string username)
        {
            if (username.Length <= 3)
            {
                MessageBox.Show("Name长度需大于3位");
                return;
            }
            string key = GenerateKey(username);
            textBox2.Text = key;
            if (checkBox1.Checked) AutoMode(username, key);
        }

        private static string GenerateKey(string name)
        {
            int length = name.Length;

            byte[] arr0 =
[
            0x13, 0x16, 0x99, 0x11, 0x63, 0x15, 0x54, 0x52, 0x88, 0x01, 0x31, 0x56, 0x68, 0x55, 0x37, 0x00,
            0x25, 0x58, 0x2D, 0x61, 0x62, 0x6F, 0x6F, 0x2D, 0x6D, 0x65, 0x2D, 0x25, 0x58, 0x25, 0x69, 0x2D,
            0x53, 0x43, 0x41, 0x00, 0x47, 0x6F, 0x6F, 0x64, 0x20, 0x57, 0x6F, 0x72, 0x6B, 0x21, 0x20, 0x6E,
            0x6F, 0x77, 0x20, 0x6D, 0x61, 0x6B, 0x65, 0x20, 0x61, 0x20, 0x6B, 0x65, 0x79, 0x67, 0x65, 0x6E
];

            int unknown = 0;
            int part1 = 0;
            int part2 = 0;

            while (unknown < length)
            {
                int v1 = unknown;
                byte v2 = (byte)name[v1];
                int v4 = arr0[v1 + 1];
                v4 += part1 + v2;
                part1 = v4;
                int v6 = (byte)name[v1];
                v6 *= 0xA;
                part1 += v6;
                unknown++;
            }

            unknown = 0;

            while (unknown < length)
            {
                int ecx = arr0[unknown];
                ecx *= 0xA;
                part2 += ecx;
                byte eax = (byte)name[2]; // Assuming the index 2 is intended
                int edx = arr0[unknown];
                edx += part2 + eax;
                part2 = edx;
                part2 += 0x31337;
                unknown++;
            }

            string hexPart1 = part1.ToString("X");
            string hexPart2 = part2.ToString("X");
            string decimalPart2 = part2.ToString();

            string result = $"{hexPart1}-aboo-me-{hexPart2}{decimalPart2}-SCA";
            return result;
        }

        private static void AutoMode(string username, string key)
        {
            string windowTitle = "Aboo Me - kiTo / SCA";
            IntPtr hWnd = FindWindow(null, windowTitle);

            if (hWnd == IntPtr.Zero)
            {
                MessageBox.Show("窗口未找到");
                return;
            }
            int usernameControlId = 1000;
            int keyControlId = 1005;
            int submitButtonId = 1003;

            IntPtr usernameHwnd = GetDlgItem(hWnd, usernameControlId);
            IntPtr keyHwnd = GetDlgItem(hWnd, keyControlId);
            IntPtr submitButtonHwnd = GetDlgItem(hWnd, submitButtonId);

            if (usernameHwnd == IntPtr.Zero || keyHwnd == IntPtr.Zero || submitButtonHwnd == IntPtr.Zero)
            {
                MessageBox.Show("控件未找到");
                return;
            }

            StringBuilder usernameToSet = new(username);
            StringBuilder keyToSet = new(key);

            SendMessage(usernameHwnd, WM_SETTEXT, IntPtr.Zero, usernameToSet);
            SendMessage(keyHwnd, WM_SETTEXT, IntPtr.Zero, keyToSet);
            SendMessage(submitButtonHwnd, BM_CLICK, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
