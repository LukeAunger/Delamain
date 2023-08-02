using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Delamain_backend.Pages
{
    [Authorize]
    public class AwaitVerify : PageModel
    {
        private readonly Delamain_backend.Data.DataContext _context;

        public AwaitVerify(Delamain_backend.Data.DataContext context)
        {
            _context = context;
        }

        public IList<Login> logins { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Logins != null)
            {
                logins = await _context.Logins.ToListAsync();
            }
        }
    }
}
