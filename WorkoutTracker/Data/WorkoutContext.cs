using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Models;

namespace WorkoutTracker.Data
{
    public class WorkoutContext : DbContext
    {
        public WorkoutContext(DbContextOptions options) : base(options) { }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }
        public DbSet<WorkoutSession> WorkoutSessions { get; set; }
        public DbSet<SessionExercise> SessionExercises { get; set; }
    }
}