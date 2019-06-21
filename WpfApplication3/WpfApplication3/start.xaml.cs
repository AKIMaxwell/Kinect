using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace WpfApplication3
{
    /// <summary>
    /// start.xaml 的交互逻辑
    /// </summary>
    public partial class start : Window
    {
        bool is_had_try = false;
        bool is_had = false;

        public start()
        {
            InitializeComponent();
            this.Closing += new System.ComponentModel.CancelEventHandler(this.start_formClosing);
            
        }

        private void Button_1_MoveOn(object sender, RoutedEventArgs e)
        {
            Button_Yes();
            is_had = true;
        }

        private void Button_Yes()
        {
            bYes b_yes = new bYes();
            b_yes.Show();
        }

        private void button_2_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (button_2.Margin.Bottom == 20 && !is_had_try)
            {
                is_had_try = true;
                button_2.Margin = new Thickness(0, 0, 20, 100);
            }
            else if (button_2.Margin.Bottom == 20 && button_2.Margin.Right == 20)
            {
                button_2.Margin = new Thickness(0, 0, 160, 20);
                button_1.Margin = new Thickness(0, 0, 20, 20);
            }
            else
            {
                button_2.Margin = new Thickness(0, 0, 20, 20);
                button_1.Margin = new Thickness(0, 0, 160, 20);
            }

        }

        private void start_formClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!is_had && is_had_try)
                System.Windows.Forms.MessageBox.Show("即使关掉也改变不了你是SB的事实", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            else if(!is_had)
            {
                System.Windows.Forms.MessageBox.Show("确定要取消吗", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                e.Cancel = true;
            }
            
        }

        private void button_2_Click(object sender, RoutedEventArgs e)
        {
            Button_no();
            is_had = true;
        }

        private void Button_no()
        {
            bno _bno = new bno();
            _bno.Show();
        }
    }
}
