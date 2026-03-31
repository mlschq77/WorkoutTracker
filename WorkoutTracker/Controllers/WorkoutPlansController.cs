using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data;
using WorkoutTracker.Models;

namespace WorkoutTracker.Controllers
{
    public class WorkoutPlansController : Controller
    {
        private readonly WorkoutContext _context;

        public WorkoutPlansController(WorkoutContext context)
        {
            _context = context;
        }

        // GET: WorkoutPlans
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkoutPlans.ToListAsync());
        }

        // GET: WorkoutPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutPlan = await _context.WorkoutPlans
                .FirstOrDefaultAsync(m => m.WorkoutPlanId == id);
            if (workoutPlan == null)
            {
                return NotFound();
            }

            return View(workoutPlan);
        }

        // GET: WorkoutPlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkoutPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkoutPlanId,Name,Description")] WorkoutPlan workoutPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workoutPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workoutPlan);
        }

        // GET: WorkoutPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutPlan = await _context.WorkoutPlans.FindAsync(id);
            if (workoutPlan == null)
            {
                return NotFound();
            }
            return View(workoutPlan);
        }

        // POST: WorkoutPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkoutPlanId,Name,Description")] WorkoutPlan workoutPlan)
        {
            if (id != workoutPlan.WorkoutPlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workoutPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutPlanExists(workoutPlan.WorkoutPlanId))
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
            return View(workoutPlan);
        }

        // GET: WorkoutPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutPlan = await _context.WorkoutPlans
                .FirstOrDefaultAsync(m => m.WorkoutPlanId == id);
            if (workoutPlan == null)
            {
                return NotFound();
            }

            return View(workoutPlan);
        }

        // POST: WorkoutPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workoutPlan = await _context.WorkoutPlans.FindAsync(id);
            if (workoutPlan != null)
            {
                _context.WorkoutPlans.Remove(workoutPlan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutPlanExists(int id)
        {
            return _context.WorkoutPlans.Any(e => e.WorkoutPlanId == id);
        }
    }
}
