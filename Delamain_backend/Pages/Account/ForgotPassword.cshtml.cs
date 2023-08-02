using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Delamain_backend.Services.emailService;
using Delamain_backend.Services.LoginService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Delamain_backend.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly ILoginService _loginService;
        public ForgotPasswordModel(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        [EmailAddress]
        public string email { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync(string email)
        {
            var checker = await _loginService.ForgotPassword(email);
            if (checker == null)
            {
                return Page();
            }
            else if (checker == "User not found.")
            {
                return RedirectToPage("/Account/InvalidUser");
            }
            else if (checker == "You may now reset your password")
            {
                return RedirectToPage("/Account/ResetPassword");
            }
            else return Page();
        }
    }
}
