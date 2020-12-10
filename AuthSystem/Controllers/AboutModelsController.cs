using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Models;
using AuthSystem.Data;
using Microsoft.AspNetCore.Authorization;

namespace AuthSystem.Controllers
{
    public class AboutModelsController : Controller
    {
        private readonly AuthSystemContext _context;

        public AboutModelsController(AuthSystemContext context)
        {
            _context = context;
        }

        // GET: AboutModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.About.ToListAsync());
        }

        [Authorize(Policy = "komandorpolicy")]
        public async Task<IActionResult> IndexAdmin()
        {
            return View(await _context.About.ToListAsync());
        }

        // GET: AboutModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutModel = await _context.About
                .FirstOrDefaultAsync(m => m.AboutId == id);
            if (aboutModel == null)
            {
                return NotFound();
            }

            return View(aboutModel);
        }

        // GET: AboutModels/Create
        [Authorize(Policy = "komandorpolicy")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: AboutModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "komandorpolicy")]
        public async Task<IActionResult> Create([Bind("AboutId,Title,AboutContent")] AboutModel aboutModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aboutModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aboutModel);
        }

        // GET: AboutModels/Edit/5
        [Authorize(Policy = "komandorpolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutModel = await _context.About.FindAsync(id);
            if (aboutModel == null)
            {
                return NotFound();
            }
            return View(aboutModel);
        }

        // POST: AboutModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "komandorpolicy")]
        public async Task<IActionResult> Edit(int id, [Bind("AboutId,Title,AboutContent")] AboutModel aboutModel)
        {
            if (id != aboutModel.AboutId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aboutModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutModelExists(aboutModel.AboutId))
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
            return View(aboutModel);
        }

        // GET: AboutModels/Delete/5
        [Authorize(Policy = "komandorpolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutModel = await _context.About
                .FirstOrDefaultAsync(m => m.AboutId == id);
            if (aboutModel == null)
            {
                return NotFound();
            }

            return View(aboutModel);
        }

        // POST: AboutModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "komandorpolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aboutModel = await _context.About.FindAsync(id);
            _context.About.Remove(aboutModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutModelExists(int id)
        {
            return _context.About.Any(e => e.AboutId == id);
        }
    }
}
