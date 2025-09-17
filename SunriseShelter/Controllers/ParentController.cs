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
    [Authorize(Roles = "Admin")]
    public class ParentController : Controller
    {
        private readonly SunriseShelterDbContext _context;
        private readonly UserManager<SunriseShelterUser> _userManager;

        public ParentController(SunriseShelterDbContext context, UserManager<SunriseShelterUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Parent
        public async Task<IActionResult> Index(string sortOrder, string searchString, int? pageNumber, string currentFilter)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["LastNameSortParm"] = sortOrder == "lastName_asc" ? "lastName_desc" : "lastName_asc";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            // Get users in the "Parent" role
            var parentUsers = _userManager.Users;

            if (!String.IsNullOrEmpty(searchString))
            {
                parentUsers = parentUsers.Where(u => u.FirstName.Contains(searchString) || u.LastName.Contains(searchString));
            }

            IQueryable<SunriseShelterUser> sortedUsers;
            switch (sortOrder)
            {
                case "name_desc":
                    sortedUsers = parentUsers.OrderByDescending(u => u.FirstName).ThenByDescending(u => u.LastName);
                    break;

                case "lastName_asc":
                    sortedUsers = parentUsers.OrderBy(u => u.LastName).ThenBy(u => u.FirstName);
                    break;

                case "lastName_desc":
                    sortedUsers = parentUsers.OrderByDescending(u => u.LastName).ThenByDescending(u => u.FirstName);
                    break;

                default:
                    sortedUsers = parentUsers.OrderBy(u => u.FirstName).ThenBy(u => u.LastName);
                    break;
            }

            int pageSize = 16;
            return View(await PaginatedList<SunriseShelterUser>.CreateAsync(sortedUsers.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Parent/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parent = await _userManager.FindByIdAsync(id);
            if (parent == null)
            {
                return NotFound();
            }

            return View(parent);
        }

        // REMOVED: Create actions - Users should register through authentication system

        // GET: Parent/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parent = await _userManager.FindByIdAsync(id);
            if (parent == null)
            {
                return NotFound();
            }
            return View(parent);
        }

        // POST: Parent/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FirstName,LastName,DateOfBirth,PhoneNumber,Email,MaritalStatus,Address,BirthPlace")] SunriseShelterUser parent)
        {
            if (id != parent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = await _userManager.FindByIdAsync(id);
                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    existingUser.FirstName = parent.FirstName;
                    existingUser.LastName = parent.LastName;
                    existingUser.DateOfBirth = parent.DateOfBirth;
                    existingUser.PhoneNumber = parent.PhoneNumber;
                    existingUser.Email = parent.Email;
                    existingUser.UserName = parent.Email;
                    existingUser.MaritalStatus = parent.MaritalStatus;
                    existingUser.Address = parent.Address;
                    existingUser.BirthPlace = parent.BirthPlace;

                    var result = await _userManager.UpdateAsync(existingUser);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ParentExists(parent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(parent);
        }

        // GET: Parent/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parent = await _userManager.FindByIdAsync(id);
            if (parent == null)
            {
                return NotFound();
            }

            return View(parent);
        }

        // POST: Parent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var parent = await _userManager.FindByIdAsync(id);
            if (parent != null)
            {
                var result = await _userManager.DeleteAsync(parent);
                if (!result.Succeeded)
                {
                    // Handle errors
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(parent);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ParentExists(string id)
        {
            return await _userManager.FindByIdAsync(id) != null;
        }
    }
}
