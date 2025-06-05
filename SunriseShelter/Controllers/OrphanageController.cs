using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
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

        // GET: Orphanage
        public async Task<IActionResult> Index(string sortOrder, string searchString) // The searchString parameter represents a keyword of a search which will be used for filtering //
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = searchString;       // This will pass the value from the controller to the view to display the filtered value //


            var orphanages = from o in _context.Orphanage select o;

            if (!String.IsNullOrEmpty(searchString))  // If the searchString is not empty then it will exectute the statement //
            {
                orphanages = orphanages.Where(o => o.Name.Contains(searchString) || o.State.Contains(searchString)); // It can filter the orphanage name as well as the state //
            }

            switch (sortOrder)
            {
                case "name_desc":
                    orphanages = orphanages.OrderByDescending(o => o.Name);
                    break;

                default:
                    orphanages = orphanages.OrderBy(o => o.Name);
                    break;
            }
            return View(await orphanages.ToListAsync());
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
            if (!ModelState.IsValid)
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

            if (!ModelState.IsValid)
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
