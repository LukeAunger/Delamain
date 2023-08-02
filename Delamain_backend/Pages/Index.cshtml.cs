using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Delamain_backend.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Delamain_backend.Data.DataContext _context;

        public IndexModel(Delamain_backend.Data.DataContext context)
        {
            _context = context;
        }

        public IList<Queuemodel> Queuemodel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.queuemodels != null)
            {
                Queuemodel = await _context.queuemodels.ToListAsync();
            }
        }
    }
}
