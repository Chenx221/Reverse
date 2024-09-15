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
        
        const uint WM_GETTEXT = 0x000D;
        const uint WM_SETTEXT = 0x000C;
        const uint BM_CLICK = 0x00F5;

        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            FillAndSubmitKey();
        }

        private static string GenerateKey(string username)
        {
            char firstChar = username[0];
            string newChar = "";
            if ((firstChar >= 'a' && firstChar < 'z') ||
                (firstChar >= 'A' && firstChar < 'Z') ||
                (firstChar >= '0' && firstChar < '9'))
            {
                char incrementedChar = (char)(firstChar + 1);
                newChar = incrementedChar.ToString();
            }
            string seconds = DateTime.Now.Second.ToString("D2");
            string yearLastTwoDigits = DateTime.Now.Year.ToString().Substring(2, 2);
            string s1 = "Grand-Theft-Auto-Vice-City";
            string s2 = "bbidhan-ThE-Great";
            return $"{newChar}{seconds}{yearLastTwoDigits}{s1}{s2}";
        }

        private void FillAndSubmitKey()
        {
            string windowTitle = "KeyGen me #3# by :bbidhan";
            IntPtr hWnd = FindWindow(null, windowTitle);

            if (hWnd == IntPtr.Zero)
            {
                MessageBox.Show("窗口未找到");
                return;
            }
            int usernameControlId = 6;
            int keyControlId = 5;
            int submitButtonId = 4;

            IntPtr usernameHwnd = GetDlgItem(hWnd, usernameControlId);
            IntPtr keyHwnd = GetDlgItem(hWnd, keyControlId);
            IntPtr submitButtonHwnd = GetDlgItem(hWnd, submitButtonId);

            if (usernameHwnd == IntPtr.Zero || keyHwnd == IntPtr.Zero || submitButtonHwnd == IntPtr.Zero)
            {
                MessageBox.Show("控件未找到");
                return;
            }

            StringBuilder usernameBuffer = new(256);
            SendMessage(usernameHwnd, WM_GETTEXT, new IntPtr(usernameBuffer.Capacity), usernameBuffer);

            string username = usernameBuffer.ToString();
            string key = GenerateKey(username);

            StringBuilder usernameToSet = new(username);
            StringBuilder keyToSet = new(key);

            SendMessage(usernameHwnd, WM_SETTEXT, IntPtr.Zero, usernameToSet);
            SendMessage(keyHwnd, WM_SETTEXT, IntPtr.Zero, keyToSet);

            SendMessage(submitButtonHwnd, BM_CLICK, IntPtr.Zero, IntPtr.Zero);
            textBox1.Text = username;
            textBox2.Text = key;
        }
    }
}
