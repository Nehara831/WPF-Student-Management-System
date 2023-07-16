using CommunityToolkit.Mvvm.ComponentModel;
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
using System.Windows.Controls;
using System.Windows.Data;
using student_reg_system.database;
using System.Windows.Documents;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.CodeDom.Compiler;
using System.Drawing;
using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;
using student_reg_system.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Windows.Media;
using Microsoft.Data.Sqlite;
using student_reg_system.Views.AlertWindows;

namespace student_reg_system.ViewModels
{
    public partial class StudentRegVM : ObservableObject
    {



        [ObservableProperty]
        public int id;

        [ObservableProperty]
        public string? fName;
        [ObservableProperty]
        public string? lName;
        [ObservableProperty]
        public string? doB;
        [ObservableProperty]
        public string? adres;

        [ObservableProperty]
        public string searchName;

        [ObservableProperty]
        public string? email;
        [ObservableProperty]
        public int userIdObservable;
        [ObservableProperty]
        public string userFullNameObservable;
        [ObservableProperty]
        public string userLNameObservable;
        [ObservableProperty]
        public string userDepartmentObservable;

        [ObservableProperty]
        public string userEmailObservable;
        [ObservableProperty]
        public int testName;
        /* [ObservableProperty]*/

        [ObservableProperty]
        public static string testName2;

        [ObservableProperty]
        public static ObservableCollection<Student> studentList;

        [ObservableProperty]
        public ObservableCollection<Module> moduleListStudent;


        public ObservableCollection<Module> SelectedModulesStudent;


        public List<Module> selectedModules = new List<Module>();

        public bool IsSelectedModule { get; set; }



        public StudentRegVM()
        {
            LoadStudent();

            UserIdObservable = LoginViewVM.CurrentUserId;


        }
        public StudentRegVM(Student student, List<Module> moduleList)
        {

            ModuleListStudent = new ObservableCollection<Module>(moduleList);
            Id = student.StudentIDStudent;
            FName = student.FirstNameStudent;
            LName = student.LastNameStudent;
            Adres = student.AdressStudent;
            DoB = student.DateofBirthStudent;
            Email = student.EmailAdress;
            UpdateSelectedModulesForStudent(student);

        }



        [RelayCommand]

        public void AddStudent()

        {
            DoB = getDateOfBirth(DoB);


            using (var db = new StudentContext())
            {
                var existingStudent = db.Students.FirstOrDefault(u => u.StudentIDStudent == Id);
                if (existingStudent != null)
                {
                    db.Remove(existingStudent);
                    db.SaveChanges();
                }
                var user = db.Users.FirstOrDefault(u => u.IDUser == LoginViewVM.CurrentUserId);


                foreach (var module in ModuleListStudent)
                {
                    bool isSelected = module.IsSelected;

                    if (isSelected)
                    {
                        selectedModules.Add(db.Modules.FirstOrDefault(u => u.ModuleName == module.ModuleName));

                    }

                }

                Student student = new Student()
                {
                    StudentIDStudent = Id,
                    FirstNameStudent = FName,
                    LastNameStudent = LName,
                    DateofBirthStudent = DoB,
                    AdressStudent = Adres,
                    EmailAdress = Email,

                    Modules = selectedModules,
                    Users = new List<User>() { user },

                };


                db.Students.Add(student);
                if (user != null)
                {
                    if (user.Students == null)
                    {
                        user.Students = new List<Student>();
                    }
                    user.Students.Add(student);
                }

                db.SaveChanges();
            }



            LoadStudent();


            ClearTextBoxes();
            var currentWindow = Application.Current.Windows.OfType<StudentRegView>().SingleOrDefault(w => w.IsActive);
            currentWindow?.Close();
            var currentWindow1 = Application.Current.Windows.OfType<UserView>().SingleOrDefault(w => w.IsActive);
            currentWindow1?.Close();
            UserView newview = new UserView();

            newview.Show();

        }
    


