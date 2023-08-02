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
    public class DeleteModel : PageModel
    {
        private readonly Delamain_backend.Data.DataContext _context;

        public DeleteModel(Delamain_backend.Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Queuemodel Queuemodel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.queuemodels == null)
            {
                return NotFound();
            }

            var queuemodel = await _context.queuemodels
                .Include(q=>q.userdetail)
                .FirstOrDefaultAsync(m => m.queueID == id);

            if (queuemodel == null)
            {
                return NotFound();
            }
            else 
            {
                Queuemodel = queuemodel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.queuemodels == null)
            {
                return NotFound();
            }
            var queuemodel = await _context.queuemodels.FindAsync(id);
            var userdetail = await _context.userdetails.FirstOrDefaultAsync(m=>m.queueId == id);

            if (queuemodel != null && userdetail != null)
            {
                Queuemodel = queuemodel;
                var Userdetail = userdetail;
                _context.queuemodels.Remove(Queuemodel);
                _context.userdetails.Remove(Userdetail);
                foreach (var item in await _context.queuemodels.ToListAsync())
                {
                    if(item.queueordernum > Queuemodel.queueordernum)
                    {
                        var newplace = item.queueordernum - 1;
                        item.queueordernum = newplace;
                    }
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
