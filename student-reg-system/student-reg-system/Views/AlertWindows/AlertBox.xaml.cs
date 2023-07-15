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

namespace student_reg_system.Views.AlertWindows
{
    /// <summary>
    /// Interaction logic for AlertBox.xaml
    /// </summary>
    public partial class AlertBox : Window
    {
        public AlertBox(string title, string content)
        {
            InitializeComponent();
            titleTextBlock.Text = title;
            messageTextBlock.Text = content;
        }

        private void Closebtn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
