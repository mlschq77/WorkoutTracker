using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data;
using WorkoutTracker.Models;
using Microsoft.AspNetCore.Authorization;

namespace WorkoutTracker.Controllers
{
    [Authorize] // Niezalogowany zostanie przekierowany do /Auth/Login
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
            if (id == null) return NotFound();

            var plan = await _context.WorkoutPlans
                .FirstOrDefaultAsync(m => m.WorkoutPlanId == id);

            if (plan == null) return NotFound();

            return View(plan);
        }

        // GET: WorkoutPlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkoutPlans/Create
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
            if (id == null) return NotFound();
            var plan = await _context.WorkoutPlans.FindAsync(id);
            if (plan == null) return NotFound();
            return View(plan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkoutPlanId,Name,Description")] WorkoutPlan workoutPlan)
        {
            if (id != workoutPlan.WorkoutPlanId) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(workoutPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workoutPlan);
        }

        // GET: WorkoutPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var plan = await _context.WorkoutPlans.FirstOrDefaultAsync(m => m.WorkoutPlanId == id);
            if (plan == null) return NotFound();
            return View(plan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plan = await _context.WorkoutPlans.FindAsync(id);
            if (plan != null) _context.WorkoutPlans.Remove(plan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}