using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data;
using WorkoutTracker.Models;
using Microsoft.AspNetCore.Authorization;

namespace WorkoutTracker.Controllers
{
    [Authorize]
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
            var sessions = _context.WorkoutSessions.Include(w => w.WorkoutPlan);
            return View(await sessions.ToListAsync());
        }

        // GET: WorkoutSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var workoutSession = await _context.WorkoutSessions
                .Include(w => w.WorkoutPlan)
                .FirstOrDefaultAsync(m => m.WorkoutSessionId == id);

            if (workoutSession == null) return NotFound();

            return View(workoutSession);
        }

        // GET: WorkoutSessions/Create
        public IActionResult Create()
        {
            ViewBag.WorkoutPlanId = new SelectList(_context.WorkoutPlans, "WorkoutPlanId", "Name");
            return View();
        }

        // POST: WorkoutSessions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkoutSessionId,Date,DurationMinutes,Notes,WorkoutPlanId")] WorkoutSession workoutSession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workoutSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.WorkoutPlanId = new SelectList(_context.WorkoutPlans, "WorkoutPlanId", "Name", workoutSession.WorkoutPlanId);
            return View(workoutSession);
        }

        // GET: WorkoutSessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var workoutSession = await _context.WorkoutSessions.FindAsync(id);
            if (workoutSession == null) return NotFound();

            ViewBag.WorkoutPlanId = new SelectList(_context.WorkoutPlans, "WorkoutPlanId", "Name", workoutSession.WorkoutPlanId);
            return View(workoutSession);
        }

        // POST: WorkoutSessions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkoutSessionId,Date,DurationMinutes,Notes,WorkoutPlanId")] WorkoutSession workoutSession)
        {
            if (id != workoutSession.WorkoutSessionId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workoutSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.WorkoutSessions.Any(e => e.WorkoutSessionId == workoutSession.WorkoutSessionId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.WorkoutPlanId = new SelectList(_context.WorkoutPlans, "WorkoutPlanId", "Name", workoutSession.WorkoutPlanId);
            return View(workoutSession);
        }

        // GET: WorkoutSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var session = await _context.WorkoutSessions
                .Include(w => w.WorkoutPlan)
                .FirstOrDefaultAsync(m => m.WorkoutSessionId == id);

            if (session == null) return NotFound();

            return View(session);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var session = await _context.WorkoutSessions.FindAsync(id);
            if (session != null) _context.WorkoutSessions.Remove(session);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}