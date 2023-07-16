using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using student_reg_system.database;
using student_reg_system.Models;
using student_reg_system.ViewModels;
using student_reg_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestsStudentRegistration
{
    public class StudentREgistrationVMTests
    {
        private readonly string _path = @"C:\Users\User\OneDrive\Desktop\WPF-Projects\Student-sys\student-reg-system\StudentData.db";
        private int Id=4234;
        private string FName ="Ashen";
        private string LName = "Nethsara";
        private string Adress = "Gampaha";
        private string DoB = "1999-02-24";
        private string Email = "ashen@gmail.com";
        private List<Module> ModuleList = new List<Module>()
{
    new Module { ModuleId = 3301,ModuleName="Analog", Grade = "C", CreditValue = 3.0,Department="Computer",IsSelected=false },
    new Module { ModuleId = 3304,ModuleName="Digital", Grade = "A+", CreditValue = 2.0,Department="Computer",IsSelected=false  },
};
        [Fact]
     
        public void InsertStudentIntoDatabase()
        {
            int pre_count;
            var serviceProvider = new ServiceCollection()
                .AddDbContext<StudentContext>(optionsBuilder => optionsBuilder.UseSqlite($"DataSource={_path}"))
                .BuildServiceProvider();

           

            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<StudentContext>();
                pre_count = context.Students.Count();

                var user = context.Users.FirstOrDefault(u => u.IDUser == 4097);
                var moduleList = context.Modules.ToList();
                var student = new Student
                {
                    StudentIDStudent = 4234,
                    FirstNameStudent = "Ashen",
                    LastNameStudent = "Nethsara",
                    EmailAdress = "ashen@gmail.com",
                    DateofBirthStudent = "1999-02-24",
                    AdressStudent = "Gampaha",
                    Modules = moduleList
                };
            

            context.Students.Add(student);
                context.SaveChanges();
            }

           
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<StudentContext>();

                // Retrieve the student with the provided student ID
                var studentId = 4234; // Replace with the desired student ID
                var addedStudent = context.Students.FirstOrDefault(s => s.StudentIDStudent == studentId);
                addedStudent.Modules = context.Students.Include(s => s.Modules).FirstOrDefault(s => s.StudentIDStudent == Id).Modules;
                
                Assert.Equal(pre_count + 1, context.Students.Count());

                // Assert statements
                Assert.NotNull(addedStudent); // Check that the student is not null (exists)
                Assert.Equal("Ashen", addedStudent.FirstNameStudent);
                Assert.Equal("Nethsara", addedStudent.LastNameStudent);
                Assert.Equal("Gampaha", addedStudent.AdressStudent);
                Assert.Equal("1999-02-24", addedStudent.DateofBirthStudent);
                Assert.Equal("ashen@gmail.com", addedStudent.EmailAdress);

                // Compare the module list
                Assert.Equal(ModuleList.Count, addedStudent.Modules.Count);

                for (int i = 0; i < ModuleList.Count; i++)
                {
                    Assert.Equal(ModuleList[i].ModuleId, addedStudent.Modules.ElementAt(i).ModuleId);
                    Assert.Equal(ModuleList[i].ModuleName, addedStudent.Modules.ElementAt(i).ModuleName);
                    Assert.Equal(ModuleList[i].Grade, addedStudent.Modules.ElementAt(i).Grade);
                    Assert.Equal(ModuleList[i].CreditValue, addedStudent.Modules.ElementAt(i).CreditValue);
                    Assert.Equal(ModuleList[i].Department, addedStudent.Modules.ElementAt(i).Department);
                    Assert.Equal(ModuleList[i].IsSelected, addedStudent.Modules.ElementAt(i).IsSelected);
                }

                
            }

        }
       

// ...

   
}
}
