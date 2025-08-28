using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SunriseShelter.Areas.Identity.Data;
using SunriseShelter.Models;

namespace SunriseShelter.Controllers
{
    public class AdoptionController : Controller
    {
        private readonly SunriseShelterDbContext _context;
        private readonly UserManager<SunriseShelterUser> _userManager;

        public AdoptionController(SunriseShelterDbContext context, UserManager<SunriseShelterUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string sortOrder, string searchString, int? pageNumber, string currentFilter)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;

            // Changed from _context.Parent to _context.Users
            var adoptions = from a in _context.Adoption
                .Include(a => a.Children)
                .Include(a => a.Orphanage)
                .Include(a => a.Parent)
                            select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                adoptions = adoptions.Where(a => a.Children.Name.Contains(searchString) ||
                                               a.Parent.LastName.Contains(searchString) ||
                                               a.Orphanage.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    adoptions = adoptions.OrderByDescending(c => c.Parent.LastName);
                    break;
                case "Date":
                    adoptions = adoptions.OrderBy(c => c.AdoptionDate);
                    break;
                case "date_desc":
                    adoptions = adoptions.OrderByDescending(c => c.AdoptionDate);
                    break;
                default:
                    adoptions = adoptions.OrderBy(c => c.Parent.LastName);
                    break;
            }

            int pageSize = 16;
            return View(await PaginatedList<Adoption>.CreateAsync(adoptions.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var adoption = await _context.Adoption
                .Include(a => a.Children)
                .Include(a => a.Orphanage)
                .Include(a => a.Parent)
                .FirstOrDefaultAsync(m => m.AdoptionId == id);

            if (adoption == null) return NotFound();
            return View(adoption);
        }

        public IActionResult Create()
        {
            // Changed from _context.Parent to _context.Users
            ViewData["ChildrenId"] = new SelectList(_context.Children, "ChildrenId", "Name");
            ViewData["OrphanageId"] = new SelectList(_context.Orphanage, "OrphanageId", "Name");
            ViewData["ParentId"] = new SelectList(_context.Users, "Id", "LastName"); // Changed here
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdoptionId,AdoptionDate,ParentId,ChildrenId,OrphanageId")] Adoption adoption)
        {
            if (ModelState.IsValid) // Changed from !ModelState.IsValid to ModelState.IsValid
            {
                _context.Add(adoption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ChildrenId"] = new SelectList(_context.Children, "ChildrenId", "Name", adoption.ChildrenId);
            ViewData["OrphanageId"] = new SelectList(_context.Orphanage, "OrphanageId", "Name", adoption.OrphanageId);
            ViewData["ParentId"] = new SelectList(_context.Users, "Id", "LastName", adoption.ParentId); // Changed here
            return View(adoption);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var adoption = await _context.Adoption.FindAsync(id);
            if (adoption == null) return NotFound();

            ViewData["ChildrenId"] = new SelectList(_context.Children, "ChildrenId", "Name", adoption.ChildrenId);
            ViewData["OrphanageId"] = new SelectList(_context.Orphanage, "OrphanageId", "Name", adoption.OrphanageId);
            ViewData["ParentId"] = new SelectList(_context.Users, "Id", "LastName", adoption.ParentId); // Changed here
            return View(adoption);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdoptionId,AdoptionDate,ParentId,ChildrenId,OrphanageId")] Adoption adoption)
        {
            if (id != adoption.AdoptionId) return NotFound();

            if (ModelState.IsValid) // Changed from !ModelState.IsValid to ModelState.IsValid
            {
                try
                {
                    _context.Update(adoption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdoptionExists(adoption.AdoptionId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["ChildrenId"] = new SelectList(_context.Children, "ChildrenId", "Name", adoption.ChildrenId);
            ViewData["OrphanageId"] = new SelectList(_context.Orphanage, "OrphanageId", "Name", adoption.OrphanageId);
            ViewData["ParentId"] = new SelectList(_context.Users, "Id", "LastName", adoption.ParentId); // Changed here
            return View(adoption);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var adoption = await _context.Adoption
                .Include(a => a.Children)
                .Include(a => a.Orphanage)
                .Include(a => a.Parent)
                .FirstOrDefaultAsync(m => m.AdoptionId == id);

            if (adoption == null) return NotFound();
            return View(adoption);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adoption = await _context.Adoption.FindAsync(id);
            if (adoption != null) _context.Adoption.Remove(adoption);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdoptionExists(int id)
        {
            return _context.Adoption.Any(e => e.AdoptionId == id);
        }
    }
}