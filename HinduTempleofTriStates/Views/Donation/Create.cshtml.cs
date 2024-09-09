using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Views.Donation
{
    public class CreateModel : PageModel
    {
        private readonly IDonationService _donationService;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(IDonationService donationService, ILogger<CreateModel> logger)
        {
            _donationService = donationService;
            _logger = logger;
        }

        [BindProperty]
        public Models.Donation? Donation { get; set; }  // Nullable Donation model for form binding

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Donation == null)
            {
                _logger.LogError("Donation model is null.");
                ModelState.AddModelError(string.Empty, "An error occurred while creating the donation.");
                return Page();
            }

            Donation.Id = Guid.NewGuid();  // Assign a new GUID to the donation

            var result = await _donationService.AddDonationAsync(Donation);  // Call the service to add donation

            if (result)
            {
                return RedirectToPage("Confirmation", new { id = Donation.Id });
            }

            _logger.LogError("Error adding donation.");
            ModelState.AddModelError(string.Empty, "An error occurred while creating the donation.");
            return Page();
        }
    }
}
