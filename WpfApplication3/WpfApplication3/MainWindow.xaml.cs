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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;

namespace WpfApplication3
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        bool is_had = false;
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += new System.ComponentModel.CancelEventHandler(this.main_closing);
        }

        private void button_1_Click(object sender, RoutedEventArgs e)
        {
            is_had = true;
            start _start = new start();
            _start.Show();
        }

        private void main_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!is_had)
            {
                while (System.Windows.Forms.MessageBox.Show("确定不试一试吗", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.OK) ;
                e.Cancel = true;
            }
        }
    }
}
