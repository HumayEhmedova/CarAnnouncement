namespace CarAnnouncementProject.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public Announcement Announcement { get; set; }
        public int AnnouncementId { get; set; }
    }
}
