using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutTracker.Models
{
    public class WorkoutSession
    {
        [Key]
        public int WorkoutSessionId { get; set; }

        [Required(ErrorMessage = "Data jest wymagana")]
        [Display(Name = "Data treningu")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Podaj czas trwania")]
        [Display(Name = "Czas trwania (minuty)")]
        public int DurationMinutes { get; set; }

        [Display(Name = "Notatki")]
        public string? Notes { get; set; }

        // Relacja do Planu
        [Required(ErrorMessage = "Musisz wybrać plan")]
        [Display(Name = "Plan treningowy")]
        public int WorkoutPlanId { get; set; }

        [ForeignKey("WorkoutPlanId")]
        public virtual WorkoutPlan? WorkoutPlan { get; set; }

        public virtual ICollection<SessionExercise> SessionExercises { get; set; } = new List<SessionExercise>();
    }
}