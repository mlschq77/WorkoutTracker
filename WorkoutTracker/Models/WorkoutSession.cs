using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WorkoutTracker.Models
{
    public class WorkoutSession
    {
        [Key]
        public int WorkoutSessionId { get; set; }

        [Required(ErrorMessage = "Podaj datę treningu")]
        [Display(Name = "Data treningu")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [MaxLength(300)]
        [Display(Name = "Notatki")]
        public string? Notes { get; set; }

        [ForeignKey("WorkoutPlan")]
        [Display(Name = "Plan treningowy")]
        public int WorkoutPlanId { get; set; }

        [ValidateNever]
        public virtual WorkoutPlan WorkoutPlan { get; set; } = null!;

        [ValidateNever]
        public virtual ICollection<SessionExercise>? SessionExercises { get; set; }
    }
}