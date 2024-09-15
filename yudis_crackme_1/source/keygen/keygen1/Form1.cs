using System.Runtime.InteropServices;
using System.Text;
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

        private static string GenerateKey(string username)
        {
            const byte xorValue = 0x35;
            StringBuilder serialBuilder = new();
            for (int i = 0; i < username.Length; i++)
            {
                byte xorResult = (byte)(username[i] ^ xorValue);
                serialBuilder.Append(xorResult.ToString("X"));
            }
            return serialBuilder.ToString();
        }

        private static void AutoMode(string username, string key)
        {
            string windowTitle = "Check your serial !";
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

        private void Generate(string username)
        {
            string key = GenerateKey(username);
            textBox2.Text = key;
            if (checkBox1.Checked) AutoMode(username, key);
        }
    }
}
