using System.ComponentModel.DataAnnotations;

namespace CarAnnouncementProject.Models
{
    public class FuelType
    {
        public int Id { get; set; }
        
        public string FuelName { get; set; }
        public ICollection<Announcement> Announcements { get; set; }
    }
}
