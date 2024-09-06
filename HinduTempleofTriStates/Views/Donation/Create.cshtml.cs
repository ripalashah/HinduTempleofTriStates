using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Services;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Models.Donation
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _donationService.AddDonationAsync(Donation);
            return RedirectToPage("Index");
        }
    }
}
