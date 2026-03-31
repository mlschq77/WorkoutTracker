using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutTracker.Models
{
    public class Exercise
    {
        [Key]
        public int ExerciseId { get; set; }

        [Required(ErrorMessage = "Podaj nazwę ćwiczenia")]
        [MaxLength(100)]
        [Display(Name = "Nazwa ćwiczenia")]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        [Display(Name = "Opis")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Podaj partię mięśniową")]
        [MaxLength(50)]
        [Display(Name = "Partia mięśniowa")]
        public string MuscleGroup { get; set; } = null!;

        public virtual ICollection<SessionExercise>? SessionExercises { get; set; }
    }
}