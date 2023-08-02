using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Azure.Core;
using Delamain_backend.Models;
using Delamain_backend.Services.LoginService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Delamain_backend.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ILoginService _loginService;

        public LoginModel(ILoginService loginService)
        {
            _loginService = loginService;
        }

        //Set a new instance of the loginrequest model to be used to make a call to the controller
        [BindProperty]
        public LoginReq loginRequest { get; set; }

        public void OnGet()
        {
        }

        public class LoginReq
        {
            [Required, EmailAddress]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var login = new LoginRequest
            {
                Email = loginRequest.Email,
                Password = loginRequest.Password
            };

            var checker = await _loginService.Login(login);

            if (checker == null)
            {
                return Page();
            }
            if (checker == "Not verified")
            {
                return Page();
            }
            if (checker == "Login failed")
            {
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginRequest.Email)
            };
            var identity = new ClaimsIdentity(claims, "CookieAuth");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", claimsPrincipal);
            return RedirectToPage("/Index");
        }
    }
}
