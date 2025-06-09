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
    public class ParentController : Controller
    {
        private readonly SunriseShelterDbContext _context;

        public ParentController(SunriseShelterDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]

        // GET: Parent
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["LastNameSortParm"] = sortOrder == "lastName_asc" ? "lastName_desc" : "lastName_asc";
            ViewData["CurrentFilter"] = searchString;

            var parents = from p in _context.Parent select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                parents = parents.Where(p => p.FirstName.Contains(searchString) || p.LastName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    parents = parents.OrderByDescending(p => p.FirstName).ThenByDescending(p => p.LastName);  // Sort by FirstName DESC then LastName DESC
                    break;

                case "lastName_asc":
                    parents = parents.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);  // Sort by LastName ASC then FirstName ASC
                    break;

                case "lastName_desc":
                    parents = parents.OrderByDescending(p => p.LastName).ThenByDescending(p => p.FirstName); // Sort by LastName DESC then FirstName DESC
                    break;

                default:
                    parents = parents.OrderBy(p => p.FirstName).ThenBy(p => p.LastName);  // Default: FirstName ASC then LastName ASC
                    break;
            }

            return View(await parents.ToListAsync());
        }

        // GET: Parent/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parent = await _context.Parent
                .FirstOrDefaultAsync(m => m.ParentId == id);
            if (parent == null)
            {
                return NotFound();
            }

            return View(parent);
        }

        // GET: Parent/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParentId,FirstName,LastName,DateOfBirth,Phone,Email,MartialStatus,Address,BirthPlace")] Parent parent)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(parent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parent);
        }

        // GET: Parent/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parent = await _context.Parent.FindAsync(id);
            if (parent == null)
            {
                return NotFound();
            }
            return View(parent);
        }

        // POST: Parent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParentId,FirstName,LastName,DateOfBirth,Phone,Email,MartialStatus,Address,BirthPlace")] Parent parent)
        {
            if (id != parent.ParentId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(parent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParentExists(parent.ParentId))
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
            return View(parent);
        }

        // GET: Parent/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parent = await _context.Parent
                .FirstOrDefaultAsync(m => m.ParentId == id);
            if (parent == null)
            {
                return NotFound();
            }

            return View(parent);
        }

        // POST: Parent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parent = await _context.Parent.FindAsync(id);
            if (parent != null)
            {
                _context.Parent.Remove(parent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParentExists(int id)
        {
            return _context.Parent.Any(e => e.ParentId == id);
        }
    }
}
