using System;
using System.Security.Cryptography;
using Delamain_backend.Data;
using Delamain_backend.Models;
using Delamain_backend.Services.emailService;
using Delamain_backend.Services.LoginService;
using Delamain_backend.Services.userRequestService;
using FakeItEasy;
using FluentAssertions;
using MailKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Delamain_backend.Tests.Repository
{
    public class LoginServiceTest
    {
        private async Task<DataContext> GetDataContextAsync()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Logins.CountAsync() <= 0)
            {
                for (int i = 0; i < 1; i++)
                {
                    var password = "Password";
                    CreatePasswordHash(password,
                        out byte[] TestpasswordHash, out byte[] TestpasswordSalt);
                    databaseContext.Logins.Add(
                        new Login()
                        {
                            Email = "Luke@Gmail.com",
                            PasswordSalt = TestpasswordSalt,
                            PasswordHash = TestpasswordHash,
                            VerificationToken = CreateRandomToken()
                        }); databaseContext.Logins.Add(
                        new Login()
                        {
                            Email = "Gill@Gmail.com",
                            PasswordSalt = TestpasswordSalt,
                            PasswordHash = TestpasswordHash,
                            VerificationToken = CreateRandomToken(),
                            PasswordResetToken = "21774D4464A4C6FA2E027ACE907F9F67E5D4F68E5D30F7332DD08D6F6E2984C581E993204DD7B94B65CF659A77852D75A2DBFF36BC20163BF5FB030034844048",
                            ResetTokenExpires = DateTime.Now
                        }); databaseContext.Logins.Add(
                        new Login()
                        {
                            Email = "Bob@Bob",
                            PasswordSalt = TestpasswordSalt,
                            PasswordHash = TestpasswordHash,
                            VerificationToken = CreateRandomToken(),
                            VerifieAt = DateTime.Now,
                            PasswordResetToken = "21774D4464A4C6FA2E027ACE907F9F67E5D4F68E5D30F7332DD08D6F6E2984C581E993204DD7B94B65CF659A77852D75A2DBFF36BC20163BF5FB030034844048",
                            ResetTokenExpires = DateTime.Now
                        });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Fact]
        public async void userRequestService_Register_Success()
        {
            //Arrange
            var newuser = new LoginRegisterRequest()
            {
                Email = "Jean@Hotmail",
                Password = "Password.123",
                ConfirmPassword = "Password.123"
            };
            var dbContext = await GetDataContextAsync();
            var email = A.Fake<IEmailSender>();
            var Service = new LoginService(dbContext, email);

            //Act
            var result = Service.Register(newuser);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<string>>();
            result.Result.Should().BeEquivalentTo("Success");
        }
        [Fact]
        public async void userRequestService_Login_Success()
        {
            //Arrange
            var newuser = new LoginRequest()
            {
                Email = "Bob@Bob",
                Password = "Password",
            };
            var dbContext = await GetDataContextAsync();
            var email = A.Fake<IEmailSender>();
            var Service = new LoginService(dbContext, email);

            //Act
            var result = Service.Login(newuser);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<string>>();
            result.Result.Should().BeEquivalentTo($" {newuser.Email} Logged in");
        }
        [Fact]
        public async void userRequestService_Login_NotVarified()
        {
            //Arrange
            var newuser = new LoginRequest()
            {
                Email = "Gill@Gmail.com",
                Password = "Password",
            };
            var dbContext = await GetDataContextAsync();
            var email = A.Fake<IEmailSender>();
            var Service = new LoginService(dbContext, email);

            //Act
            var result = Service.Login(newuser);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<string>>();
            result.Result.Should().BeEquivalentTo("Not verified");
        }
        [Fact]
        public async void userRequestService_Login_Nouser()
        {
            //Arrange
            var newuser = new LoginRequest()
            {
                Email = "Phill@Gmail.com",
                Password = "Password",
            };
            var dbContext = await GetDataContextAsync();
            var email = A.Fake<IEmailSender>();
            var Service = new LoginService(dbContext, email);

            //Act
            var result = Service.Login(newuser);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<string>>();
            result.Result.Should().BeEquivalentTo("Login failed");
        }
        [Fact]
        public async void userRequestService_Login_WrongPassword()
        {
            //Arrange
            var newuser = new LoginRequest()
            {
                Email = "Bob@Bob",
                Password = "Password123",
            };
            var dbContext = await GetDataContextAsync();
            var email = A.Fake<IEmailSender>();
            var Service = new LoginService(dbContext, email);

            //Act
            var result = Service.Login(newuser);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<string>>();
            result.Result.Should().BeEquivalentTo("Login failed");
        }
        [Fact]
        public async void userRequestService_ForgotPassword_Success()
        {
            //Arrange
            var newuser = "Gill@Gmail.com";
            var dbContext = await GetDataContextAsync();
            var email = A.Fake<IEmailSender>();
            var Service = new LoginService(dbContext, email);

            //Act
            var result = Service.ForgotPassword(newuser);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<string>>();
        }
        [Fact]
        public async void userRequestService_ResetPassword_Success()
        {
            //Arrange
            var newuser = new ResetPasswordRequest()
            {
                Token = "21774D4464A4C6FA2E027ACE907F9F67E5D4F68E5D30F7332DD08D6F6E2984C581E993204DD7B94B65CF659A77852D75A2DBFF36BC20163BF5FB030034844048",
                Password = "Password.123",
                ConfirmPassword = "Password.123"
            };
            var dbContext = await GetDataContextAsync();
            var email = A.Fake<IEmailSender>();
            var Service = new LoginService(dbContext, email);

            //Act
            var result = Service.ResetPassword(newuser);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<string>>();
        }
        [Fact]
        public async void userRequestService_Verify_Success()
        {
            //Arrange
            var dbContext = await GetDataContextAsync();
            var user = await dbContext.Logins.LastAsync();
            var email = A.Fake<IEmailSender>();
            var Service = new LoginService(dbContext, email);

            //Act
            var result = Service.Verify(user.VerificationToken);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<string>>();
            result.Result.Should().BeEquivalentTo("Varified");
        }
        [Fact]
        public async void userRequestService_Verify_Fail()
        {
            //Arrange
            var dbContext = await GetDataContextAsync();
            var user = "failure";
            var email = A.Fake<IEmailSender>();
            var Service = new LoginService(dbContext, email);

            //Act
            var result = Service.Verify(user);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<string>>();
            result.Result.Should().BeEquivalentTo("Invalid token");
        }


        //encrypting the password with hash and salt and making it so there is not one salt for all the hashes
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}

