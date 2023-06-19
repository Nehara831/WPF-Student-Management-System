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
    /// Interaction logic for StudentDataPage.xaml
    /// </summary>
    public partial class StudentDataPage : Page
    {
        public StudentDataPage()
        {
            InitializeComponent();
            DataContext = new StudentRegVM();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Your code to handle the TextChanged event goes here
        }


        private void TextBox_TextChanged2(object sender, TextChangedEventArgs e)
        {
            // Your code to handle the TextChanged event goes here
        }

        private void CalculateGPA(object sender, TextChangedEventArgs e)
        {
            // Your code to handle the TextChanged event goes here
        }

        private void AddStudent(object sender, TextChangedEventArgs e)
        {
            // Your code to handle the TextChanged event goes here
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StudentRegView newview = new StudentRegView();
            newview.Show();
        }
    }
}
