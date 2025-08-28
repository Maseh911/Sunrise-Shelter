using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SunriseShelter.Models;

namespace SunriseShelter.Controllers
{
    public class ChildrenController : Controller
    {
        private readonly SunriseShelterDbContext _context;

        public ChildrenController(SunriseShelterDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")] // Doesn't allow people that haven't logged in to open this tab //

        // GET: Children
        public async Task<IActionResult> Index(string searchString, string sortOrder, string statusFilter, string genderFilter, int? pageNumber, string currentFilter)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["StatusSortParm"] = sortOrder == "Status" ? "status_desc" : "Status";
            ViewData["GenderSortParm"] = sortOrder == "Gender" ? "gender_desc" : "Gender";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentStatusFilter"] = statusFilter;
            ViewData["CurrentGenderFilter"] = genderFilter;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var childrens = from c in _context.Children.Include(c => c.Orphanage)
                            select c;

            // Search filter
            if (!String.IsNullOrEmpty(searchString))
            {
                childrens = childrens.Where(c => c.Name.Contains(searchString) ||
                                                c.Orphanage.Name.Contains(searchString));
            }

            // Status filter
            if (!String.IsNullOrEmpty(statusFilter))
            {
                childrens = childrens.Where(c => c.Status == statusFilter);
            }

            // Gender filter
            if (!String.IsNullOrEmpty(genderFilter))
            {
                childrens = childrens.Where(c => c.Gender == genderFilter);
            }

            // Sorting
            switch (sortOrder)
            {
                case "name_desc":
                    childrens = childrens.OrderByDescending(c => c.Name);
                    break;
                case "Date":
                    childrens = childrens.OrderBy(c => c.DateOfAdmission);
                    break;
                case "date_desc":
                    childrens = childrens.OrderByDescending(c => c.DateOfAdmission);
                    break;
                case "Status":
                    childrens = childrens.OrderBy(c => c.Status);
                    break;
                case "status_desc":
                    childrens = childrens.OrderByDescending(c => c.Status);
                    break;
                case "Gender":
                    childrens = childrens.OrderBy(c => c.Gender);
                    break;
                case "gender_desc":
                    childrens = childrens.OrderByDescending(c => c.Gender);
                    break;
                default:
                    childrens = childrens.OrderBy(c => c.Name);
                    break;
            }

            // Get distinct values for filter dropdowns
            ViewBag.StatusList = new SelectList(await _context.Children.Select(c => c.Status).Distinct().ToListAsync());
            ViewBag.GenderList = new SelectList(await _context.Children.Select(c => c.Gender).Distinct().ToListAsync());

            int pageSize = 16;
            return View(await PaginatedList<Children>.CreateAsync(childrens.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Children/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var children = await _context.Children
                .Include(c => c.Orphanage)
                .FirstOrDefaultAsync(m => m.ChildrenId == id);
            if (children == null)
            {
                return NotFound();
            }

            return View(children);
        }

        // GET: Children/Create
        public IActionResult Create()
        {
            ViewData["OrphanageId"] = new SelectList(_context.Orphanage, "OrphanageId", "Name");
            ViewData["StatusList"] = new SelectList(new[] { "Available", "In Process", "Adopted" });
            ViewData["GenderList"] = new SelectList(new[] { "Male", "Female", "Other" });
            return View();
        }

        // POST: Children/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChildrenId,Name,Gender,DateOfBirth,BirthPlace,DateOfAdmission,Status,OrphanageId")] Children children)
        {
            if (ModelState.IsValid)
            {
                _context.Add(children);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrphanageId"] = new SelectList(_context.Orphanage, "OrphanageId", "Name", children.OrphanageId);
            ViewData["StatusList"] = new SelectList(new[] { "Available", "In Process", "Adopted" });
            ViewData["GenderList"] = new SelectList(new[] { "Male", "Female", "Other" });
            return View(children);
        }

        // GET: Children/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var children = await _context.Children.FindAsync(id);
            if (children == null)
            {
                return NotFound();
            }
            ViewData["OrphanageId"] = new SelectList(_context.Orphanage, "OrphanageId", "Name", children.OrphanageId);
            ViewData["StatusList"] = new SelectList(new[] { "Available", "In Process", "Adopted" }, children.Status);
            ViewData["GenderList"] = new SelectList(new[] { "Male", "Female", "Other" }, children.Gender);
            return View(children);
        }

        // POST: Children/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChildrenId,Name,Gender,DateOfBirth,BirthPlace,DateOfAdmission,Status,OrphanageId")] Children children)
        {
            if (id != children.ChildrenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(children);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChildrenExists(children.ChildrenId))
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
            ViewData["OrphanageId"] = new SelectList(_context.Orphanage, "OrphanageId", "Name", children.OrphanageId);
            ViewData["StatusList"] = new SelectList(new[] { "Available", "In Process", "Adopted" }, children.Status);
            ViewData["GenderList"] = new SelectList(new[] { "Male", "Female", "Other" }, children.Gender);
            return View(children);
        }

        // GET: Children/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var children = await _context.Children
                .Include(c => c.Orphanage)
                .FirstOrDefaultAsync(m => m.ChildrenId == id);
            if (children == null)
            {
                return NotFound();
            }

            return View(children);
        }

        // POST: Children/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var children = await _context.Children.FindAsync(id);
            if (children != null)
            {
                _context.Children.Remove(children);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChildrenExists(int id)
        {
            return _context.Children.Any(e => e.ChildrenId == id);
        }
    }
}