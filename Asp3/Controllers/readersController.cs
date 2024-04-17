using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asp3.Data;
using Asp3.Models;

namespace Asp3.Controllers
{
    public class readersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public readersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: readers
        public async Task<IActionResult> Index()
        {
              return _context.reader != null ? 
                          View(await _context.reader.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.reader'  is null.");
        }

        // GET: readers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.reader == null)
            {
                return NotFound();
            }

            var reader = await _context.reader
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        // GET: readers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: readers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,PhoneNumber")] reader reader)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reader);
        }

        // GET: readers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.reader == null)
            {
                return NotFound();
            }

            var reader = await _context.reader.FindAsync(id);
            if (reader == null)
            {
                return NotFound();
            }
            return View(reader);
        }

        // POST: readers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,PhoneNumber")] reader reader)
        {
            if (id != reader.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!readerExists(reader.Id))
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
            return View(reader);
        }

        // GET: readers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.reader == null)
            {
                return NotFound();
            }

            var reader = await _context.reader
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        // POST: readers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.reader == null)
            {
                return Problem("Entity set 'ApplicationDbContext.reader'  is null.");
            }
            var reader = await _context.reader.FindAsync(id);
            if (reader != null)
            {
                _context.reader.Remove(reader);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool readerExists(int id)
        {
          return (_context.reader?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
