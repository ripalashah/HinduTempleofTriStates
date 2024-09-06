using Microsoft.AspNetCore.Mvc.RazorPages;
using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Models
{
    public class IndexModel : PageModel
    {
        private readonly DonationService _donationService;

        public IndexModel(DonationService donationService)
        {
            _donationService = donationService;
        }

        public List<Donation> Donations { get; set; } = new List<Donation>();

        public async Task OnGetAsync()
        {
            Donations = await _donationService.GetDonationsAsync();
        }
    }
}
