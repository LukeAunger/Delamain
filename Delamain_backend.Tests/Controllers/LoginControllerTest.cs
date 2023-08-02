using System;
using Delamain_backend.Controllers;
using Delamain_backend.Models;
using Delamain_backend.Services.LoginService;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Delamain_backend.Tests.Controllers
{
	public class LoginControllerTest
	{
        private readonly ILoginService _loginService;
        private readonly LoginController _LoginController;

        public LoginControllerTest()
        {
			//Depedancies
			_loginService = A.Fake<ILoginService>();
            //SUT
            _LoginController = new LoginController(_loginService);
        }
        [Fact]
		public void LoginController_Login_Success()
		{
            //Arrange
            var userObj = A.Fake<LoginRequest>();
            A.CallTo(() => _loginService.Login(userObj)).Returns($" {userObj.Email} Logged in");
            var controller = _LoginController;
            //Act
            var result = controller.Login(userObj);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [Fact]
        public void LoginController_Register_Success()
        {
            //Arrange
            var userObj = A.Fake<LoginRegisterRequest>();
            A.CallTo(() => _loginService.Register(userObj)).Returns("Success");
            var controller = _LoginController;
            //Act
            var result = controller.Register(userObj);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [Fact]
        public void LoginController_verify_Success()
        {
            //Arrange
            var userObj = "";
            A.CallTo(() => _loginService.Verify(userObj)).Returns("Varified");
            var controller = _LoginController;
            //Act
            var result = controller.Verify(userObj);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [Fact]
        public void LoginController_ForgotPassword_Success()
        {
            //Arrange
            var userObj = "";
            A.CallTo(() => _loginService.ForgotPassword(userObj)).Returns("You may now reset your password");
            var controller = _LoginController;
            //Act
            var result = controller.ForgotPassword(userObj);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
        }
        [Fact]
        public void LoginController_ResetPassword_Success()
        {
            //Arrange
            var userObj = A.Fake<ResetPasswordRequest>();
            A.CallTo(() => _loginService.ResetPassword(userObj)).Returns("PasswordChanged! ");
            var controller = _LoginController;
            //Act
            var result = controller.ResetPassword(userObj);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}

