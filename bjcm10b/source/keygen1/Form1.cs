﻿using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
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
            if (username.Length < 2)
            {
                MessageBox.Show("Name长度需大于等于2");
                return;
            }
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
            string? key = GenerateKey(username);
            if (string.IsNullOrEmpty(key))
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
            if (checkBox1.Checked) AutoMode(username, key);
        }

        private static string? GenerateKey(string name)
        {
            return $" {(name[0] + name[^1]) * 0xF423F}";
        }


        private static void AutoMode(string username, string key)
        {
            string windowTitle = "Bjanes Crackme V1.0b";
            IntPtr hWnd = FindWindow(null, windowTitle);

            if (hWnd == IntPtr.Zero)
            {
                MessageBox.Show("窗口未找到");
                return;
            }
            int serialControlId = 0x1;

            int frameControlId = 0x3;
            int usernameControlId = 0x5;
            int submitButtonId = 0x4;


            IntPtr serialHwnd = GetDlgItem(hWnd, serialControlId);
            IntPtr frameHwnd = GetDlgItem(hWnd, frameControlId);
            IntPtr usernameHwnd = GetDlgItem(frameHwnd, usernameControlId);
            IntPtr submitButtonHwnd = GetDlgItem(frameHwnd, submitButtonId);

            if (usernameHwnd == IntPtr.Zero || frameHwnd == IntPtr.Zero || serialHwnd == IntPtr.Zero || submitButtonHwnd == IntPtr.Zero)
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
