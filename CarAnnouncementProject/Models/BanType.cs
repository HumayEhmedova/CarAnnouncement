namespace CarAnnouncementProject.Models
{
    public class BanType
    {
        public int Id { get; set; }
        public string BanName { get; set; }
        public ICollection<Announcement> Announcements { get; set; }
    }
}
