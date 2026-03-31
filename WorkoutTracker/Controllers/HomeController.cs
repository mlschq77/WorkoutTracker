using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data;

namespace WorkoutTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly WorkoutContext _context;

        public HomeController(WorkoutContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? planId)
        {
            ViewBag.Plans = new SelectList(_context.WorkoutPlans, "WorkoutPlanId", "Name", planId);
            ViewBag.SelectedPlan = planId;

            var sessions = _context.WorkoutSessions
                .Include(s => s.WorkoutPlan)
                .Include(s => s.SessionExercises!)
                    .ThenInclude(se => se.Exercise)
                .OrderByDescending(s => s.Date)
                .AsQueryable();

            if (planId.HasValue)
            {
                sessions = sessions.Where(s => s.WorkoutPlanId == planId.Value);
            }

            return View(sessions.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}