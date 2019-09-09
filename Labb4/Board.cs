using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Labb4
{
    public class Board : Form
    {
        public Button button1;
        public Board()
        {
            button1 = new Button
            {
                Size = new Size(40, 40),
                Location = new Point(30, 30),
                Text = "Click me"
            };
            this.Controls.Add(button1);
            button1.Click += new EventHandler(Button1_Click);
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hello World");
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(613, 539);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }
    }
}
