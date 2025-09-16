using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SunriseShelter.Models;
using System.Security.Claims;

namespace SunriseShelter.Controllers
{
    public class AdoptionController : Controller
    {
        private readonly SunriseShelterDbContext _context;

        public AdoptionController(SunriseShelterDbContext context)
        {
            _context = context;
        }

        // GET: Adoption
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(string searchString, string sortOrder, int? pageNumber, string currentFilter)
        {
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["StatusSortParm"] = sortOrder == "Status" ? "status_desc" : "Status";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var adoptions = from a in _context.Adoption
                                .Include(a => a.Parent)
                                .Include(a => a.Children)
                            select a;

            // Search filter
            if (!String.IsNullOrEmpty(searchString))
            {
                adoptions = adoptions.Where(a => a.Parent.FirstName.Contains(searchString) ||
                                               a.Parent.LastName.Contains(searchString) ||
                                               a.Children.Name.Contains(searchString) ||
                                               a.Status.Contains(searchString));
            }

            // Sorting
            switch (sortOrder)
            {
                case "Date":
                    adoptions = adoptions.OrderBy(a => a.ApplicationDate);
                    break;
                case "date_desc":
                    adoptions = adoptions.OrderByDescending(a => a.ApplicationDate);
                    break;
                case "Status":
                    adoptions = adoptions.OrderBy(a => a.Status);
                    break;
                case "status_desc":
                    adoptions = adoptions.OrderByDescending(a => a.Status);
                    break;
                default:
                    adoptions = adoptions.OrderBy(a => a.ApplicationDate);
                    break;
            }

            int pageSize = 16;
            return View(await PaginatedList<Adoption>.CreateAsync(adoptions.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Adoption/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoption
                .Include(a => a.Children)
                .Include(a => a.Parent)
                .FirstOrDefaultAsync(m => m.AdoptionId == id);

            if (adoption == null)
            {
                return NotFound();
            }

            return View(adoption);
        }

        // GET: Adoption/Create - For admins only
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["ChildrenId"] = new SelectList(_context.Children, "ChildrenId", "Name");
            ViewData["ParentId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["StatusList"] = new SelectList(new[] { "Pending", "Approved", "Rejected" });
            return View();
        }

        // POST: Adoption/Create - For admins only
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdoptionId,ApplicationDate,AdoptionDate,Status,ParentMessage,ParentId,ChildrenId")] Adoption adoption)
        {
            if (!ModelState.IsValid)
            {
                // Handle the nullable AdoptionDate
                if (adoption.Status == "Approved" && !adoption.AdoptionDate.HasValue)
                {
                    adoption.AdoptionDate = DateTime.Now;
                }
                else if (adoption.Status != "Approved")
                {
                    adoption.AdoptionDate = null;
                }

                _context.Add(adoption);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Adoption application created successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChildrenId"] = new SelectList(_context.Children, "ChildrenId", "Name", adoption.ChildrenId);
            ViewData["ParentId"] = new SelectList(_context.Users, "Id", "FirstName", adoption.ParentId);
            ViewData["StatusList"] = new SelectList(new[] { "Pending", "Approved", "Rejected" }, adoption.Status);
            return View(adoption);
        }

        // GET: Adoption/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoption.FindAsync(id);
            if (adoption == null)
            {
                return NotFound();
            }

            ViewData["ChildrenId"] = new SelectList(_context.Children, "ChildrenId", "Name", adoption.ChildrenId);
            ViewData["ParentId"] = new SelectList(_context.Users, "Id", "FirstName", adoption.ParentId);
            ViewData["StatusList"] = new SelectList(new[] { "Pending", "Approved", "Rejected" }, adoption.Status);

            return View(adoption);
        }

[HttpPost]
[Authorize(Roles = "Admin")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("AdoptionId,Status,ParentMessage,ParentId,ChildrenId")] Adoption adoption)
{
    if (id != adoption.AdoptionId)
    {
        return NotFound();
    }

    if (!ModelState.IsValid)
    {
        try
        {
            // Get the existing adoption to preserve dates
            var existingAdoption = await _context.Adoption.FindAsync(id);
            if (existingAdoption == null)
            {
                return NotFound();
            }

            // Update only the editable fields
            existingAdoption.Status = adoption.Status;
            existingAdoption.ParentId = adoption.ParentId;
            existingAdoption.ChildrenId = adoption.ChildrenId;

            // Handle status changes and date logic
            if (adoption.Status == "Approved" && existingAdoption.Status != "Approved")
            {
                existingAdoption.AdoptionDate = DateTime.Now;
                // Update child status
                var child = await _context.Children.FindAsync(adoption.ChildrenId);
                if (child != null)
                {
                    child.Status = "In Process";
                    _context.Update(child);
                }
            }
            else if (existingAdoption.Status == "Approved" && adoption.Status != "Approved")
            {
                existingAdoption.AdoptionDate = null;
                // Revert child status if needed
                var child = await _context.Children.FindAsync(adoption.ChildrenId);
                if (child != null && child.Status == "In Process")
                {
                    child.Status = "Available";
                    _context.Update(child);
                }
            }

            _context.Update(existingAdoption);
            await _context.SaveChangesAsync();
            
            TempData["SuccessMessage"] = "Adoption application updated successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AdoptionExists(adoption.AdoptionId))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
    }

    // Reload ViewData if validation fails
    ViewData["ChildrenId"] = new SelectList(_context.Children, "ChildrenId", "Name", adoption.ChildrenId);
    ViewData["ParentId"] = new SelectList(_context.Users, "Id", "FirstName", adoption.ParentId);
    ViewData["StatusList"] = new SelectList(new[] { "Pending", "Approved", "Rejected" }, adoption.Status);
    
    return View(adoption);
}

        // GET: Adoption/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoption
                .Include(a => a.Children)
                .Include(a => a.Parent)
                .FirstOrDefaultAsync(m => m.AdoptionId == id);

            if (adoption == null)
            {
                return NotFound();
            }

            return View(adoption);
        }

        // POST: Adoption/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adoption = await _context.Adoption
                .Include(a => a.Children)
                .FirstOrDefaultAsync(a => a.AdoptionId == id);

            if (adoption != null)
            {
                // If the adoption was approved, revert child status to Available
                if (adoption.Status == "Approved")
                {
                    var child = await _context.Children.FindAsync(adoption.ChildrenId);
                    if (child != null && child.Status == "In Process")
                    {
                        child.Status = "Available";
                        _context.Update(child);
                    }
                }

                _context.Adoption.Remove(adoption);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Adoption application deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Adoption/UserCreate - For parents to create adoption request
        [Authorize]
        public async Task<IActionResult> UserCreate(int? childrenId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (childrenId.HasValue)
            {
                // Pre-fill with specific child
                var child = await _context.Children
                    .FirstOrDefaultAsync(c => c.ChildrenId == childrenId && c.Status == "Available");

                if (child == null)
                {
                    TempData["ErrorMessage"] = "Child not found or not available for adoption.";
                    return RedirectToAction("Index", "Children");
                }

                ViewBag.SelectedChild = child;
                ViewBag.AvailableChildren = new SelectList(new List<Children> { child }, "ChildrenId", "Name", child.ChildrenId);
            }
            else
            {
                // Show all available children
                var availableChildren = await _context.Children
                    .Where(c => c.Status == "Available")
                    .ToListAsync();

                ViewBag.AvailableChildren = new SelectList(availableChildren, "ChildrenId", "Name");
            }

            return View();
        }

        // POST: Adoption/UserCreate
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCreate([Bind("ChildrenId,ParentMessage")] Adoption adoption)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                // Verify child is still available
                var child = await _context.Children
                    .FirstOrDefaultAsync(c => c.ChildrenId == adoption.ChildrenId && c.Status == "Available");

                if (child == null)
                {
                    ModelState.AddModelError("", "This child is no longer available for adoption.");
                    var availableChildren = await _context.Children
                        .Where(c => c.Status == "Available")
                        .ToListAsync();
                    ViewBag.AvailableChildren = new SelectList(availableChildren, "ChildrenId", "Name");
                    return View(adoption);
                }

                // Auto-fill the remaining fields
                adoption.ParentId = userId;
                adoption.ApplicationDate = DateTime.Now;
                adoption.Status = "Pending";

                _context.Add(adoption);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Adoption application submitted successfully! Our team will review your request shortly.";
                return RedirectToAction("Index", "Children");
            }

            // Reload available children if validation fails
            var availableChildrenList = await _context.Children
                .Where(c => c.Status == "Available")
                .ToListAsync();
            ViewBag.AvailableChildren = new SelectList(availableChildrenList, "ChildrenId", "Name", adoption.ChildrenId);

            return View(adoption);
        }

