using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Delamain_backend.Pages.Account
{
	public class LoginFailedModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> tryAgain()
        {
            return RedirectToPage("/Index");
        }
    }
}
