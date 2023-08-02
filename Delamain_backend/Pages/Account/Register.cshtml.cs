using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Delamain_backend.Services.LoginService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Delamain_backend.Pages.Account.LoginModel;

namespace Delamain_backend.Pages.Account
{
	public class RegisterModel : PageModel
    {
        private readonly ILoginService _loginService;
        public RegisterModel(ILoginService loginService)
        {
            _loginService = loginService;
        }

        //Set a new instance of the loginrequest model to be used to make a call to the controller
        [BindProperty]
        public LoginReq loginRequest { get; set; }

        public class LoginReq
        {
            [Required, EmailAddress]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            [Required, Compare("Password")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var login = new LoginRegisterRequest
            {
                Email = loginRequest.Email,
                Password = loginRequest.Password,
                ConfirmPassword = loginRequest.ConfirmPassword
            };

            var checker = await _loginService.Register(login);

            if (checker == null)
            {
                return Page();
            }
            if (checker == "Error")
            {
                return RedirectToPage("/Account/Registererror");
            }
            if (checker == "Success")
            {
                return RedirectToPage("/Account/Login");
            }
            else return Page();
        }
    }
}
