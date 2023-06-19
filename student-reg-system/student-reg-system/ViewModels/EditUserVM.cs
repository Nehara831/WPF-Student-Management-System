using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using student_reg_system.database;
using student_reg_system.Models;
using student_reg_system.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace student_reg_system.ViewModels
{
    partial class EditUserVM : ObservableObject
    {
        [ObservableProperty]
        public static int userId;
        [ObservableProperty]
        public string? userFirstName;
        [ObservableProperty]
        public string? userLastName;
        [ObservableProperty]
        public string? userEmail;
        [ObservableProperty]
        public static string? userDepartment;
        [ObservableProperty]
        public string userUserName;
        [ObservableProperty]
        public string userPassword;
        [ObservableProperty]
        public int userPhone;


        [ObservableProperty]
        public ObservableCollection<User> usersList;
        [ObservableProperty]
        public ObservableCollection<Module> selectedModules = new ObservableCollection<Module>();
        [ObservableProperty]

        public ObservableCollection<int> users;

        [ObservableProperty]
        public ObservableCollection<Module> moduleList;

        public EditUserVM(User user, List<Module> moduleList)
        {

            ModuleList = new ObservableCollection<Module>(moduleList);
            UserId = user.IDUser;
            UserFirstName = user.FirstNameUser;
            UserLastName = user.LastNameUser;
            UserEmail = user.EmailUser;
            UserDepartment = user.DepartmentUser;
            UserPhone = user.PhoneUser;
            UserPassword = user.Password;
            UserUserName = user.UserName;
            UpdateSelectedModulesForUser(user);


        }
        [RelayCommand]
        public void AddUser()
        {

            using (var db = new StudentContext())
            {
                var user1 = db.Users.FirstOrDefault(u => u.IDUser == UserId);
                if (user1 != null)
                {
                    db.Remove(user1);
                    db.SaveChanges();
                }


                foreach (var module in ModuleList)
                {

                    bool isSelected = module.IsSelected;


                    if (isSelected)
                    {

                        SelectedModules.Add(db.Modules.FirstOrDefault(u => u.ModuleName == module.ModuleName));

                    }

                }


                User user = new User()
                {
                    IDUser = UserId,
                    FirstNameUser = UserFirstName,
                    LastNameUser = UserLastName,
                    EmailUser = UserEmail,
                    PhoneUser = UserPhone,
                    DepartmentUser = UserDepartment,

                    Modules = SelectedModules.ToList(),
                    Password = UserPassword,
                    UserName = UserUserName,
                    Students = new List<Student>(),
                };

                db.Users.Add(user);
                db.SaveChanges();

            }

            LoadUser();
            using (var db = new StudentContext())
            {
                var listusers = db.Users


                .ToList();
                UsersList = new ObservableCollection<User>(listusers);

            }
            ClearTextBoxes();
            var currentWindow = Application.Current.Windows.OfType<EditUser>().SingleOrDefault(w => w.IsActive);
            currentWindow?.Close();
            var currentWindow1 = Application.Current.Windows.OfType<AdminView>().SingleOrDefault(w => w.IsActive);
            currentWindow1?.Close();


            AdminView newview = new AdminView();
            newview.Show();



        }
        [RelayCommand]
        public void LoadUser()
        {

            using (var db = new StudentContext())
            {
                var listusers = db.Users


                .ToList();
                UsersList = new ObservableCollection<User>(listusers);


                var modules = db.Modules

                .ToList();

                if (modules == null)
                {
                    ModuleList = new ObservableCollection<Module>();
                }
                ModuleList = new ObservableCollection<Module>(modules);


            }
        }
        [RelayCommand]
        public void UpdateSelectedModulesForUser(User user)
        {


            foreach (var moduleL in ModuleList)
            {
                bool exists = false;
                if (user.Modules != null)

                    foreach (var module in user.Modules)
                    {

                        if (moduleL.ModuleId == module.ModuleId)
                        {
                            exists = true;
                        }
                    }



                if (exists)
                {
                    moduleL.IsSelected = true;
                }
            }
        }


        [RelayCommand]
        public void ClearTextBoxes()
        {
            var window = Application.Current.Windows.OfType<EditUser>().SingleOrDefault(x => x.IsActive);

            window.t1.Clear();
            window.t2.Text = "";
            window.t3.Text = "";
            window.t4.Text = "";
            window.t5.Text = "";
            window.t6.Text = "";
            window.t7.Text = "";
            window.t8.Text = "";

            // window.myComboBox.SelectedItem = null;
        }
        [RelayCommand]
        public void DeleteUser(User user)

        {
            using (var db = new StudentContext())
            {
                db.Remove(user);
                db.SaveChanges();


            }
            using (var db = new StudentContext())
            {
                var listusers = db.Users


                .ToList();
                UsersList = new ObservableCollection<User>(listusers);

            }

        }
    }
}

