namespace CarAnnouncementProject.Models
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MarkaId { get; set; }
        public Marka Marka { get; set; }
        public ICollection<Announcement> Announcements { get; set; }
    }
}
