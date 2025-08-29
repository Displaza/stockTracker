namespace fin_backend.Models.DTOs
{
    public class AuthResponseDto
    {
        //public bool IsAuthSuccessful { get; set; }
        //public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
