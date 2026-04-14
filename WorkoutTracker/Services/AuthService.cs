using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data;
using WorkoutTracker.Models;

namespace WorkoutTracker.Services
{
    public class AuthService
    {
        private readonly WorkoutContext _context;

        public AuthService(WorkoutContext context)
        {
            _context = context;
        }

        // Hashowanie hasła — BCrypt zamienia "haslo123" na długi ciąg znaków
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Sprawdzenie czy podane hasło pasuje do hasha w bazie
        public bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        // Rejestracja — zwraca null jeśli login lub email już istnieje
        public async Task<User?> Register(string username, string firstName,
            string lastName, string email, string password)
        {
            // Sprawdź czy login lub email już zajęty
            var exists = await _context.Users
                .AnyAsync(u => u.Username == username || u.Email == email);

            if (exists) return null;

            var user = new User
            {
                Username = username,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = HashPassword(password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Logowanie — zwraca użytkownika jeśli dane poprawne
        public async Task<User?> Login(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null) return null;
            if (!VerifyPassword(password, user.PasswordHash)) return null;

            return user;
        }
    }
}