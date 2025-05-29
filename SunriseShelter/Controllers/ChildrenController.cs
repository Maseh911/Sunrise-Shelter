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
    public class ChildrenController : Controller
    {
        private readonly SunriseShelterDbContext _context;

        public ChildrenController(SunriseShelterDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")] // Doesn't allow people that haven't logged in to open this tab //

        // GET: Children
        public async Task<IActionResult> Index(string searchString) 
        {
            ViewData["CurrentFilter"] = searchString;  

            var childrens = from c in _context.Children select c;

            if (!String.IsNullOrEmpty(searchString))  
            {
                childrens = childrens.Where(d => d.Name.Contains(searchString)); 
            }

            return View(await childrens.ToListAsync());
        }

        // GET: Children/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var children = await _context.Children
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
            return View();
        }

        // POST: Children/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChildrenId,Name,DateOfBirth,BirthPlace,DateOfAdmission")] Children children)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(children);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            return View(children);
        }

        // POST: Children/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChildrenId,Name,DateOfBirth,BirthPlace,DateOfAdmission")] Children children)
        {
            if (id != children.ChildrenId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
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
