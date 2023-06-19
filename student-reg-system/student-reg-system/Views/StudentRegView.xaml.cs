using Microsoft.EntityFrameworkCore;
using student_reg_system.database;
using student_reg_system.Models;
using student_reg_system.ViewModels;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace student_reg_system.Views
{
    /// <summary>
    /// Interaction logic for StudentRegView.xaml
    /// </summary>
    public partial class StudentRegView : Window
    {
      

        public StudentRegView()
        {
            InitializeComponent();
            DataContext = new StudentRegVM();
           

        }
        public StudentRegView(Student student, List<Module> moduleList)
        {
            InitializeComponent();
            DataContext = new StudentRegVM(student, moduleList);
         

        }



        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            /*Window userViewWindow = Application.Current.Windows.OfType<UserView>().SingleOrDefault(x => x.Name == "UserView");
            userViewWindow?.Close();*/
            this.Close();


        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            /* Window userViewWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.Name == "UserView");

             userViewWindow.WindowState = WindowState.Minimized;*/
            this.WindowState = WindowState.Minimized;
        }

    }
}
