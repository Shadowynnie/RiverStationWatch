using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RiverStationWatch.Pages.Config
{
    [Authorize]
    public class ConfigurationModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            // Check if the user is authenticated
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the login page if the user is not authenticated
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            return Page();
        }
    }
}
