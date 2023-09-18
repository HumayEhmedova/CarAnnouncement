using System.ComponentModel.DataAnnotations;

namespace CarAnnouncementProject.DTOs.AutenticationrDtos
{
    public class LoginDto
    {
        [MaxLength(300)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [MaxLength(200)]
        [DataType(DataType.Password)]
        public string  Password { get; set; }
    }
}
