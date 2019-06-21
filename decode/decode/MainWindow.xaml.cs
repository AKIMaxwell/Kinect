﻿using System;
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

namespace decode
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            textBlock.Text = deCode(textBox.Text);
        }

        internal static string deCode(string text1)
        {
            try
            {
            byte[] bytes = Convert.FromBase64String(text1);
            return Encoding.Unicode.GetString(bytes, 0, bytes.Length);

            }
            catch
            {
                return "";
            }
        }
    }
}
