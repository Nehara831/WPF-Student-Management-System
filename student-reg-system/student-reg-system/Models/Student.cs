using MessagePack;
using student_reg_system.database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace student_reg_system.Models
{
    public class Student
    {
        [Key]
        [ForeignKey("Users")]
        public int StudentIDStudent { get; set; }
        
        public string FirstNameStudent { get; set; }
        public string LastNameStudent{ get; set; }
        public string DateofBirthStudent { get; set; }
        public string AdressStudent { get; set; }

        public double Gpa { get; set; }
  
        public string EmailAdress { get; set; }
        public ICollection<Module> Modules { get; set; }

        public ICollection<User> Users { get; set; }

        public Student()
        {

            

        }
        
     
    }
}