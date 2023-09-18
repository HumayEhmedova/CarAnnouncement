using CarAnnouncementProject.DAL;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarAnnouncementProject.Models
{
    public class Announcement
    {
        public Announcement()
        {
            this.Images= new List<Image>();
        }
        public int Id { get; set; }
        public int ProductionYear { get; set; }
        public int Price { get; set; }
        public int EngineVolume { get; set; }
        public int Distance { get; set; }
        public FuelType FuelType { get; set; }
        public int FuelTypeId { get; set; }
        public Color Color { get; set; }
        public int ColorId { get; set; }
        public BanType BanType { get; set; }
        public int BanTypeId { get; set; }
        public GearBox GearBox { get; set; }
        public int GearBoxId { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public AppUser AppUser { get; set; }
        public string UserId { get; set; }
        public ICollection<Image> Images { get; set; }
        
    }
}
