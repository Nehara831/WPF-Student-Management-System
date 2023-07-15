using student_reg_system.Models;
using student_reg_system.database;
using student_reg_system.ViewModels;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace student_reg_system.UnitTestsStudentRegistration
{
    public class GPACalculatorVMTests
    {

        private readonly string _path = @"D:\UOR education\SEM03\PP-Project\GUI-Project\Updated-Repo\WPF-Student-Management-System\student-reg-system\sqlite\StudentData.db";
        [Fact]
        public void SearchCommand_FetchesStudentFromDatabase()
        {
            // Arrange
            var studentId = 23;
            var studentName = "Ashen Nethsara";
            var studentEmail = "ashen@gmail.com";
            var moduleList = new List<Module>()
            {
               ////////////////// new Module { ModuleId = 3301, Grade = "A-", CreditValue = 2 },
               //////////////// new Module { ModuleId = 3305, Grade = "C+", CreditValue = 3 },
               
            };
            var student = new Student
            {
                StudentIDStudent = studentId,
                FirstNameStudent = "Ashen",
                LastNameStudent = "Nethsara",
                EmailAdress = studentEmail,
                Modules = moduleList
            };

            var serviceProvider = new ServiceCollection()
                .AddDbContext<StudentContext>(optionsBuilder => optionsBuilder.UseSqlite($"DataSource={_path}"))
                .BuildServiceProvider();

            var vm = new GPACalculatorVM { StudentId = studentId };
            vm.ModuleList = new ObservableCollection<Module>();

            // Act
            vm.Search();

            // Assert
            Assert.Equal(studentName, vm.StudentName);
            Assert.Equal(studentEmail, vm.StudenEmail);
            Assert.Equal(moduleList.Count, vm.ModuleList.Count);
            for (int i = 0; i < moduleList.Count; i++)
            {
                Assert.Equal(moduleList[i].ModuleId, vm.ModuleList[i].ModuleId);
               // Assert.Equal(moduleList[i].Grade, vm.ModuleList[i].Grade);
                Assert.Equal(moduleList[i].CreditValue, vm.ModuleList[i].CreditValue);
            }
        }

         


    }
}