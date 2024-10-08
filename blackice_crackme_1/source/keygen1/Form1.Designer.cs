namespace keygen1
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
            button1 = new Button();
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            textBox2 = new TextBox();
            label3 = new Label();
            checkBox1 = new CheckBox();
            button2 = new Button();
            button3 = new Button();
            label4 = new Label();
            textBox3 = new TextBox();
            button4 = new Button();
            label5 = new Label();
            textBox4 = new TextBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(393, 2);
            button1.Name = "button1";
            button1.Size = new Size(93, 172);
            button1.TabIndex = 0;
            button1.Text = "Generate";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 18);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 1;
            label1.Text = "Name";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(75, 14);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(262, 23);
            textBox1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(34, 51);
            label2.Name = "label2";
            label2.Size = new Size(35, 15);
            label2.TabIndex = 3;
            label2.Text = "Serial";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(75, 47);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(262, 23);
            textBox2.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(312, 155);
            label3.Name = "label3";
            label3.Size = new Size(74, 15);
            label3.TabIndex = 5;
            label3.Text = "By Chenx221";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(75, 155);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(89, 19);
            checkBox1.TabIndex = 6;
            checkBox1.Text = "Auto Mode ";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(343, 13);
            button2.Name = "button2";
            button2.Size = new Size(44, 24);
            button2.TabIndex = 7;
            button2.Text = "Copy";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(343, 47);
            button3.Name = "button3";
            button3.Size = new Size(44, 23);
            button3.TabIndex = 8;
            button3.Text = "Copy";
            button3.UseVisualStyleBackColor = true;
            button3.Click += Button3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(10, 82);
            label4.Name = "label4";
            label4.Size = new Size(59, 15);
            label4.TabIndex = 9;
            label4.Text = "Company";
            label4.Visible = false;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(75, 79);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(262, 23);
            textBox3.TabIndex = 10;
            textBox3.Visible = false;
            // 
            // button4
            // 
            button4.Location = new Point(343, 79);
            button4.Name = "button4";
            button4.Size = new Size(44, 23);
            button4.TabIndex = 11;
            button4.Text = "Copy";
            button4.UseVisualStyleBackColor = true;
            button4.Visible = false;
            button4.Click += Button4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(31, 116);
            label5.Name = "label5";
            label5.Size = new Size(35, 15);
            label5.TabIndex = 12;
            label5.Text = "Serial";
            label5.Visible = false;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(75, 113);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(262, 23);
            textBox4.TabIndex = 13;
            textBox4.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(491, 183);
            Controls.Add(textBox4);
            Controls.Add(label5);
            Controls.Add(button4);
            Controls.Add(textBox3);
            Controls.Add(label4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(checkBox1);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "KeyGen For blackice_crackme_1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private TextBox textBox2;
        private Label label3;
        private CheckBox checkBox1;
        private Button button2;
        private Button button3;
        private Label label4;
        private TextBox textBox3;
        private Button button4;
        private Label label5;
        private TextBox textBox4;
    }
}
