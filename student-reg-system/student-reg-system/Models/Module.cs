using student_reg_system.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace student_reg_system.Models
{
    public class Module
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string Grade { get; set; }
        public double CreditValue { get; set; }
        


        public string Department { get; set; }


        public ICollection<Student> Students { get; set; }
        public ICollection<User> Users { get; set; }


        private bool _isSelected;


        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;

            }
        }

       
        public Module() { }



    }
}
