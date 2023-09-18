namespace CarAnnouncementProject.DTOs.AnnouncementDtos
{
    public class AnnouncementFilterDto
    {
        public int MinProductionYear { get; set; }
        public int MaxProductionYear { get; set; }
        public int MinDistance { get; set; }
        public int MaxDistance { get; set; }
        public int minPrice { get; set; }
        public int maxPrice { get; set; }
        public int ModelId { get; set; }
        public int MarkaId { get; set; }
        public int ColorId { get; set; }
        public int BanTypeId { get; set; }
        public int EngineVolumeId { get; set; }
        public int FuelTypeId { get; set; }
        public int GearBoxId { get; set; }
       
    }
}
