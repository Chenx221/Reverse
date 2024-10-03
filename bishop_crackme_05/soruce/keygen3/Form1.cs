using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace keygen3
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindow(string? lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, StringBuilder? lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        const uint WM_SETTEXT = 0x000C;
        const uint BM_CLICK = 0x00F5;
        private delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

        public Form1()
        {
            InitializeComponent();
            UpdateWindowTitle();
        }

        private void UpdateWindowTitle()
        {
            Text += IsRunningAsAdministrator() ? " (Admin)" : " (Non-Admin)";
            button3.Enabled = !IsRunningAsAdministrator();
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
            string name = textBox1.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Key不能为空");
                return;
            }
            Generate(name);
        }

        private void Generate(string name)
        {
            //if (name.Length < 5)
            //{
            //    MessageBox.Show("Name至少5位长度");
            //    return;
            //}
            //else if (name.Length > 0x14)
            //{
            //    MessageBox.Show("Name最大长度为20");//不影响密钥验证，但这里还是加了这个限制
            //    return;
            //}
            if (name[0] == ' ')
            {
                MessageBox.Show("Name开头不能包含空格");
                return;
            }
            string? key = GenerateKey(name);
            if (string.IsNullOrEmpty(key))
            {
                MessageBox.Show("生成Key失败");
                return;
            }
            textBox3.Text = key;

            if (checkBox1.Checked) AutoMode(name, key);
        }

        private static string? GenerateKey(string name)
        {
            long result = 0;
            foreach (char c in name)
            {
                int ci = c;
                result += ci * ci;
            }
            result += result * result;
            return result.ToString();
        }

        private static void SaveKeyFile(string name, string serial)
        {
            SaveFileDialog saveFileDialog = new()
            {
                FileName = "cm5.dat", // 固定文件名
                Filter = "激活凭证 (*.dat)|*.dat",  // 限制保存为.dat文件
                Title = "请选择密钥文件的保存位置"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    string result = $"{name}\r\n{serial}";
                    File.WriteAllText(filePath, result);
                    MessageBox.Show("文件保存成功!");
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("保存文件失败，请尝试以管理员身份运行应用程序。");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"保存文件失败: {ex.Message}");
                }
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            string key = textBox3.Text;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(key))
            {
                MessageBox.Show("请先计算密钥再生成key文件");
                return;
            }
            SaveKeyFile(name, key);
        }

        private void Button3_Click(object sender, EventArgs e)
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



        private static IntPtr[] childHandles = new IntPtr[10];
        private static int childCount = 0;

        private static bool EnumChildWindowsProc(IntPtr hwnd, IntPtr lParam)
        {
            if (childCount >= childHandles.Length)
                return false; // Stop enumeration if the array is full

            childHandles[childCount++] = hwnd;
            return true; // Continue enumeration
        }

        private static void AutoMode(string username)
        {
            string windowTitle = "ABuggerCrackMe#2";
            IntPtr hWnd = FindWindow(null, windowTitle);

            if (hWnd == IntPtr.Zero)
            {
                MessageBox.Show("窗口未找到");
                return;
            }
            //int usernameControlId = 0x402C4;
            //int submitButtonId = 0x6028A;
            //IntPtr usernameHwnd = GetDlgItem(hWnd, usernameControlId);
            //IntPtr submitButtonHwnd = GetDlgItem(hWnd, submitButtonId);

            //IntPtr usernameHwnd = FindWindowEx(hWnd, IntPtr.Zero, "Edit", string.Empty);
            //IntPtr submitButtonHwnd = FindWindowEx(hWnd, IntPtr.Zero, "Button", string.Empty);

            childCount = 0;
            EnumChildWindows(hWnd, EnumChildWindowsProc, IntPtr.Zero);
            if (childCount < 5)
            {
                MessageBox.Show("控件未找到");
                return;
            }

            IntPtr usernameHwnd = childHandles[4];
            IntPtr submitButtonHwnd = childHandles[3];

            if (usernameHwnd == IntPtr.Zero || submitButtonHwnd == IntPtr.Zero)
            {
                MessageBox.Show("控件未找到");
                return;
            }

            StringBuilder usernameToSet = new(username);

            SendMessage(usernameHwnd, WM_SETTEXT, IntPtr.Zero, usernameToSet);
            MessageBox.Show("请在稍后弹出的文件选框中选择C:\\CrackMes\\File文件");
            SendMessage(submitButtonHwnd, BM_CLICK, IntPtr.Zero, IntPtr.Zero);
        }
        private static void AutoMode(string username, string key)
        {
            string windowTitle = "BiSHoP's VB Crackme #5";
            IntPtr hWnd = FindWindow(null, windowTitle);

            if (hWnd == IntPtr.Zero)
            {
                MessageBox.Show("窗口未找到");
                return;
            }

            int usernameControlId = 0x5;
            int serialControlId = 0x4;
            int submitButtonId = 0x3;

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

        private void Button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                Clipboard.SetText(textBox1.Text);
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text))
            {
                Clipboard.SetText(textBox3.Text);
            }
        }
    }
}
