using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Models
{
    public class WorkoutPlan
    {
        [Key]
        public int WorkoutPlanId { get; set; }

        [Required(ErrorMessage = "Podaj nazwę planu")]
        [MaxLength(100)]
        [Display(Name = "Nazwa planu")]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        [Display(Name = "Opis")]
        public string? Description { get; set; }

        public virtual ICollection<WorkoutSession>? Sessions { get; set; }
    }
}