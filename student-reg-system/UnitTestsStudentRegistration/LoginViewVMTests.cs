
using student_reg_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestsStudentRegistration
{
    
    public class LoginViewVMTests
    {

        [Fact]
        public void LoginAcess_WithValidUserCredentials_ShouldGrantUserAccess()
        {
            // Arrange
            var viewModel = new LoginViewVM();
            viewModel.UserName = "oshan";
            viewModel.PassWord = "oshan123";

            // Act
            viewModel.LoginAcess();

            // Assert
            Assert.Equal(1, LoginViewVM.CurrentUserId);
        }
    }
}
