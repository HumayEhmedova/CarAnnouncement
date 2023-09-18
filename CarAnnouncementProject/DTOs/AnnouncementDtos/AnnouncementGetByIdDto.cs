using CarAnnouncementProject.DTOs.Announcement;

namespace CarAnnouncementProject.DTOs.AnnouncementDtos
{
    public class AnnouncementGetByIdDto
    {
        public int ProductionYear { get; set; }
        public int Price { get; set; }
        public int EngineVolume { get; set; }
        public int Distance { get; set; }
        public int ModelId { get; set; }
        public int BanTypeId { get; set; }
        public int ColorId { get; set; }
        public int FuelTypeId { get; set; }
        public int GearBoxId { get; set; }
        public int MarkaId { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
