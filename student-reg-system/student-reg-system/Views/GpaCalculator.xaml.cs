using student_reg_system.ViewModels;
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

namespace student_reg_system.Views
{
    /// <summary>
    /// Interaction logic for GpaCalculator.xaml
    /// </summary>
    public partial class GpaCalculator : Page
    {
        public GpaCalculator()
        {
            InitializeComponent();
            DataContext = new GPACalculatorVM();
           
        }

        private void CalculateGPA(object sender, RoutedEventArgs e)
        {
        }

        private void TextBox_TextChanged2(object sender, TextChangedEventArgs e)
        {
            // Your code to handle the TextChanged event goes here
        }













    }
}
