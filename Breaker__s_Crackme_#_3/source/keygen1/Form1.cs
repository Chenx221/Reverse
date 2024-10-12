using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Xml.Linq;

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

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        const uint WM_SETTEXT = 0x000C;
        const uint BM_CLICK = 0x00F5;

        public Form1()
        {
            InitializeComponent();
            UpdateWindowTitle();
        }
        private void UpdateWindowTitle()
        {
            Text += IsRunningAsAdministrator() ? " (Admin)" : " (Non-Admin)";
            button6.Enabled = !IsRunningAsAdministrator();
        }
        private static bool IsRunningAsAdministrator()
        {
            try
            {
                var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
                return false;
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(textBox1.Text))
            //{
            //    MessageBox.Show("Name不能为空");
            //    return;
            //}
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
            Generate(textBox1.Text, textBox2.Text, textBox3.Text);
        }

        private void Generate(string name, string nickname, string age)
        {
            //if (name.Length < 4)
            //{
            //    MessageBox.Show("Name长度需大于等于4");
            //    return;
            //}
            //if (username == "Ab")
            //{
            //    MessageBox.Show("Name不能为Ab");
            //    return;
            //}
            //if(Regex.IsMatch(username, @"\d"))
            //{
            //    MessageBox.Show("Name不允许包含数字");
            //    return;
            //}
            //if (username[0]==' ')
            //{
            //    MessageBox.Show("Name开头不能包含空格");
            //    return;
            //}
            string? serial = GenerateKey(name, nickname, age);
            if (string.IsNullOrEmpty(serial))
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
            textBox4.Text = serial;
            if (checkBox1.Checked) AutoMode(name, nickname, age, serial);
        }

        private static string? GenerateKey(string name, string nickname, string age)
        {
            int nameLength = name.Length;
            int nicknameLength = nickname.Length;
            int ageLength = age.Length;

            string p1 = (nicknameLength * nameLength * ageLength).ToString();
            string p2 = Reverse(Mid(name, 1, 3));
            string p3 = Mid(nickname, 2, 2);
            int p4 = nameLength + nicknameLength + ageLength;
            string p5 = Mid(age, 1, 1);
            int p6 = nameLength;

            return $"{p1}-{p2}{p3}{p4}-{p5}{p6}";
        }
        public static string Mid(string str, int start, int length)
        {
            if (start < 0 || start >= str.Length)
                return string.Empty;
            int actualLength = Math.Min(length, str.Length - start);
            return str.Substring(start, actualLength);
        }
        static string Reverse(string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private static void AutoMode(string name, string nickname, string age, string serial)
        {
            string windowTitle = "Breaker's Crackme # 3";
            List<IntPtr> windowHandles = FindAllWindowsWithTitle(windowTitle);

            if (windowHandles.Count == 0)
            {
                MessageBox.Show("窗口未找到");
                return;
            }
            IntPtr hWnd = windowHandles[0];



            //int frameControlId = 0x3;
            int nameControlId = 0x5;
            int nicknameControlId = 0x4;
            int ageControlId = 0x3;
            int serialControlId = 0x2;
            int submitButtonId = 0x1;


            //IntPtr frameHwnd = GetDlgItem(hWnd, frameControlId);
            //IntPtr usernameHwnd = GetDlgItem(frameHwnd, usernameControlId);
            //IntPtr submitButtonHwnd = GetDlgItem(frameHwnd, submitButtonId);
            IntPtr nameHwnd = GetDlgItem(hWnd, nameControlId);
            IntPtr nicknameHwnd = GetDlgItem(hWnd, nicknameControlId);
            IntPtr ageHwnd = GetDlgItem(hWnd, ageControlId);
            IntPtr serialHwnd = GetDlgItem(hWnd, serialControlId);
            IntPtr submitButtonHwnd = GetDlgItem(hWnd, submitButtonId);

            if (nameHwnd == IntPtr.Zero || nicknameHwnd == IntPtr.Zero || ageHwnd == IntPtr.Zero || serialHwnd == IntPtr.Zero || submitButtonHwnd == IntPtr.Zero)
            {
                MessageBox.Show("控件未找到");
                return;
            }
            //if (usernameHwnd == IntPtr.Zero || frameHwnd == IntPtr.Zero || serialHwnd == IntPtr.Zero || submitButtonHwnd == IntPtr.Zero)
            //{
            //    MessageBox.Show("控件未找到");
            //    return;
            //}

            StringBuilder nameToSet = new(name);
            StringBuilder nicknameToSet = new(nickname);
            StringBuilder ageToSet = new(age);
            StringBuilder serialToSet = new(serial);

            SendMessage(nameHwnd, WM_SETTEXT, IntPtr.Zero, nameToSet);
            SendMessage(nicknameHwnd, WM_SETTEXT, IntPtr.Zero, nicknameToSet);
            SendMessage(ageHwnd, WM_SETTEXT, IntPtr.Zero, ageToSet);
            SendMessage(serialHwnd, WM_SETTEXT, IntPtr.Zero, serialToSet);
            SendMessage(submitButtonHwnd, BM_CLICK, IntPtr.Zero, IntPtr.Zero);
        }
        static List<IntPtr> FindAllWindowsWithTitle(string title)
        {
            List<IntPtr> handles = [];
            EnumWindows((hWnd, lParam) =>
            {
                if (IsWindowVisible(hWnd))
                {
                    StringBuilder buffer = new(256);
                    if (GetWindowText(hWnd, buffer, buffer.Capacity) > 0)
                    {
                        if (buffer.ToString().Equals(title, StringComparison.OrdinalIgnoreCase))
                        {
                            handles.Add(hWnd);
                        }
                    }
                }
                return true; // 继续枚举窗口
            }, IntPtr.Zero);

            return handles;
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

        private void Button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox4.Text))
            {
                Clipboard.SetText(textBox4.Text);
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            try
            {
                string exePath = Application.ExecutablePath;
                var processInfo = new ProcessStartInfo
                {
                    FileName = exePath,
                    UseShellExecute = true,
                    Verb = "runas",
                    Arguments = ""
                };
                Process.Start(processInfo);
                Application.Exit();
            }
            catch (System.ComponentModel.Win32Exception)
            {
                MessageBox.Show("权限提升请求被拒绝。");
            }
        }


    }
}
