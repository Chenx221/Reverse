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
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Name不能为空");
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("First Name不能为空");
                return;
            }
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Company不能为空");
                return;
            }
            Generate(textBox1.Text, textBox2.Text, textBox3.Text);
        }

        private void Generate(string username, string fname, string company)
        {
            if (username.Length >= 0x11 || fname.Length >= 0x11 || company.Length >= 0x11)
            {
                MessageBox.Show("长度不合规");
                return;
            }
            long[] key = GenerateKey(username, fname, company);

            //string key = GenerateKey(username);

            //if (!int.TryParse(username, out int serial1))
            //{
            //    MessageBox.Show("Serial1不是有效数字");
            //    return;
            //}
            //string key = GenerateKey(serial1);

            textBox4.Text = string.Join("-", key);

            //if (checkBox1.Checked) AutoMode(username, fname, company, key);
        }

        //private static string GenerateKey(string name)
        //{
        //    return ((char)(name[name.Length - 1] ^ 3)).ToString();
        //}

        private static long[] GenerateKey(string name, string fname, string company)
        {
            int day = DateTime.Now.Day;
            int month = DateTime.Now.Month;

            long nameAsc = Convert.ToInt32(name[0]);
            long fnameAsc = Convert.ToInt32(fname[0]);
            long companyAsc = Convert.ToInt32(company[0]);

            int length1 = name.Length;
            int length2 = fname.Length;
            int length3 = company.Length;

            string part1 = (length1 < 6) ? "444" : ((length1 < 11) ? "555" : "666");
            string part2 = (length2 < 6) ? "777" : ((length2 < 11) ? "888" : "999");
            string part3 = (length3 < 6) ? "111" : ((length3 < 11) ? "222" : "333");

            long s1 = nameAsc * Convert.ToInt64(part1) * day / month;
            long s2 = fnameAsc * Convert.ToInt64(part2) * day / month;
            long s3 = companyAsc * Convert.ToInt64(part3) * day / month;

            return [s1, s2, s3];
        }

        //private static string GenerateKey(int serial)
        //{
        //    int a = 0x539; // ebp-4
        //    int c = 2 + a - 1; //ebp-C

        //    while (a != 0)
        //    {
        //        serial ^= c--;
        //        a--;
        //    }

        //    return serial.ToString();
        //}

        private static void AutoMode(string username, string firstname, string company)
        {
            string windowTitle = "Registration";
            IntPtr hWnd = FindWindow(null, windowTitle);

            if (hWnd == IntPtr.Zero)
            {
                MessageBox.Show("窗口未找到");
                return;
            }

            int usernameControlId = 0x12;
            int firstnameControlId = 0x10;
            int companyControlId = 0xF;
            int submitButtonId = 0x11;

            IntPtr usernameHwnd = GetDlgItem(hWnd, usernameControlId);
            IntPtr firstnameHwnd = GetDlgItem(hWnd, firstnameControlId);
            IntPtr companyHwnd = GetDlgItem(hWnd, companyControlId);
            IntPtr submitButtonHwnd = GetDlgItem(hWnd, submitButtonId);

            if (usernameHwnd == IntPtr.Zero || firstnameHwnd == IntPtr.Zero || companyHwnd == IntPtr.Zero || submitButtonHwnd == IntPtr.Zero)
            {
                MessageBox.Show("控件未找到");
                return;
            }

            StringBuilder usernameToSet = new(username);
            StringBuilder firstnameToSet = new(firstname);
            StringBuilder companyToSet = new(company);

            SendMessage(usernameHwnd, WM_SETTEXT, IntPtr.Zero, usernameToSet);
            SendMessage(firstnameHwnd, WM_SETTEXT, IntPtr.Zero, firstnameToSet);
            SendMessage(companyHwnd, WM_SETTEXT, IntPtr.Zero, companyToSet);
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

        private void Button4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox3.Text);
        }
    }
}
