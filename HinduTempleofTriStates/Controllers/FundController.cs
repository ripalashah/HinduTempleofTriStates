using HinduTempleofTriStates.Models;
using HinduTempleofTriStates.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HinduTempleofTriStates.Controllers
{
    [Route("funds")]
    public class FundController : Controller
    {
        private readonly FundService _fundService;

        public FundController(FundService fundService)
        {
            _fundService = fundService;
        }

        // GET: Fund
        [HttpGet]
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var funds = await _fundService.GetAllFundsAsync();
            return View(funds);
        }

        // GET: Fund/Create
        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fund/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create([Bind("AccountName,FundName,Balance,Amount,Description")] Fund fund)
        {
            if (ModelState.IsValid)
            {
                fund.Id = Guid.NewGuid();
                await _fundService.AddFundAsync(fund);
                return RedirectToAction(nameof(Index));
            }
            return View(fund);
        }

        // GET: Fund/Edit/5
        [HttpGet]
        [Route("Edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var fund = await _fundService.GetFundByIdAsync(id);
            if (fund == null)
            {
                return NotFound();
            }
            return View(fund);
        }

        // POST: Fund/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,AccountName,FundName,Balance,Amount,Description")] Fund fund)
        {
            if (id != fund.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _fundService.UpdateFundAsync(fund);
                return RedirectToAction(nameof(Index));
            }
            return View(fund);
        }

        // GET: Fund/Delete/5
        [HttpGet]
        [Route("Delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var fund = await _fundService.GetFundByIdAsync(id);
            if (fund == null)
            {
                return NotFound();
            }
            return View(fund);
        }

        // POST: Fund/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("DeleteConfirmed/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _fundService.DeleteFundAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
