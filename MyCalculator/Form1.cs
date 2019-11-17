using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCalculator
{
    public partial class Form1 : Form
    {
        double result = 0;
        double initialnum = 1;
        Queue<string> expression = new Queue<string>();
        Queue<int> numbers = new Queue<int>();
        String last_sign = "+";
        Boolean exist_sign = false;
        Boolean exist_equal = false;
        String last_num = "0";
        String this_num = "0";
        String op_num1 = "0";
        String op_num2 = "0";
        String next_num = "";
        double memorynum = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button a = (Button)sender;
            double result = 0;
            String boxtext = a.Text.ToString();
            label1.Focus();
            if (boxtext == "%")
            {
                if (last_sign == "+" || last_sign == "-")
                {
                    this_num = (Convert.ToDouble(this_num) * Convert.ToDouble(last_num) / 100).ToString();
                }
                else if (last_sign == "x" || last_sign == "÷")
                {
                    this_num = (Convert.ToDouble(this_num) / 100).ToString();
                }
                resultbox.Text = this_num.ToString();
                exist_sign = false;
                return;
            }
            if (boxtext == "1/x")
            {
                if (Convert.ToDouble(this_num) == 0)
                {
                    resultbox.Text = "除数不能为0";
                    clear_result();
                    return;
                }
                this_num = (1 / Convert.ToDouble(this_num)).ToString();
                resultbox.Text = this_num.ToString();
                
                exist_sign = false;
                return;
            }
            if (boxtext == "sqrt")
            {
                if (Convert.ToDouble(this_num) < 0)
                {
                    resultbox.Text = "负数不能开根号";
                    clear_result();
                    return;
                }
                this_num = (Math.Sqrt(Convert.ToDouble(this_num))).ToString();
                resultbox.Text = this_num.ToString();
                exist_sign = false;
                return;
            }
            if (exist_sign)
            {
                last_sign = boxtext;
                return;
            }
            if (exist_equal)
            {
                last_sign = boxtext;
                exist_sign = true;
                exist_equal = false;
                return;
            }

            else if (last_sign == "+")
            {
                result = Convert.ToDouble(last_num) + Convert.ToDouble(this_num);
            }
            else if (last_sign == "-")
            {
                result = Convert.ToDouble(last_num) - Convert.ToDouble(this_num);
            }
            else if (last_sign == "x")
            {
                result = Convert.ToDouble(last_num) * Convert.ToDouble(this_num);
            }
            else if (last_sign == "÷")
            {
                if (Convert.ToDouble(this_num) == 0)
                {
                    resultbox.Text = "除数不能为0";
                    clear_result();
                    return;
                }
                result = Convert.ToDouble(last_num) / Convert.ToDouble(this_num);
            }
            last_num = result.ToString();
            this_num = last_num;
            exist_sign = true;
            resultbox.Text = result.ToString();
            last_sign = boxtext;

        }
        private void Button_Click_1(object sender, EventArgs e)
        {

            Button a = (Button)sender;
            double result = 0;
            String boxtext = a.Text.ToString();
            label1.Focus();
            if (double.TryParse(boxtext, out result))
            {
                if (exist_sign || exist_equal) this_num = "";
                if (exist_equal)
                {
                    last_num = "0";
                    last_sign = "+";
                }
                if (this_num == "0"|| this_num == "-0")
                {
                    this_num = boxtext;
                }
                else
                    this_num += boxtext;
                resultbox.Text = this_num;
            }
            else if (boxtext == "C")
            {
                this_num = "0";
                last_num = "0";
                exist_sign = false;
                exist_equal = false;
                last_sign = "+";
                result = 0;
                resultbox.Text = this_num;
                numbers.Clear();
                numbers.Enqueue(0);
                expression.Clear();
            }
            else if (boxtext == "CE")
            {
                this_num = "0";
                resultbox.Text = this_num;
            }
            else if (boxtext == ".")
            {
                if (exist_sign || exist_equal)
                {
                    this_num = "0";
                }
                if (!this_num.Contains("."))
                {
                    this_num = this_num + ".";
                    resultbox.Text = this_num;
                }

            }
            else if (boxtext == "+/-")
            {
                if (exist_sign)
                {
                    this_num = transfer(last_num);
                    resultbox.Text = this_num;
                }
                else if (exist_equal)
                {
                    last_num = transfer(last_num);
                    resultbox.Text = last_num;
                }
                else
                {
                    this_num = transfer(this_num);
                    resultbox.Text = this_num;
                }

            }
            exist_sign = false;
            exist_equal = false;

        }
        private void Button_Click_2(object sender, EventArgs e)
        {
            label1.Focus();
            if (last_sign == "+")
            {
                result = Convert.ToDouble(last_num) + Convert.ToDouble(this_num);
            }
            else if (last_sign == "-")
            {
                result = Convert.ToDouble(last_num) - Convert.ToDouble(this_num);
            }
            else if (last_sign == "x")
            {
                result = Convert.ToDouble(last_num) * Convert.ToDouble(this_num);
            }
            else if (last_sign == "÷")
            {
                if(Convert.ToDouble(this_num) == 0)
                {
                    resultbox.Text = "除数不能为0";
                    clear_result();
                    return;
                }
                result = Convert.ToDouble(last_num) / Convert.ToDouble(this_num);
            }
            exist_equal = true;
            exist_sign = false;
            last_num = result.ToString();
            resultbox.Text = result.ToString();

        }
        private void Button_Click_3(object sender, EventArgs e)
        {
            label1.Focus();
            if (exist_equal || exist_sign) return;
            if (this_num.Replace("-", "").Length > 1)
                this_num = this_num.Remove(this_num.Length - 1, 1);
            else
                this_num = "0";
            resultbox.Text = this_num;

        }
        private void Button_Click_4(object sender, EventArgs e)
        {
            Button a = (Button)sender;
            double result = 0;
            String boxtext = a.Text.ToString();
            label1.Focus();
            if (boxtext == "MS")
            {
                if (exist_equal)
                    memorynum = Convert.ToDouble(last_num);
                else
                    memorynum = Convert.ToDouble(this_num);

            }
            else if (boxtext == "MC")
            {
                memorynum = 0;
                //resultbox.Text = memorynum.ToString();
            }
            else if (boxtext == "MR")
            {
                this_num = memorynum.ToString();
                resultbox.Text = memorynum.ToString();
            }
            else if (boxtext == "M+")
            {
                if (exist_equal)
                    memorynum += Convert.ToDouble(last_num);
                else
                    memorynum += Convert.ToDouble(this_num);
            }

        }
        private String transfer(String num)
        {
            String num1 = num;
            if (num.Contains("-"))
            {
                num1 = num.Replace("-", "");
            }
            else
            {
                num1 = "-" + (num);
            }
            return num1;
        }
        private void clear_result()
        {
                this_num = "0";
                last_num = "0";
                exist_sign = false;
                exist_equal = false;
                last_sign = "+";
                result = 0;
                numbers.Clear();
                numbers.Enqueue(0);
                expression.Clear();
        }
        /*private void show_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.D0|| (Keys)e.KeyChar == Keys.D1 || (Keys)e.KeyChar == Keys.D2 || (Keys)e.KeyChar == Keys.D3 || (Keys)e.KeyChar == Keys.D4
                || (Keys)e.KeyChar == Keys.D5 || (Keys)e.KeyChar == Keys.D6 || (Keys)e.KeyChar == Keys.D7 || (Keys)e.KeyChar == Keys.D8 || (Keys)e.KeyChar == Keys.D9)
            {
                Button_Click_1(sender, e);
            }
        }*/
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.D0 || keyData == Keys.NumPad0)
            {
                button15.Focus();
                button15.PerformClick();
            }
            else if (keyData == Keys.D1 || keyData == Keys.NumPad1)
            {
                button19.Focus();
                button19.PerformClick();
            }
            else if (keyData == Keys.D2 || keyData == Keys.NumPad2)
            {
                button14.Focus();
                button14.PerformClick();
            }
            else if (keyData == Keys.D3|| keyData == Keys.NumPad3)
            {
                button99.Focus();
                button99.PerformClick();
            }
            else if (keyData == Keys.D4 || keyData == Keys.NumPad4)
            {
                button18.Focus();
                button18.PerformClick();
            }
            else if (keyData == Keys.D5 || keyData == Keys.NumPad5)
            {
                button13.Focus();
                button13.PerformClick();
            }
            else if (keyData == Keys.D6 || keyData == Keys.NumPad6)
            {
                button8.Focus();
                button8.PerformClick();
            }
            else if (keyData == Keys.D7 || keyData == Keys.NumPad7)
            {
                button17.Focus();
                button17.PerformClick();
            }
            else if (keyData == Keys.D8 || keyData == Keys.NumPad8)
            {
                button12.Focus();
                button12.PerformClick();
            }
            else if (keyData == Keys.D9 || keyData == Keys.NumPad9)
            {
                button7.Focus();
                button7.PerformClick();
            }
            else if (keyData == Keys.C)
            {
                button11.Focus();
                button11.PerformClick();
            }
            else if (keyData == Keys.Decimal)
            {
                button10.Focus();
                button10.PerformClick();
            }
            else if (keyData == Keys.Add)
            {
                button2.Focus();
                button2.PerformClick();
            }
            else if (keyData == Keys.Subtract || keyData == Keys.OemMinus)
            {
                button3.Focus();
                button3.PerformClick();
            }
            else if (keyData == Keys.Multiply)
            {
                button4.Focus();
                button4.PerformClick();
            }
            else if (keyData == Keys.Divide)
            {
                button5.Focus();
                button5.PerformClick();
            }
            else if (keyData == Keys.Enter || keyData == Keys.Oemplus)
            {
                button1.Focus();
                button1.PerformClick();
            }
            else if (keyData == Keys.Back)
            {
                button6.Focus();
                button6.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);

        }
        /*private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Keys key = e.KeyCode;
            if (e.Control != true)//如果没按Ctrl键
                return;
            switch (key)
            {
                case Keys.NumPad0:
                    //按下小键盘0以后
                    break;
                case Keys.NumPad1:
                    button19.PerformClick();//按下小键盘1以后
                    e.Handled = true;
                    break;
                case Keys.S:
                    button19.PerformClick();//按下S键以后
                    e.Handled = true;
                    break;
                case Keys.Up:
                    //按下向下键以后
                    break;
            }
        }*/
        private void Resultbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
