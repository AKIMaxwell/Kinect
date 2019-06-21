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

namespace WpfApplication3
{
    /// <summary>
    /// bYes.xaml 的交互逻辑
    /// </summary>
    public partial class bYes : Window
    {
        public bYes()
        {
            InitializeComponent();
        }

        private void sure_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
