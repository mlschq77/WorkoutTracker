using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data;
using WorkoutTracker.Models;
using Microsoft.AspNetCore.Authorization;

namespace WorkoutTracker.Controllers
{
    [Authorize] // Domyślnie wymagaj logowania dla wszystkich akcji
    public class ExercisesController : Controller
    {
        private readonly WorkoutContext _context;

        public ExercisesController(WorkoutContext context)
        {
            _context = context;
        }

        // GET: Exercises
        [AllowAnonymous] // Publiczny dostęp do listy i wyszukiwania
        public async Task<IActionResult> Index(string searchString)
        {
            var exercises = from e in _context.Exercises
                            select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                exercises = exercises.Where(s => s.Name!.Contains(searchString)
                                              || s.Category!.Contains(searchString));
            }

            return View(await exercises.ToListAsync());
        }

        // GET: Exercises/Details/5
        [AllowAnonymous] // Publiczny dostęp do szczegółów
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var exercise = await _context.Exercises
                .FirstOrDefaultAsync(m => m.ExerciseId == id);

            if (exercise == null) return NotFound();

            return View(exercise);
        }

        // GET: Exercises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Exercises/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExerciseId,Name,Description,Category")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(exercise);
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise == null) return NotFound();
            return View(exercise);
        }

        // POST: Exercises/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExerciseId,Name,Description,Category")] Exercise exercise)
        {
            if (id != exercise.ExerciseId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.ExerciseId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var exercise = await _context.Exercises
                .FirstOrDefaultAsync(m => m.ExerciseId == id);
            if (exercise == null) return NotFound();

            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercise = await _context.Exercises.FindAsync(id);
            if (exercise != null)
            {
                _context.Exercises.Remove(exercise);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.ExerciseId == id);
        }
    }
}