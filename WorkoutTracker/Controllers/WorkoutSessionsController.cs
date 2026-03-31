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
    public class WorkoutSessionsController : Controller
    {
        private readonly WorkoutContext _context;

        public WorkoutSessionsController(WorkoutContext context)
        {
            _context = context;
        }

        // GET: WorkoutSessions
        public async Task<IActionResult> Index()
        {
            var workoutContext = _context.WorkoutSessions.Include(w => w.WorkoutPlan);
            return View(await workoutContext.ToListAsync());
        }

        // GET: WorkoutSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutSession = await _context.WorkoutSessions
                .Include(w => w.WorkoutPlan)
                .FirstOrDefaultAsync(m => m.WorkoutSessionId == id);
            if (workoutSession == null)
            {
                return NotFound();
            }

            return View(workoutSession);
        }

        // GET: WorkoutSessions/Create
        public IActionResult Create()
        {
            ViewData["WorkoutPlanId"] = new SelectList(_context.WorkoutPlans, "WorkoutPlanId", "Name");
            return View();
        }

        // POST: WorkoutSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkoutSessionId,Date,Notes,WorkoutPlanId")] WorkoutSession workoutSession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workoutSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkoutPlanId"] = new SelectList(_context.WorkoutPlans, "WorkoutPlanId", "Name", workoutSession.WorkoutPlanId);
            return View(workoutSession);
        }

        // GET: WorkoutSessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutSession = await _context.WorkoutSessions.FindAsync(id);
            if (workoutSession == null)
            {
                return NotFound();
            }
            ViewData["WorkoutPlanId"] = new SelectList(_context.WorkoutPlans, "WorkoutPlanId", "Name", workoutSession.WorkoutPlanId);
            return View(workoutSession);
        }

        // POST: WorkoutSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkoutSessionId,Date,Notes,WorkoutPlanId")] WorkoutSession workoutSession)
        {
            if (id != workoutSession.WorkoutSessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workoutSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutSessionExists(workoutSession.WorkoutSessionId))
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
            ViewData["WorkoutPlanId"] = new SelectList(_context.WorkoutPlans, "WorkoutPlanId", "Name", workoutSession.WorkoutPlanId);
            return View(workoutSession);
        }

        // GET: WorkoutSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutSession = await _context.WorkoutSessions
                .Include(w => w.WorkoutPlan)
                .FirstOrDefaultAsync(m => m.WorkoutSessionId == id);
            if (workoutSession == null)
            {
                return NotFound();
            }

            return View(workoutSession);
        }

        // POST: WorkoutSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workoutSession = await _context.WorkoutSessions.FindAsync(id);
            if (workoutSession != null)
            {
                _context.WorkoutSessions.Remove(workoutSession);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutSessionExists(int id)
        {
            return _context.WorkoutSessions.Any(e => e.WorkoutSessionId == id);
        }
    }
}
