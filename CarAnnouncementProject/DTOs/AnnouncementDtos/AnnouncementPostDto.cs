﻿using Microsoft.AspNetCore.Http;

namespace CarAnnouncementProject.DTOs.Announcement
{
    public class AnnouncementPostDto
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
        public List<IFormFile> Images { get; set; }
    }
}
