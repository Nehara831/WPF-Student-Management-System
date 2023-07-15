using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using student_reg_system.database;
using student_reg_system.Models;
using System.Reflection.Metadata.Ecma335;

namespace UnitTestsStudentRegistration
{
    public class StudentDataContextTesting
    {

        [Fact]
        public void InsertStudentIntoDatabase()
        {
            var options = new DbContextOptionsBuilder<StudentContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase") 
                .Options;

            var moduleList = new List<Module>()
            {
                new Module { ModuleId = 3301, Grade = "A-", CreditValue = 2 },
                new Module { ModuleId = 3305, Grade = "C+", CreditValue = 3 },

            };

            using (var context = new StudentContext())
            {
                var student = new Student
                {
                    StudentIDStudent = 4097,
                    FirstNameStudent = "Ashen",
                    LastNameStudent = "Nethsara",
                    EmailAdress = "ashen@gmail.com",
                    Modules =moduleList
                };
            }

            using(var context = new StudentContext())
            {
                Assert.Equal(4, context.Students.Count());
              
            }
        }
    }
}
