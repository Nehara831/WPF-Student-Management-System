using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
   
    partial class ModuleRegVM : ObservableObject
    {
        [ObservableProperty]
        public int moduleIdObservable;
        [ObservableProperty]
        public string moduleNameObservable;
        [ObservableProperty]
        public char gradeObservable;
        [ObservableProperty]
        public double creditValueObservable;
        [ObservableProperty]
        public double gradePointObservable;
       
        
        [ObservableProperty]
        public string userModuleDepartmentObservable;
        public ModuleRegVM()
        {
            
        }


        [RelayCommand]

        public void AddModule()
        {
           
            using (var db = new StudentContext())
            {
                Module module = new Module()
                {
                    ModuleId = ModuleIdObservable,
                    ModuleName = ModuleNameObservable,

                    CreditValue = creditValueObservable,
                    GradePoint = gradePointObservable,
                    
                   Department=userModuleDepartmentObservable,
                  /* Grade="",*/

                };
                db.Modules.Add(module);
              
                db.SaveChanges();

            }
            ClearTextBoxes();
        }
        [RelayCommand]
        public void ClearTextBoxes()
        {
            var window = Application.Current.Windows.OfType<ModuleRegistrationxaml>().SingleOrDefault(x => x.IsActive);

            window.t1.Text = "";
            window.t2.Text = "";
            window.t3.Text = "";
            window.t4.Text = "";
            window.t5.Text = "";
           
        }


    }
}