using System.Runtime.InteropServices;
using System.Text;

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
            if (string.IsNullOrEmpty(textBox1.Text)) MessageBox.Show("Serial1不能为空");
            Generate(textBox1.Text);
        }

        private void Generate(string username)
        {
            //if (username.Length <= 3)
            //{
            //    MessageBox.Show("Name长度需大于3位");
            //    return;
            //}
            //string key = GenerateKey(username);

            if (!int.TryParse(username, out int serial1))
            {
                MessageBox.Show("Serial1不是有效数字");
                return;
            }
            string key = GenerateKey(serial1);
            textBox2.Text = key;
            //if (checkBox1.Checked) AutoMode(username, key);
        }

        //private static string GenerateKey(string name)
        //{
        //    return ((char)(name[name.Length - 1] ^ 3)).ToString();
        //}

        private static string GenerateKey(int serial)
        {
            int a = 0x539; // ebp-4
            int c = 2 + a - 1; //ebp-C

            while (a != 0)
            {
                serial ^= c--;
                a--;
            }

            return serial.ToString();
        }

        private static void AutoMode(string username, string key)
        {
            string windowTitle = "Crackme 8";
            IntPtr hWnd = FindWindow(null, windowTitle);

            if (hWnd == IntPtr.Zero)
            {
                MessageBox.Show("窗口未找到");
                return;
            }

            int usernameControlId = 4;
            int keyControlId = 3;
            int submitButtonId = 2;

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

        private void Button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox2.Text);
        }
    }
}
