using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace student_reg_system.Models
{
    public class StudentModules
    {
        [Key]
        public int ModuleId { get; set; }
        [Key]
        public int StudentId { get; set; }

        public string Grade { get; set; }

        public string ModukeName { get; set; }

        public double CreditValue { get; set; }
    }
}
