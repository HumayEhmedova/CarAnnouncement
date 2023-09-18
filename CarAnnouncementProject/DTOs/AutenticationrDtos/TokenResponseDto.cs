namespace CarAnnouncementProject.DTOs.AutenticationrDtos
{
    public class TokenResponseDto
    {
        public string? Token { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string? Username { get; set; }
        public string? UserId { get; set; }
    }
}
