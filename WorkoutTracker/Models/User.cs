using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Login")]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [Display(Name = "Imię")]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(150)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        // Zahaszowane hasło — nigdy nie przechowujemy jawnego!
        [Required]
        public string PasswordHash { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}