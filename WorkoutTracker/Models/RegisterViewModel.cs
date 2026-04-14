using System.ComponentModel.DataAnnotations;

namespace WorkoutTracker.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Podaj login")]
        [MaxLength(50)]
        [Display(Name = "Login")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Podaj imię")]
        [MaxLength(100)]
        [Display(Name = "Imię")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Podaj nazwisko")]
        [MaxLength(100)]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Podaj email")]
        [EmailAddress(ErrorMessage = "Niepoprawny email")]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Podaj hasło")]
        [MinLength(6, ErrorMessage = "Hasło musi mieć minimum 6 znaków")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Powtórz hasło")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła nie są zgodne")]
        [Display(Name = "Powtórz hasło")]
        public string ConfirmPassword { get; set; } = null!;
    }
}