using CarAnnouncementProject.DTOs.AutenticationrDtos;
using CarAnnouncementProject.DTOs.RegisterDtos;
using CarAnnouncementProject.Enums;
using CarAnnouncementProject.Exceptions;
using CarAnnouncementProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace CarAnnouncementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        public AccountsController(UserManager<AppUser> userManager,
                                 IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        #region Register
        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromForm] RegisterDto registerDto)
        {
            try
            {
                AppUser user = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                    PhoneNumber = registerDto.PhoneNumber
                };
                IdentityResult identityResult = await _userManager.CreateAsync(user, registerDto.Password);
                if (!identityResult.Succeeded)
                {
                    string errors = "";
                    foreach (IdentityError error in identityResult.Errors)
                    {
                        errors += error.Description;
                    }
                    throw new UserCreateFailedException(errors);
                }
                IdentityResult roleResult = await _userManager.AddToRoleAsync(user, RoleTypes.Member.ToString());
                if (!roleResult.Succeeded)
                {
                    string errors = "";
                    foreach (IdentityError error in roleResult.Errors)
                    {
                        errors += error.Description;
                    }
                    throw new RoleCreateFailedException(errors);
                }
                return Ok("User created successfully");
            }
            catch (UserCreateFailedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (RoleCreateFailedException)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        #endregion


        #region Login
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromForm] LoginDto loginDto)
        {
            try
            {
                AppUser user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user == null) throw new AuthFailedException("Email or password incorrect");
                if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
                {
                    throw new AuthFailedException("Email or password incorrect");
                };
                //security key
                SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));
                SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
                //claims
                List<Claim> claims = new()
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.NameIdentifier,user.Id),
                    new Claim(ClaimTypes.Email,user.Email)
                };
                //get user role and set to claim
                var roles = await _userManager.GetRolesAsync(user);
                foreach (string role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                //JWT yaradilmasi
                JwtSecurityToken JwtSecurityToken = new(
               issuer: _configuration["Jwt:Issuer"],
               audience: _configuration["Jwt:Audience"],
               claims: claims,
               notBefore: DateTime.UtcNow,
               expires: DateTime.UtcNow.AddMonths(1),
               signingCredentials: signingCredentials
                );
                ////Token-ni user-a verilmesi(string-e cevrilmesi)
                JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
                var token = jwtSecurityTokenHandler.WriteToken(JwtSecurityToken);
                TokenResponseDto tokenResponseDto = new TokenResponseDto()
                {
                    Token = token,
                    ExpireDate = JwtSecurityToken.ValidTo,
                    Username = user.UserName,
                    UserId = user.Id.ToString(),

                };
                return Ok(tokenResponseDto);
            }
            catch (AuthFailedException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion


    }
}
