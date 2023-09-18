namespace CarAnnouncementProject.Models
{
    public class GearBox
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Announcement> Announcement { get; set; }
    }
}