        private String getDateOfBirth(String dob)
        {
            string[] parts = dob.Split(' ');

            string date = parts[0];

            return date;
        }

        private bool IsNumeric(string input)
        {
            return int.TryParse(input, out _);
        }

        public void LoadStudent()
        {
            using (var db = new StudentContext())
            {

                var user = db.Users.Include(u => u.Students).SingleOrDefault(u => u.IDUser == LoginViewVM.CurrentUserId);
               

                // StudentList = new ObservableCollection<Student>(db.Students);
                if (user != null && user.Students != null)
                {
                    StudentList = new ObservableCollection<Student>(user.Students);
                }
                
                UserIdObservable = user.IDUser;
                UserFullNameObservable = user.FirstNameUser + " " + user.LastNameUser;
                UserLNameObservable = user.LastNameUser;
                UserDepartmentObservable = user.DepartmentUser + "  Department";
                UserEmailObservable = user.EmailUser;

                

                var modules = db.Modules
                    .Where(m => m.Department == user.DepartmentUser)
                    .ToList();


                ModuleListStudent = new ObservableCollection<Module>(modules);







            }

        }
        [RelayCommand]
        public void ShowModules()
        {
            
            using (var db = new StudentContext())
            {
                var testStudent = db.Students.Include(s => s.Modules).FirstOrDefault(s => s.StudentIDStudent == TestName);


                TestName2 = "";
                if (testStudent != null)
                {

                    if (testStudent.Modules != null)
                    {
                        foreach (Module module in testStudent.Modules)
                        {
                            TestName2 += " " + module.ModuleName;
                        }
                    }

                }
            }
        }


        [RelayCommand]
        public void ClearTextBoxes()
        {
            var window = Application.Current.Windows.OfType<StudentRegView>().SingleOrDefault(x => x.IsActive);

            window.t1.Text = "";
            window.t2.Text = "";
            window.t3.Text = "";
           
            window.t5.Text = "";
            window.t6.Text = "";
        }

        [RelayCommand]
        public void UpdateSelectedModulesForStudent(Student student)
        {

            
            foreach (var moduleL in ModuleListStudent)
            {
                bool exists = false;
                foreach (var module in student.Modules)
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
        public void EditStudent(Student student)
        {
            List<Module> DepModuleList = new List<Module>();
            List<Module> StudentModuleList = new List<Module>();

            using (var db = new StudentContext())
            {
                var user = db.Users.Include(u => u.Students).SingleOrDefault(u => u.IDUser == LoginViewVM.CurrentUserId);

                student.Modules = db.Students.Include(s => s.Modules).FirstOrDefault(s => s.StudentIDStudent == student.StudentIDStudent).Modules;

                DepModuleList = db.Modules.Where(m => m.Department == user.DepartmentUser).ToList();

            }

            var editView = new StudentRegView(student, DepModuleList);
            editView.Show();
        }

        [RelayCommand]
        public void Search()
        {
            using (StudentContext context = new StudentContext())
            {
                ObservableCollection<Student> stuList = new ObservableCollection<Student>();
                foreach (var st in StudentList)
                {

                    if (st.FirstNameStudent.StartsWith(SearchName, StringComparison.OrdinalIgnoreCase))
                    {
                        stuList.Add(st);
                    }

                }
                if (stuList.Count > 0)
                {
                    StudentList = new ObservableCollection<Student>(stuList);
                }
                else
                {

                    AlertBox notfoundalert = new AlertBox("Student not found!", "Student is not in the datebase");
                    notfoundalert.Show();
                }

            }

        }

        [RelayCommand]
        public void DeleteStudent(Student student)

        {
            using (var db = new StudentContext())
            {
                db.Remove(student);
                db.SaveChanges();

              

            }
            using (var db = new StudentContext())
            {
                var listusers = db.Students


                .ToList();
                StudentList = new ObservableCollection<Student>(listusers);

            }

        }
    }

}

   
