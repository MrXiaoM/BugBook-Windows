using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bugbook
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        const int MOUSEEVENTF_LEFTUP = 0x0004;
        
        bool enable_mouse = false;
        int temp = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = getRandomString((int)numericUpDown1.Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                button2.Enabled = false;
                timer1.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (enable_mouse)
            {
                button3.Text = "开启鼠标点击";
                label6.Size = new Size(0, 25);
                enable_mouse = false;
                numericUpDown2.Enabled = numericUpDown3.Enabled = numericUpDown4.Enabled = checkBox1.Enabled = true;
            }
            else
            {
                button3.Text = "关闭鼠标点击";
                label6.Size = new Size(0, 25);
                enable_mouse = true;
                numericUpDown2.Enabled = numericUpDown3.Enabled = numericUpDown4.Enabled = checkBox1.Enabled = false;
            }
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                button2.Enabled = true;
                timer1.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SendKeys.Send(getRandomChinese());
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label5.Text = "当前鼠标坐标:\n(" + Control.MousePosition.X + ", " + Control.MousePosition.Y + ")";
            if (enable_mouse)
            {
                label6.Size = new Size((int)(((float)(int)temp / (int)numericUpDown2.Value) * 186), 25);
                if (temp >= numericUpDown2.Value)
                {
                    temp = 0;
                    if(checkBox1.Checked)
                        SetCursorPos((int)numericUpDown3.Value, (int)numericUpDown4.Value);
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                }
                else
                {
                    temp++;
                }
            }
        }

        public static String getRandomString(int length)
        {
            String result = "";
            for (int i = 0; i < length; i++)
            {
                result += getRandomChinese();
            }
            return result;
        }

        public static String getRandomChinese()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();//生成字节数组
            int iRoot = BitConverter.ToInt32(buffer, 0);
            return new String(new char[] { (char)(new Random(iRoot).Next(20902) + 19968) });
        }
    }
}
