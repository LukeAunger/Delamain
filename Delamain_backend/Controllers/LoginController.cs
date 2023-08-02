using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Delamain_backend.Models;
using Delamain_backend.Services.LoginService;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Delamain_backend.Controllers
{
    //this name setup would mean the url for this controller would be api/Login
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginRegisterRequest request)
        {
            var checker = await _loginService.Register(request);
            if (checker != "Success")
            {
                return Ok("Success");
            }
            else return BadRequest("Error");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var checker = await _loginService.Login(request);
            if (checker != null)
            {
                return Ok(checker);
            }
            else return BadRequest();
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify(string token)
        {
            var checker = await _loginService.Verify(token);
            if (checker != null)
            {
                return Ok(checker);
            }
            else return BadRequest();
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var checker = await _loginService.ForgotPassword(email);
            if (checker != null)
            {
                return Ok(checker);
            }
            else return BadRequest();
        }

        [HttpPost("password-reset")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var checker = await _loginService.ResetPassword(request);
            if (checker != null)
            {
                return Ok(checker);
            }
            else return BadRequest();
        }
    }
}

