using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AuthSystem.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

using AuthSystem.Data;
using Microsoft.AspNetCore.Authorization;

namespace AuthSystem.Controllers
{
    public class ImageController : Controller
    {
        private readonly AuthSystemContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ImageController(AuthSystemContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Image
        public async Task<IActionResult> Index()
        {
            return View(await _context.Images.ToListAsync());
        }
        [Authorize(Policy = "komandorpolicy")]
        public async Task<IActionResult> IndexAdmin()
        {
            return View(await _context.Images.ToListAsync());
        }

        // GET: Image/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageModel = await _context.Images
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (imageModel == null)
            {
                return NotFound();
            }

            return View(imageModel);
        }

        // GET: Image/Create
        [Authorize(Policy = "komandorpolicy")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Image/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "komandorpolicy")]
        public async Task<IActionResult> Create([Bind("ImageId,Title,ImageFile")] ImageModel imageModel)
        {
            if (ModelState.IsValid)
            {
                //Save image to wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
                string extension = Path.GetExtension(imageModel.ImageFile.FileName);
                imageModel.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await imageModel.ImageFile.CopyToAsync(fileStream);
                }
                //Insert record
                _context.Add(imageModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexAdmin));
            }
            return View(imageModel);
        }

        // GET: Image/Edit/5
        [Authorize(Policy = "komandorpolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageModel = await _context.Images.FindAsync(id);
            if (imageModel == null)
            {
                return NotFound();
            }
            return View(imageModel);
        }

        // POST: Image/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "komandorpolicy")]

        public async Task<IActionResult> Edit(int id, [Bind("ImageId,Title,ImageName")] ImageModel imageModel)
        {
            if (id != imageModel.ImageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imageModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageModelExists(imageModel.ImageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexAdmin));
            }
            return View(imageModel);
        }

        // GET: Image/Delete/5
        [Authorize(Policy = "komandorpolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageModel = await _context.Images
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (imageModel == null)
            {
                return NotFound();
            }

            return View(imageModel);
        }

        // POST: Image/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "komandorpolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imageModel = await _context.Images.FindAsync(id);

            //delete image from wwwroot/image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", imageModel.ImageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            //delete the record
            _context.Images.Remove(imageModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexAdmin));
        }

        private bool ImageModelExists(int id)
        {
            return _context.Images.Any(e => e.ImageId == id);
        }
    }
}
