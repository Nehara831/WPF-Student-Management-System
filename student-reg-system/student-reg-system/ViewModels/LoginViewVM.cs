using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.ApplicationInsights.Extensibility.Implementation;
using student_reg_system.database;
using student_reg_system.Models;
using student_reg_system.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace student_reg_system.ViewModels
{
    public partial class LoginViewVM:ObservableObject
    {

        [ObservableProperty]
        public string userName;
        [ObservableProperty]
        public string passWord;
        [ObservableProperty]
        public string loginView;
        public  static int CurrentUserId { get; set; }
       


        [RelayCommand]
        public  void LoginAcess()
        {
           


            
          using (StudentContext context = new StudentContext())
          {
          
              bool userfound = context.Users.Any(user => user.UserName == UserName && user.Password == PassWord);
              bool adminfound = context.AdminAuthentications.Any(admin => admin.Username == UserName && admin.Password == PassWord);


                    if (userfound)
              {
                  var user = context.Users.FirstOrDefault(u => u.UserName == UserName && u.Password == PassWord);

                  CurrentUserId = user.IDUser;
                   

                  GrantUserAcess();

              }
              else if (adminfound) {
                        var admin = context.AdminAuthentications.FirstOrDefault(a => a.Username == UserName && a.Password == PassWord);

                        CurrentUserId = admin.AdminId;
                    

                        GrantAdminAcess();
                    }
              else
              {
                  MessageBox.Show("User Not Found");
              }

          }
      

        }
        [RelayCommand]
        public static void GrantUserAcess()
        {
            UserView userView= new UserView ();
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?.Close();
            userView.Show();
            

        }
        public static void GrantAdminAcess()
        {
           AdminView adminView = new AdminView();
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?.Close();
            adminView.Show();

        }


    }
}
