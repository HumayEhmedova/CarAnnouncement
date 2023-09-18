using System.ComponentModel.DataAnnotations;

namespace CarAnnouncementProject.DTOs.RegisterDtos
{
    public class RegisterDto
    {   //Name
        [MaxLength(200)]
        public string Username { get; set; }
        //Email
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [MaxLength(300)]
        public string Email { get; set; }
        //Password
        [DataType(DataType.Password)]
        [MaxLength(200)]
        public string Password { get; set; }
        //ConfirmPassword
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        //PhoneNumber
        [DataType(DataType.PhoneNumber)]
        [StringLength(14, MinimumLength = 10, ErrorMessage = "Phone number must be between 10 and 14 characters long.")]
        public string PhoneNumber { get; set; }
    }
}
