using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore.Infrastructure;
using student_reg_system.database;

namespace student_reg_system
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {



        protected override void OnStartup(StartupEventArgs e)
        {
            DatabaseFacade facade1= new DatabaseFacade(new StudentContext());
           
            facade1.EnsureCreated();
            
        }
    }
}
