using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Delamain_backend.Models;
using Delamain_backend.Services.LoginService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol.Plugins;

namespace Delamain_backend.Pages.Account
{
	public class ResetPasswordModel : PageModel
    {

        private readonly ILoginService _loginService;

        public ResetPasswordModel(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [BindProperty]
        public ResetPasswordRequest reset { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var checker = await _loginService.ResetPassword(reset);
            if (checker == null)
            {
                return Page();
            }
            else if (checker == "Invalid token.")
            {
                return RedirectToPage("/Account/IncorrectToken");
            }
            else if(checker == "Password changed! ")
            {
                return RedirectToPage("/Account/PasswordChanged");
            }
            return Page();
        }
    }
}
