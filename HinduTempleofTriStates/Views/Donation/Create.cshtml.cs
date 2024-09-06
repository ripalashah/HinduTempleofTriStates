using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Services;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Models
{
    public class CreateModel : PageModel
    {
        private readonly DonationService _donationService;

        public CreateModel(DonationService donationService)
        {
            _donationService = donationService;
        }

        [BindProperty]
        public DonationService Donation { get; set; } = new Donation();

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _donationService.AddDonation(Donation);
            return RedirectToPage("Index");
        }
    }
}
