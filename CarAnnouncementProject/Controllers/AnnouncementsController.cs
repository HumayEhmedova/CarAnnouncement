using AutoMapper;
using CarAnnouncementProject.DAL;
using CarAnnouncementProject.DTOs.Announcement;
using CarAnnouncementProject.DTOs.AnnouncementDtos;
using CarAnnouncementProject.Exceptions;
using CarAnnouncementProject.Extensions.FileExtension;
using CarAnnouncementProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace CarAnnouncementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        public readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public readonly UserManager<AppUser> _userManager;
        public AnnouncementsController(AppDbContext db,
                                       IWebHostEnvironment env,
                                       IMapper mapper,
                                       UserManager<AppUser> userManager)
        {
            _db = db;
            _env = env;
            _mapper = mapper;
            _userManager = userManager;
        }
        #region GetAllAnnouncements
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            List<Announcement> dbAnnouncements = await _db.Announcements.Include(A => A.Images).Include(A => A.Model).ThenInclude(M => M.Marka).ToListAsync();
            List<AnnouncementGetDto> announcementDtos = _mapper.Map<List<AnnouncementGetDto>>(dbAnnouncements);
            return Ok(announcementDtos);
        }
        #endregion


        #region GetAnnouncementById
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid request. The 'id' parameter cannot be null.");
            }
            Announcement dbAnnouncements = await _db.Announcements.Include(a => a.Images).Include(a => a.Model).
            ThenInclude(m => m.Marka).FirstOrDefaultAsync(a => a.Id == id);
            if (dbAnnouncements == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Announcement object don't found");
            }
            AnnouncementGetByIdDto announcementsDto = _mapper.Map<AnnouncementGetByIdDto>(dbAnnouncements);
            return Ok(announcementsDto);
        }
        #endregion


        #region SearchAnnouncementByFilter
        [HttpGet("GetAllbyFilter")]
        public async Task<ActionResult> GetAllbyFilter(int modelId, int markaId, int colorId, int banTypeId, int fuelTypeId, int gearBoxId, decimal maxPrice, decimal minPrice, int minDistance, int maxDistance, int maxProductionYear, int minProductionYear)
        {
            var dbAnnouncements = _db.Announcements.Include(a => a.Images).Include(a => a.Model).ThenInclude(m => m.Marka).
                Where(a =>

                    (a.ModelId == modelId || modelId == 0) &&
                    (a.Model.MarkaId == markaId || markaId == 0) &&
                    (a.ColorId == colorId || colorId == 0) &&
                    (a.BanTypeId == banTypeId || banTypeId == 0) &&
                    (a.FuelTypeId == fuelTypeId || fuelTypeId == 0) &&
                    (a.GearBoxId == gearBoxId || gearBoxId == 0) &&
                    (a.Price <= maxPrice || maxPrice == 0) &&
                    (a.Price >= minPrice || minPrice == 0) &&
                    (a.Price <= maxDistance || maxDistance == 0) &&
                    (a.Distance >= minDistance || minDistance == 0) &&
                    (a.ProductionYear <= maxProductionYear || maxProductionYear == 0) &&
                    (a.ProductionYear >= minProductionYear || minProductionYear == 0)
                ).ToList();

            if (dbAnnouncements.Capacity == 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Sorry, nothing was found based on your search.Please choose more suitable search filters.");
            }
            List<AnnouncementGetByIdDto> mappedAnnouncement = _mapper.Map<List<AnnouncementGetByIdDto>>(dbAnnouncements);
            return Ok(mappedAnnouncement);
        }
        #endregion


        #region CreateAnnouncement
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromForm] AnnouncementPostDto announcementDto)
        {
            //Get Autorized user id or name
            AppUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            Announcement announcement = new Announcement
            {
                ProductionYear = announcementDto.ProductionYear,
                Price = announcementDto.Price,
                EngineVolume = announcementDto.EngineVolume,
                Distance = announcementDto.Distance,
                ModelId = announcementDto.ModelId,
                BanTypeId = announcementDto.BanTypeId,
                ColorId = announcementDto.ColorId,
                FuelTypeId = announcementDto.FuelTypeId,
                GearBoxId = announcementDto.GearBoxId,
                UserId = user.Id
            };
            if (!announcementDto.Images.CheckFileFormat("image"))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Unsupported media type.Please choose only image format");
            }
            if (!announcementDto.Images.CheckFileSize(100))
            {
                return StatusCode(StatusCodes.Status400BadRequest, "The maximum size allowed is 100 kilobytes.");
            }
            foreach (IFormFile file in announcementDto.Images)
            {

                string filename = await file.SaveFileAsync(_env.WebRootPath, "assets", "announcements");
                Image carImage = new Image
                {
                    ImageUrl = filename,
                    Announcement = announcement
                };
                _db.Images.Add(carImage);
            }
            _db.Add(announcement);
            await _db.SaveChangesAsync();
            return Ok();
        }
        #endregion


        #region UpdateAnnouncement
        [Authorize(Roles = "Member")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromForm] AnnouncementUpdateDto announcementDto, int? id)
        {
            //Get Autorized user id or name
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Announcement dbAnnouncement = await _db.Announcements.FindAsync(id);
            if (dbAnnouncement == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Announcement object don't found");
            }
            if (dbAnnouncement.UserId != userId)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "You do not have the necessary permissions to update this announcement.");
            }

            if (announcementDto.ProductionYear != null)
            {
                dbAnnouncement.ProductionYear = (int)announcementDto.ProductionYear;
            }
            if (announcementDto.Price != null)
            {
                dbAnnouncement.Price = (int)announcementDto.Price;
            }
            if (announcementDto.EngineVolume != null)
            {
                dbAnnouncement.EngineVolume = (int)announcementDto.EngineVolume;
            }
            if (announcementDto.Distance != null)
            {
                dbAnnouncement.Distance = (int)announcementDto.Distance;
            }
            if (announcementDto.FuelTypeId != null)
            {
                dbAnnouncement.FuelTypeId = (int)announcementDto.FuelTypeId;
            }
            if (announcementDto.ColorId != null)
            {
                dbAnnouncement.ColorId = (int)announcementDto.ColorId;
            }
            if (announcementDto.BanTypeId != null)
            {
                dbAnnouncement.BanTypeId = (int)announcementDto.BanTypeId;
            }
            if (announcementDto.GearBoxId != null)
            {
                dbAnnouncement.GearBoxId = (int)announcementDto.GearBoxId;
            }
            if (announcementDto.ModelId != null)
            {
                dbAnnouncement.ModelId = (int)announcementDto.ModelId;
            }
            if (announcementDto.Images != null)
            {
                if (!announcementDto.Images.CheckFileFormat("image"))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Unsupported media type.Please choose only image format");
                }
                if (!announcementDto.Images.CheckFileSize(100))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "The maximum size allowed is 100 kilobytes.");
                }
                foreach (IFormFile file in announcementDto.Images)
                {
                    string filename = await file.SaveFileAsync(_env.WebRootPath, "assets", "announcements");
                    Image carImage = new Image
                    {
                        ImageUrl = filename,
                        AnnouncementId = dbAnnouncement.Id
                    };
                    _db.Images.Add(carImage);
                }
            }
            _db.Announcements.Update(dbAnnouncement);
            await _db.SaveChangesAsync();
            return Ok();
        }
        #endregion


        #region DeleteAnnouncement
        [Authorize(Roles = "Member")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Announcement dbAnnouncement = await _db.Announcements.Include(a => a.Images).FirstOrDefaultAsync(a => a.Id == id);
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            if (dbAnnouncement == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Announcement object don't found");
            }
            if (dbAnnouncement.UserId != user.Id)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "You do not have the necessary permissions to delete this announcement.");
            }
            foreach (Image file in dbAnnouncement.Images)
            {
                string path = Path.Combine(_env.WebRootPath, "assets", "announcements", file.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            _db.Announcements.Remove(dbAnnouncement);
            await _db.SaveChangesAsync();
            return Ok("Deleted successfully");
        }

        #endregion


        #region DeleteAnnouncementImage
        [Authorize(Roles = "Member")]
        [HttpDelete("{annountcementId} {imageId}")]
        public async Task<ActionResult> DeleteImage(int annountcementId, int imageId)
        {
            Announcement dbAnnouncement = await _db.Announcements.Include(a => a.Images).FirstOrDefaultAsync(a => a.Id == annountcementId);
            var user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            if (dbAnnouncement == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Announcement object don't found");
            }
            if (dbAnnouncement.UserId != user.Id)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "You do not have the necessary permissions to delete this image.");
            }
            if (dbAnnouncement.Images.Any(i => i.Id == imageId))
            {
                foreach (Image image in dbAnnouncement.Images)
                {
                    if (image.Id == imageId)
                    {
                        _db.Images.Remove(image);
                        string path = Path.Combine(_env.WebRootPath, "assets", "announcements", image.ImageUrl);
                        if (System.IO.File.Exists(path))
                        {
                            System.IO.File.Delete(path);
                        }
                    }
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound, "Image object don't found");
            }
            await _db.SaveChangesAsync();
            return Ok();
        }
        #endregion


    }
}
