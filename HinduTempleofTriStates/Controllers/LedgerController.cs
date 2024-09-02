using HinduTempleofTriStates.Models;
using Microsoft.AspNetCore.Mvc;
using TempleManagementSystem.Models;
using TempleManagementSystem.Services;

namespace TempleManagementSystem.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _ledgerService.GetAllAccountsAsync();
            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            var account = await _ledgerService.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

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
    }
}
