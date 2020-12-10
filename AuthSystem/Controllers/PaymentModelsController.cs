using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Data;
using AuthSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace AuthSystem.Controllers
{
    [Authorize(Policy = "standardpolicy")]
    public class PaymentModelsController : Controller
    {
        private readonly AuthSystemContext _context;

        public PaymentModelsController(AuthSystemContext context)
        {
            _context = context;
        }

        // GET: PaymentModels
        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Payment.ToListAsync());
        }

        //[Authorize(Policy = "adminpolicy")]
        public async Task<IActionResult> IndexAdmin()
        {
            return View(await _context.Payment.ToListAsync());
        }

        // GET: PaymentModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentModel = await _context.Payment
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (paymentModel == null)
            {
                return NotFound();
            }

            return View(paymentModel);
        }

        // GET: PaymentModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,Title,Date,Content,Number")] PaymentModel paymentModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentModel);
        }

        // GET: PaymentModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentModel = await _context.Payment.FindAsync(id);
            if (paymentModel == null)
            {
                return NotFound();
            }
            return View(paymentModel);
        }

        // POST: PaymentModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,Title,Date,Content,Number")] PaymentModel paymentModel)
        {
            if (id != paymentModel.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentModelExists(paymentModel.PaymentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(paymentModel);
        }

        // GET: PaymentModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentModel = await _context.Payment
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (paymentModel == null)
            {
                return NotFound();
            }

            return View(paymentModel);
        }

        // POST: PaymentModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentModel = await _context.Payment.FindAsync(id);
            _context.Payment.Remove(paymentModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentModelExists(int id)
        {
            return _context.Payment.Any(e => e.PaymentId == id);
        }
    }
}
