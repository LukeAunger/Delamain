using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Delamain_backend.Data;
using Delamain_backend.Models;
using Delamain_backend.Services.LoginService;

namespace Delamain_backend.Pages
{
    [Authorize]
    public class VerifyPageModel : PageModel
    {
        private readonly Delamain_backend.Data.DataContext _context;
        private readonly ILoginService _loginService;

        public VerifyPageModel(Delamain_backend.Data.DataContext context, ILoginService login)
        {
            _context = context;
            _loginService = login;
        }

        public Login login { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Logins == null)
            {
                return NotFound();
            }

            var user = await _context.Logins
                .FirstOrDefaultAsync(m => m.Email == id);

            if (user == null)
            {
                return NotFound();
            }
            if (user.VerifieAt != null)
            {
                return RedirectToPage("/Account/UserVerifyError");
            }
            else 
            {
                login = user;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.Logins == null)
            {
                return NotFound();
            }
            var usermodel = await _context.Logins.FindAsync(id);

            if (usermodel != null)
            {
                login = usermodel;
                if(login.VerificationToken != null)
                {
                    await _loginService.Verify(login.VerificationToken);
                }
            }
            return RedirectToPage("/AwaitVerify");
        }
    }
}