        // GET: Adoption/Review/5 (Admin review)
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Review(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adoption = await _context.Adoption
                .Include(a => a.Children)
                .Include(a => a.Parent)
                .FirstOrDefaultAsync(m => m.AdoptionId == id);

            if (adoption == null)
            {
                return NotFound();
            }

            return View(adoption);
        }

        // POST: Adoption/Approve/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            var adoption = await _context.Adoption
                .Include(a => a.Children)
                .FirstOrDefaultAsync(a => a.AdoptionId == id);

            if (adoption == null)
            {
                return NotFound();
            }

            adoption.Status = "Approved";
            adoption.AdoptionDate = DateTime.Now;

            // Update child status
            adoption.Children.Status = "In Process";

            _context.Update(adoption);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Adoption application approved successfully!";
            return RedirectToAction(nameof(Index));
        }

        // POST: Adoption/Reject/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id)
        {
            var adoption = await _context.Adoption
                .Include(a => a.Children)
                .FirstOrDefaultAsync(a => a.AdoptionId == id);

            if (adoption == null)
            {
                return NotFound();
            }

            adoption.Status = "Rejected";

            _context.Update(adoption);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Adoption application rejected.";
            return RedirectToAction(nameof(Index));
        }

        private bool AdoptionExists(int id)
        {
            return _context.Adoption.Any(e => e.AdoptionId == id);
        }
    }
}