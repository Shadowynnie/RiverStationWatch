using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RiverStationWatch.Services;

namespace RiverStationWatch.Pages
{
    public class HangFireModel : PageModel
    {
        public void OnGet()
        {
            Hangfire.BackgroundJob.Enqueue<EmailSender>(x => x.SendEmailAsync("agrooon@seznam.cz", "body", "body"));
        }
    }
}
