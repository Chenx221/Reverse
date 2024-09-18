using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace keygen3
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindow(string? lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, StringBuilder? lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
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
            string value = textBox1.Text;
            if (string.IsNullOrEmpty(value))
            {
                MessageBox.Show("Key不能为空");
                return;
            }
            byte[] bytes = Encoding.ASCII.GetBytes("ABugger2");
            string result = DESEncrypt(value, bytes, bytes);
            textBox3.Text = result;

            string directoryPath = @"C:\CrackMes";
            string filePath = Path.Combine(directoryPath, "File");

            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

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

        private void Button2_Click(object sender, EventArgs e)
        {
            string value = textBox1.Text;
            if (string.IsNullOrEmpty(value))
            {
                MessageBox.Show("Key不能为空");
                return;
            }
            if (!File.Exists(@"C:\CrackMes\File"))
            {
                MessageBox.Show("许可证文件尚未生成");
                return;
            }
            AutoMode(value);
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


        public static string DESEncrypt(string plainText, byte[] key, byte[] iv)
        {
            using var des = DES.Create();
            des.Key = key;
            des.IV = iv;

            using var encryptor = des.CreateEncryptor(des.Key, des.IV);
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
            cryptoStream.FlushFinalBlock();
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public static string DESDecrypt(string encryptedText, byte[] key, byte[] iv)
        {
            using var des = DES.Create();
            des.Key = key;
            des.IV = iv;

            using var decryptor = des.CreateDecryptor(des.Key, des.IV);
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            using var memoryStream = new MemoryStream(encryptedBytes);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var reader = new StreamReader(cryptoStream, Encoding.UTF8);
            return reader.ReadToEnd();
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
    }
}
