using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using student_reg_system.database;
using student_reg_system.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace student_reg_system.ViewModels
{

    public partial class GPACalculatorVM : ObservableObject
    {

        public double GPA;
        [ObservableProperty]
        public int? studentId;

        [ObservableProperty]
        public string? studentName;
        [ObservableProperty]
        public string? studenEmail;




        [ObservableProperty]
        public ObservableCollection<Module> moduleList = new ObservableCollection<Module>();
        [ObservableProperty]
        public ObservableCollection<string> gradeList = new ObservableCollection<string>();

        public GPACalculatorVM()
        {
            GradeList = new ObservableCollection<string>() { "A", "B", "C", "A+", "B+", "C+", "A-", "B-", "C-", "D", "F" };

        }
        [RelayCommand]
        public void Search()
        {
            using (StudentContext context = new StudentContext())
            {
                var student = context.Students.Include(s => s.Modules).FirstOrDefault(s => s.StudentIDStudent == StudentId);
                using (var db = new StudentContext())
                {
                    student.Modules = db.Students.Include(s => s.Modules).FirstOrDefault(s => s.StudentIDStudent == student.StudentIDStudent).Modules;
                }

                ModuleList = new ObservableCollection<Module>(student.Modules);
                StudentName = student.FirstNameStudent + " " + student.LastNameStudent;
                StudenEmail = student.EmailAdress;
            }



        }
        [RelayCommand]
        public void Save()
        {


            using (var db = new StudentContext())
            {
                var student = db.Students.Include(s => s.Modules).FirstOrDefault(s => s.StudentIDStudent == StudentId);


                student.Modules = db.Students.Include(s => s.Modules).FirstOrDefault(s => s.StudentIDStudent == student.StudentIDStudent).Modules;

                foreach (var module in ModuleList)
                {
                    foreach (var mod in student.Modules)
                    {
                        if (mod.ModuleId == module.ModuleId)
                        {

                            mod.Grade = module.Grade;


                        }
                    }
                    db.SaveChanges();
                }


            }
        }
        
            [RelayCommand]
            public void CalculateGPA()
            {
                double point = 0;
                double sumOfCredits = 0.0000001;
                double totalQualityPoints = 0;
                double totalCreditValues = 0;
                using (var db = new StudentContext())
                {
                    var student = db.Students.Include(s => s.Modules).FirstOrDefault(s => s.StudentIDStudent == StudentId);


                    student.Modules = db.Students.Include(s => s.Modules).FirstOrDefault(s => s.StudentIDStudent == student.StudentIDStudent).Modules;

                    foreach (var module in student.Modules)
                    {
                        double gradeValue = 0;
                    switch (module.Grade)
                    {
                        case "A+":
                            gradeValue = 4.0;
                            break;
                        case "A":
                            gradeValue = 3.7;
                            break;
                        case "A-":
                            gradeValue = 3.3;
                            break;
                        case "B+":
                            gradeValue = 3.0;
                            break;
                        case "B":
                            gradeValue = 2.7;
                            break;
                        case "B-":
                            gradeValue = 2.3;
                            break;
                        case "C+":
                            gradeValue = 2.0;
                            break;
                        case "C":
                            gradeValue = 1.7;
                            break;
                        case "C-":
                            gradeValue = 1.3;
                            break;
                        case "D":
                            gradeValue = 1.0;
                            break;
                        case "F":
                            gradeValue = 0.0;
                            break;
                        default:
                            break;
                    }


                    double qualityPoints = gradeValue * module.CreditValue;
                        totalQualityPoints += qualityPoints;
                        totalCreditValues += module.CreditValue;
                    }
                    double gpa;
                    if (totalCreditValues == 0)
                    {
                        // Avoid division by zero//////////////////////////////////////////////////////////////
                        gpa = 0;
                    }

                    else
                    {
                        gpa = totalQualityPoints / totalCreditValues;
                    }

                    MessageBox.Show($"{gpa}");
                }
            }


        }
    } 