using HinduTempleofTriStates.Models;
using Microsoft.AspNetCore.Mvc;
using HinduTempleofTriStates.Services;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedgerController : ControllerBase
    {
        private readonly LedgerService _ledgerService;

        public LedgerController(LedgerService ledgerService)
        {
            _ledgerService = ledgerService;
        }

        // Get all ledger accounts
        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _ledgerService.GetAllAccountsAsync();
            return Ok(accounts);
        }

        // Get a ledger account by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(Guid id) // Changed to Guid
        {
            var account = await _ledgerService.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // Add a new ledger account
        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody] LedgerAccount account)
        {
            if (account == null)
            {
                return BadRequest();
            }

            await _ledgerService.AddAccountAsync(account);
            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
        }

        // Add a new transaction
        [HttpPost("transaction")]
        public async Task<IActionResult> AddTransaction([FromBody] Transaction transaction)
        {
            if (transaction == null)
            {
                return BadRequest();
            }

            await _ledgerService.AddTransactionAsync(transaction);
            return Ok();
        }

        // Update an existing ledger account
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(Guid id, [FromBody] LedgerAccount account) // Changed to Guid
        {
            if (account == null || id != account.Id)
            {
                return BadRequest();
            }

            var existingAccount = await _ledgerService.GetAccountByIdAsync(id);
            if (existingAccount == null)
            {
                return NotFound();
            }

            await _ledgerService.UpdateAccountAsync(account);
            return NoContent();
        }

        // Delete a ledger account by Id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(Guid id) // Changed to Guid
        {
            var account = await _ledgerService.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            await _ledgerService.DeleteAccountAsync(id);
            return NoContent();
        }

        // Get all transactions for a specific ledger account
        [HttpGet("{id}/transactions")]
        public async Task<IActionResult> GetTransactionsByAccountId(Guid id) // Changed to Guid
        {
            var transactions = await _ledgerService.GetTransactionsByAccountIdAsync(id);
            return Ok(transactions);
        }
    }
}
