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
    public class borrowingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public borrowingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: borrowings
        public async Task<IActionResult> Index(string searchString)
        {
            var borrowings = _context.borrowing.AsQueryable(); // Start with all borrowings

            if (!String.IsNullOrEmpty(searchString))
            {
                // Retrieve the names of readers whose IDs match the ReaderId in borrowings
                var readerNames = await _context.Readers
                    .Where(r => borrowings.Select(b => b.ReaderId).Contains(r.Id))
                    .Where(r => r.Name.Contains(searchString))
                    .Select(r => r.Name)
                    .ToListAsync();

                // Filter borrowings based on the names of readers found
                borrowings = borrowings.Where(b => readerNames.Contains(b.ReaderId.ToString()));
            }

            return View(await borrowings.ToListAsync());
        }
        // GET: borrowings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.borrowing == null)
            {
                return NotFound();
            }

            var borrowing = await _context.borrowing
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowing == null)
            {
                return NotFound();
            }

            return View(borrowing);
        }

        // GET: borrowings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: borrowings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookId,ReaderId,BorrowDate,ReturnDate")] borrowing borrowing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrowing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(borrowing);
        }

        // GET: borrowings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.borrowing == null)
            {
                return NotFound();
            }

            var borrowing = await _context.borrowing.FindAsync(id);
            if (borrowing == null)
            {
                return NotFound();
            }
            return View(borrowing);
        }

        // POST: borrowings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookId,ReaderId,BorrowDate,ReturnDate")] borrowing borrowing)
        {
            if (id != borrowing.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!borrowingExists(borrowing.Id))
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
            return View(borrowing);
        }

        // GET: borrowings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.borrowing == null)
            {
                return NotFound();
            }

            var borrowing = await _context.borrowing
                .FirstOrDefaultAsync(m => m.Id == id);
            if (borrowing == null)
            {
                return NotFound();
            }

            return View(borrowing);
        }

        // POST: borrowings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.borrowing == null)
            {
                return Problem("Entity set 'ApplicationDbContext.borrowing'  is null.");
            }
            var borrowing = await _context.borrowing.FindAsync(id);
            if (borrowing != null)
            {
                _context.borrowing.Remove(borrowing);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool borrowingExists(int id)
        {
          return (_context.borrowing?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
