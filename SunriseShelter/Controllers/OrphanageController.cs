using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SunriseShelter.Areas.Identity.Data;
using SunriseShelter.Models;

namespace SunriseShelter.Controllers
{
    public class OrphanageController : Controller
    {
        private readonly SunriseShelterDbContext _context;

        public OrphanageController(SunriseShelterDbContext context)
        {
            _context = context;
        }

        [Authorize] // Doesn't allow people that haven't logged in to open this tab //

        // GET: Orphanage
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orphanage.ToListAsync());
        }

        // GET: Orphanage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orphanage = await _context.Orphanage
                .FirstOrDefaultAsync(m => m.OrphanageId == id);
            if (orphanage == null)
            {
                return NotFound();
            }

            return View(orphanage);
        }

        // GET: Orphanage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orphanage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrphanageId,Name,Address,State,Country")] Orphanage orphanage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orphanage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orphanage);
        }

        // GET: Orphanage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orphanage = await _context.Orphanage.FindAsync(id);
            if (orphanage == null)
            {
                return NotFound();
            }
            return View(orphanage);
        }

        // POST: Orphanage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrphanageId,Name,Address,State,Country")] Orphanage orphanage)
        {
            if (id != orphanage.OrphanageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orphanage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrphanageExists(orphanage.OrphanageId))
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
            return View(orphanage);
        }

        // GET: Orphanage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orphanage = await _context.Orphanage
                .FirstOrDefaultAsync(m => m.OrphanageId == id);
            if (orphanage == null)
            {
                return NotFound();
            }

            return View(orphanage);
        }

        // POST: Orphanage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orphanage = await _context.Orphanage.FindAsync(id);
            if (orphanage != null)
            {
                _context.Orphanage.Remove(orphanage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrphanageExists(int id)
        {
            return _context.Orphanage.Any(e => e.OrphanageId == id);
        }
    }
}
