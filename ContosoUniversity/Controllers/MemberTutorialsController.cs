using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Authorization;

namespace ContosoUniversity.Controllers
{
    [AllowAnonymous]
    [Authorize(Roles = "Member")]
    public class MemberTutorialsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MemberTutorialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MemberTutorials
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tutorials.ToListAsync());
        }

        // GET: MemberTutorials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutorial = await _context.Tutorials
                .SingleOrDefaultAsync(m => m.ID == id);
            if (tutorial == null)
            {
                return NotFound();
            }

            return View(tutorial);
        }

        // GET: MemberTutorials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MemberTutorials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,Price,StartDate")] Tutorial tutorial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tutorial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tutorial);
        }

        // GET: MemberTutorials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutorial = await _context.Tutorials.SingleOrDefaultAsync(m => m.ID == id);
            if (tutorial == null)
            {
                return NotFound();
            }
            return View(tutorial);
        }

        // POST: MemberTutorials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,Price,StartDate")] Tutorial tutorial)
        {
            if (id != tutorial.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tutorial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TutorialExists(tutorial.ID))
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
            return View(tutorial);
        }

        // GET: MemberTutorials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutorial = await _context.Tutorials
                .SingleOrDefaultAsync(m => m.ID == id);
            if (tutorial == null)
            {
                return NotFound();
            }

            return View(tutorial);
        }

        // POST: MemberTutorials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tutorial = await _context.Tutorials.SingleOrDefaultAsync(m => m.ID == id);
            _context.Tutorials.Remove(tutorial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TutorialExists(int id)
        {
            return _context.Tutorials.Any(e => e.ID == id);
        }
    }
}
