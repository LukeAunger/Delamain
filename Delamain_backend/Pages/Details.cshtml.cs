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
    public class DetailsModel : PageModel
    {
        private readonly Delamain_backend.Data.DataContext _context;

        public DetailsModel(Delamain_backend.Data.DataContext context)
        {
            _context = context;
        }

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
    }
}
