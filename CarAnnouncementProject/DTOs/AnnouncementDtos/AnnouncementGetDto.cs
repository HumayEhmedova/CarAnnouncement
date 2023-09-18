using CarAnnouncementProject.DTOs.Announcement;

namespace CarAnnouncementProject.DTOs.AnnouncementDtos
{
    public class AnnouncementGetDto
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int ProductionYear { get; set; }
        public int Distance { get; set; }
        public int ModelId { get; set; }
        public int MarkaId { get; set; }
        public List<string> ImageUrls { get; set; }

    }
}
