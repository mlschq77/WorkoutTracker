using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Podaj login")]
        [Display(Name = "Login")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Podaj hasło")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; } = null!;
    }
}