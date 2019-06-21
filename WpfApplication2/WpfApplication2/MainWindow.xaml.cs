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

namespace WpfApplication2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Form fro = new Form();
            //System.Windows.Forms.Application.Run(fro);
            fro.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 创建Form2，并添加事件处理函数
            Form2 frm = new Form2();
            frm.DataChange += new Form2.DataChangeHandler(DataChanged);
            frm.ShowDialog();
        }

        public void DataChanged(object sender, Form2.DataChangeEventArgs args)
        {
            // 更新窗体控件
            textBox1.Text = args.name;
            textBox2.Text = args.pass;
        }
    }
}

