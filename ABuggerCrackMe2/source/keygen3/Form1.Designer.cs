namespace keygen3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            label1 = new Label();
            button3 = new Button();
            label3 = new Label();
            label4 = new Label();
            textBox3 = new TextBox();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 31);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(305, 23);
            textBox1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(323, 12);
            button1.Name = "button1";
            button1.Size = new Size(115, 41);
            button1.TabIndex = 1;
            button1.Text = "Generate License";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(323, 59);
            button2.Name = "button2";
            button2.Size = new Size(115, 42);
            button2.TabIndex = 1;
            button2.Text = "Check";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 13);
            label1.Name = "label1";
            label1.Size = new Size(29, 15);
            label1.TabIndex = 2;
            label1.Text = "Key:";
            // 
            // button3
            // 
            button3.Location = new Point(323, 107);
            button3.Name = "button3";
            button3.Size = new Size(115, 41);
            button3.TabIndex = 3;
            button3.Text = "Restart as Admin";
            button3.UseVisualStyleBackColor = true;
            button3.Click += Button3_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(242, 133);
            label3.Name = "label3";
            label3.Size = new Size(75, 15);
            label3.TabIndex = 6;
            label3.Text = "By Chenx221";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 74);
            label4.Name = "label4";
            label4.Size = new Size(49, 15);
            label4.TabIndex = 7;
            label4.Text = "License:";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(12, 92);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(305, 23);
            textBox3.TabIndex = 8;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(446, 156);
            Controls.Add(textBox3);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(button3);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "KeyGen For ABuggerCrackMe2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button button1;
        private Button button2;
        private Label label1;
        private Button button3;
        private Label label3;
        private Label label4;
        private TextBox textBox3;
    }
}
