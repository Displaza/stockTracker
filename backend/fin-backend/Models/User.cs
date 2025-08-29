namespace fin_backend.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        // Store only the hashed password
        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "User"; // Optional, for role-based auth
    }
}
