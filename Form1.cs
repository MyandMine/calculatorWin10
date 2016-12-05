using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileTree
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 记录鼠标与窗口的相对坐标，好拖动界面
        /// </summary>
        int a, b;
        /// <summary>
        /// 记录公式
        /// </summary>
        List<Button> expBtns = null;
        /// <summary>
        /// 记录结果
        /// </summary>
        List<Button> resBtns = null;
        /// <summary>
        /// 窗口初始化时间
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            //作为判定label1是否为重新输入
            this.label1.Tag = true;
            expBtns = new List<Button>();
            resBtns = new List<Button>();
        }
        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button35_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 最大化按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button34_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
        /// <summary>
        /// 最小化按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button33_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #region 拖动相关操作
        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            this.label4.Tag = true;
            a = this.Location.X - MousePosition.X;
            b = this.Location.Y - MousePosition.Y;
        }

        private void label4_MouseMove(object sender, MouseEventArgs e)
        {
            if ((bool)this.label4.Tag)
            {
                this.Location = new Point(a + MousePosition.X, b + MousePosition.Y);
            }
        }

        private void label4_MouseUp(object sender, MouseEventArgs e)
        {
            this.label4.Tag = false;
        }
        #endregion
        /// <summary>
        /// 数字的输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNum_Click(object sender, EventArgs e)
        {
            if (this.label1.Text.Contains(".") )
            {
                this.label1.Text = (bool)this.label1.Tag ? ((Button)sender).Text : this.label1.Text + ((Button)sender).Text;
                this.label1.Tag = false;
            }
            else
            {
                if (((Button)sender).Text == ".")
                {
                    this.label1.Text = (bool)this.label1.Tag ? "0." : this.label1.Text + ((Button)sender).Text;
                    this.label1.Tag = false;
                }
                else
                {
                    double text = double.Parse(this.label1.Text == "0" || (bool)this.label1.Tag ? ((Button)sender).Text : this.label1.Text + ((Button)sender).Text);
                    this.label1.Text = text == 0 ? "0" :string.Format("{0:#,##.##}", text);
                    this.label1.Tag = false;
                }
            }
        }
        /// <summary>
        /// 二元运算的输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSign2_Click(object sender, EventArgs e)
        {
            this.label2.Text = this.label1.Text;
            this.label2.Tag = ((Button)sender).Text;
            this.label1.Tag = true;
            this.Tag = false;
        }
        /// <summary>
        /// 等号的输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEq_Click(object sender, EventArgs e)
        {
            switch((string)this.label2.Tag)
            {
                case "%":
                    this.label1.Text = string.Format("{0:#,#0.##}", (double.Parse(this.label2.Text) % double.Parse(this.label1.Text)));
                    break;
                case "÷":
                    this.label1.Text = string.Format("{0:#,#0.##}", (double.Parse(this.label2.Text) / double.Parse(this.label1.Text)));
                    break;
                case "×":
                    this.label1.Text = string.Format("{0:#,#0.##}", (double.Parse(this.label2.Text) * double.Parse(this.label1.Text)));
                    break;
                case "-":
                    this.label1.Text = string.Format("{0:#,#0.##}", (double.Parse(this.label2.Text) - double.Parse(this.label1.Text)));
                    break;
                case "+":
                    this.label1.Text = string.Format("{0:#,#0.##}", (double.Parse(this.label2.Text) + double.Parse(this.label1.Text)));
                    break;
                case "/":
                    break;
            }
            this.label2.Text = this.label1.Text;
            this.label1.Tag = true;
            this.label2.Tag = "";
            if (this.label1.Text == "")
                this.label1.Text = "0";
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.label1.Text);
        }
        /// <summary>
        /// 粘贴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Clipboard.GetText()))
            {
                this.label1.Text = Clipboard.GetText();
                try
                {
                    double.Parse(Clipboard.GetText());
                }
                catch
                {
                    this.label1.Text = "错误输入";
                }
            }
        }
        #region 复制粘贴快捷键
        /// <summary>
        /// 复制粘贴快捷键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
                this.粘贴ToolStripMenuItem_Click(this, null);
            else
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
                    this.复制ToolStripMenuItem_Click(this, null);
        }
        private void label1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
                this.粘贴ToolStripMenuItem_Click(this, null);
            else
                if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
                this.复制ToolStripMenuItem_Click(this, null);
        }
        #endregion
        /// <summary>
        /// 历史记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button32_Click(object sender, EventArgs e)
        {
            if (this.button32.Tag == null || (bool)this.button32.Tag == false)
            {
                this.tableLayoutPanel1.Controls.Remove(this.tableLayoutPanel2);
                this.tableLayoutPanel1.Controls.Add(this.panel1);
                this.button32.Tag = true;
            }
            else
            {
                this.tableLayoutPanel1.Controls.Remove(this.panel1);
                this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2);
                this.button32.Tag = false;
            }
        }
        #region 同步两个按钮按钮
        private void button37_MouseEnter(object sender, EventArgs e)
        {
            this.button36.BackColor = Color.FromArgb(215, 215, 215);
        }

        private void button37_MouseLeave(object sender, EventArgs e)
        {
            this.button36.BackColor = this.tableLayoutPanel6.BackColor;
        }

        private void button37_MouseDown(object sender, MouseEventArgs e)
        {
            this.button36.BackColor = Color.FromArgb(229, 229, 229);
        }

        private void button37_MouseUp(object sender, MouseEventArgs e)
        {
            this.button36.BackColor = Color.FromArgb(215, 215, 215);
        }

        private void button36_MouseDown(object sender, MouseEventArgs e)
        {
            this.button37.BackColor = Color.FromArgb(229, 229, 229);
        }

        private void button36_MouseLeave(object sender, EventArgs e)
        {
            this.button37.BackColor = this.tableLayoutPanel6.BackColor;
        }

        private void button36_MouseUp(object sender, MouseEventArgs e)
        {
            this.button37.BackColor = Color.FromArgb(215, 215, 215);
        }

        private void button36_MouseEnter(object sender, EventArgs e)
        {
            this.button37.BackColor = Color.FromArgb(215, 215, 215);
        }

        private void button37_Click(object sender, EventArgs e)
        {
            this.label1.Text = this.button37.Text;
            this.label2.Text = this.button36.Text;
            this.button32_Click(this, null);
        }

        #endregion

        /// <summary>
        /// 一元运算的输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSign1_Click(object sender, EventArgs e)
        {
            switch(((Button)sender).Text)
            {
                case "±":
                    this.label1.Text = this.label1.Text[0] == '-' ? this.label1.Text.Remove(0, 1) : "-" + this.label1.Text;
                    break;
                case "√":
                    this.label1.Text = string.Format("{0:#,#0.##}", Math.Sqrt(double.Parse(this.label1.Text)));
                    break;
                case "x^2":
                    this.label1.Text = string.Format("{0:#,#0.##}", Math.Pow(double.Parse(this.label1.Text),2));
                    break;
                case "1/x":
                    double text = 1 / double.Parse(this.label1.Text);
                    this.label1.Text = string.Format("{0:#,#0.##}", text);
                    break;
                case "C":
                    this.label1.Text = "";
                    this.label2.Text = "";
                    this.label2.Tag = "";
                    break;
                case "CE":
                    this.label1.Text = "";
                    this.label2.Text = "";
                    this.label2.Tag = "";
                    break;
                case "":
                    this.label1.Text = this.label1.Text.Contains(".") || this.label1.Text.Length < 2 ? this.label1.Text.Remove(this.label1.Text.Length - 1) : string.Format("{0:#,#0.##}", double.Parse(this.label1.Text.Remove(this.label1.Text.Length - 1)));
                    break;
            }
            this.label2.Text = this.label1.Text;
            this.label1.Tag = true;
            this.label2.Tag = "";
            if (this.label1.Text == "")
                this.label1.Text = "0";
        }
    }
}
