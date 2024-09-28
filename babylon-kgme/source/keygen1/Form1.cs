using System.Runtime.InteropServices;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

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
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Name不能为空");
                return;
            }
            //if (string.IsNullOrEmpty(textBox2.Text))
            //{
            //    MessageBox.Show("First Name不能为空");
            //    return;
            //}
            //if (string.IsNullOrEmpty(textBox3.Text))
            //{
            //    MessageBox.Show("Company不能为空");
            //    return;
            //}
            //Generate(textBox1.Text, textBox2.Text, textBox3.Text);
            Generate(textBox1.Text);
        }

        private void Generate(string username)
        {
            if (username.Length <= 3 || username.Length > 14)
            {
                MessageBox.Show("Name长度要求4~14位");
                return;
            }

            string? key = GenerateKey(username);
            if (key == null)
            {
                MessageBox.Show("生成Key失败");
                return;
            }
            //if (!int.TryParse(username, out int serial1))
            //{
            //    MessageBox.Show("Serial1不是有效数字");
            //    return;
            //}
            //string key = GenerateKey(serial1);
            //textBox4.Text = string.Join("-", key);
            textBox2.Text = key;
            //if (checkBox1.Checked) AutoMode(username,key);
            //禁用，由于controlId不固定
        }

        private static string? GenerateKey(string name)
        {
            string part = "-[#]]=}&&&+(=$*,,)&.*/+++[][;/..§0";
            string name_p1 = string.Join(" ", name.Select(c => c.ToString())) + " ";
            string part_p1 = new(part.Select(c => (char)(c + 1)).ToArray());
            char[] p2 = part_p1.ToCharArray();
            for (int i = 0; i < name_p1.Length; i++)
            {
                p2[i] ^= name_p1[i];
            }
            char[] p2_rev = (char[])p2.Clone();
            Array.Reverse(p2_rev);
            char[] p3 = new char[0x22];
            for (int i = 0; i < 0x11; i++)
            {
                p3[2 * i] = (char)((p2_rev[i] <= 0x1F || p2_rev[i] > 0x7A) ? 0x36 : p2_rev[i]);
                p3[2 * i + 1] = (char)((p2[i] <= 0x1F || p2[i] > 0x7A) ? 0x36 : p2[i]);
            }
            return new string(p3);
        }

        private static void AutoMode(string username, string key)
        {
            string windowTitle = "ArturDents CrackMe #2";
            IntPtr hWnd = FindWindow(null, windowTitle);

            if (hWnd == IntPtr.Zero)
            {
                MessageBox.Show("窗口未找到");
                return;
            }

            int usernameControlId = 0xBB8;
            int serialControlId = 0xBB9;
            int submitButtonId = 0xBBA;

            IntPtr usernameHwnd = GetDlgItem(hWnd, usernameControlId);
            IntPtr serialHwnd = GetDlgItem(hWnd, serialControlId);
            IntPtr submitButtonHwnd = GetDlgItem(hWnd, submitButtonId);

            if (usernameHwnd == IntPtr.Zero || serialHwnd == IntPtr.Zero || submitButtonHwnd == IntPtr.Zero)
            {
                MessageBox.Show("控件未找到");
                return;
            }

            StringBuilder usernameToSet = new(username);
            StringBuilder serialToSet = new(key);

            SendMessage(usernameHwnd, WM_SETTEXT, IntPtr.Zero, usernameToSet);
            SendMessage(serialHwnd, WM_SETTEXT, IntPtr.Zero, serialToSet);
            SendMessage(submitButtonHwnd, BM_CLICK, IntPtr.Zero, IntPtr.Zero);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                Clipboard.SetText(textBox1.Text);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                Clipboard.SetText(textBox2.Text);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                Clipboard.SetText(textBox3.Text);
            }
        }
    }
}
