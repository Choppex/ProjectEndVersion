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
    [Authorize(Policy = "skarbnikpolicy")]
    public class NotesController : Controller
    {
        private readonly AuthSystemContext _context;

        public NotesController(AuthSystemContext context)
        {
            _context = context;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Notepad.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoteId,FirstName,LastName,Date,Note")] Notes notes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notes);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Notepad
                .FirstOrDefaultAsync(m => m.NoteId == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var note = await _context.Notepad.FindAsync(id);
            _context.Notepad.Remove(note);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteExsist(int id)
        {
            return _context.Notepad.Any(e => e.NoteId == id);
        }
    }
}
