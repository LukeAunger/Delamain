using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Delamain_backend.Data;
using Delamain_backend.Models;

namespace Delamain_backend.Pages
{
    [Authorize]
    public class DeleteUserModel : PageModel
    {
        private readonly Delamain_backend.Data.DataContext _context;

        public DeleteUserModel(Delamain_backend.Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Login login { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.Logins == null)
            {
                return NotFound();
            }

            var usermodel = await _context.Logins
                .FirstOrDefaultAsync(m => m.Email == id);

            if (usermodel == null)
            {
                return NotFound();
            }
            else 
            {
                login = usermodel;
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
                _context.Logins.Remove(login);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/AwaitVerify");
        }
    }
}
