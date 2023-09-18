using Microsoft.AspNetCore.Identity;

namespace CarAnnouncementProject.Models
{
    public class AppUser:IdentityUser
    {
        public ICollection<Announcement> Announcements { get; set; }
    }
}
