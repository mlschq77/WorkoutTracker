using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WorkoutTracker.Models
{
    public class SessionExercise
    {
        [Key]
        public int SessionExerciseId { get; set; }

        [Range(1, 20, ErrorMessage = "Serie muszą być między 1 a 20")]
        [Display(Name = "Liczba serii")]
        public int Sets { get; set; }

        [Range(1, 999, ErrorMessage = "Powtórzenia muszą być między 1 a 999")]
        [Display(Name = "Powtórzenia")]
        public int Reps { get; set; }

        [Display(Name = "Ciężar (kg)")]
        [Column(TypeName = "decimal(6,2)")]
        public decimal? Weight { get; set; }

        [ForeignKey("WorkoutSession")]
        public int WorkoutSessionId { get; set; }

        [ValidateNever]
        public virtual WorkoutSession WorkoutSession { get; set; } = null!;

        [ForeignKey("Exercise")]
        public int ExerciseId { get; set; }

        [ValidateNever]
        public virtual Exercise Exercise { get; set; } = null!;
    }
}