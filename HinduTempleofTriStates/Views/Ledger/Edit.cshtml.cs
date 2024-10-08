﻿using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Views.Ledger
{
    public class EditModel : PageModel
    {
        private readonly LedgerService _ledgerService;

        public EditModel(LedgerService ledgerService)
        {
            _ledgerService = ledgerService;

        }

        [BindProperty]
        public LedgerAccount LedgerAccount { get; set; }

        public LedgerService Get_ledgerService()
        {
            return _ledgerService;
        }

        // Fetch the existing LedgerAccount data when the page is loaded
        public async Task<IActionResult> OnGetAsync(Guid id, LedgerService _ledgerService)
        {
            LedgerAccount = await _ledgerService.GetLedgerAccountByIdAsync(id);

            if (LedgerAccount == null)
            {
                return NotFound();
            }

            return Page();
        }

        // Save the changes when the form is submitted
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) // Server-side validation
            {
                return Page(); // Redisplay form with validation messages
            }

            await _ledgerService.UpdateLedgerAccountAsync(LedgerAccount); // Update the account in the database
            return RedirectToPage("Index");
        }
    }
}
