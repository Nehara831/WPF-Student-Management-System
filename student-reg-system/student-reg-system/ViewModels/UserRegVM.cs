﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using student_reg_system.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using student_reg_system.database;
using student_reg_system.Views;

using Microsoft.EntityFrameworkCore;
using student_reg_system.Views.AlertWindows;

namespace student_reg_system.ViewModels
{
    partial class UserRegVM : ObservableObject
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
        public string searchName;

        [ObservableProperty]
        public   ObservableCollection<User> usersList;
        [ObservableProperty]
        public ObservableCollection<Module> selectedModules = new ObservableCollection<Module>();
        [ObservableProperty]

        public ObservableCollection <int> users ;

        [ObservableProperty]
        public ObservableCollection<Module> moduleList;
        public UserRegVM()
        {
            UsersList = new ObservableCollection<User>();
            LoadUser();
            using (var db = new StudentContext())
            {
                var listusers = db.Users


                .ToList();
                UsersList = new ObservableCollection<User>(listusers);

            }


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
           /* UserId = 0;
            UserDepartment= null;*/

            var currentWindow = Application.Current.Windows.OfType<UserRegistration>().SingleOrDefault(w => w.IsActive);
            currentWindow?.Close();
            var currentWindow1 = Application.Current.Windows.OfType<AdminView>().SingleOrDefault(w => w.IsActive);
            currentWindow1?.Close();


            AdminView newview = new AdminView();
            newview.Show();
           

           
        }


        [RelayCommand]
        public void Search()
        {
            
            using (StudentContext context = new StudentContext())
            {

             
                ObservableCollection<User> userl = new ObservableCollection<User>();
                foreach (var user in UsersList)
                {
                  

                    if (user.FirstNameUser.StartsWith(SearchName, StringComparison.OrdinalIgnoreCase))
                    {
                        
                        userl.Add(user);
                    }

                }
                if (userl.Count > 0)
                {
                    
                    UsersList = new ObservableCollection<User>(userl);
                }
                else
                {

                    AlertBox notfoundalert = new AlertBox("User not found!", "User is not in the database.");
                    notfoundalert.Show();
                }

            }

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
        public void EditUser(User user)
        {
            List<Module> DepModuleList = new List<Module>();
           

            using (var db = new StudentContext())
            {

                user.Modules = db.Users.Include(u => u.Modules).FirstOrDefault(u => u. IDUser== user.IDUser).Modules;
                DepModuleList = db.Modules.ToList();

            }

            var editView = new EditUser(user, DepModuleList);
            editView.Show();
           
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
            var window = Application.Current.Windows.OfType<UserRegistration>().SingleOrDefault(x => x.IsActive);
            var viewModel = window.DataContext as UserRegVM;

            if (viewModel != null)
            {
                viewModel.UserDepartment = string.Empty;
                viewModel.UserId = default;
                // Clear other properties as necessary
            }

            window.t1.Clear();
            window.t2.Clear();
            window.t3.Clear();
            window.t4.Clear();
            window.t5.Clear();
            window.t6.Clear();
            window.t7.Clear();
            window.t8.Clear();
            
           
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
