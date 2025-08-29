using System.ComponentModel.DataAnnotations;

namespace fin_backend.Models.DTOs
{
    public class UserForAuthDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}
