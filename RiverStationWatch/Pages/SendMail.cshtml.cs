using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RiverStationWatch.Pages
{
    public class SendMailModel : PageModel
    {
        private readonly IEmailSender _emailSender;

        public SendMailModel(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public void OnGet()
        {
            _emailSender.SendEmailAsync("agrooon@seznam.cz", "subject", "htmlmessage");
        }

        public IActionResult OnPostSendEmail() 
        {
            return Page();
        }
    }
}
