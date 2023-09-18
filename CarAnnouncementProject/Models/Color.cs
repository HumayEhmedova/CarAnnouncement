namespace CarAnnouncementProject.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
        public ICollection<Announcement> Announcements { get; set;}
    }
}
