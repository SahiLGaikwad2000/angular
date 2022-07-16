using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TravelBookingWebApi.Models;

namespace TravelBookingWebApi.Controllers
{
    /*
     * Authentication APIs
     * @author Shivani Bansod
     */
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        /*
         * Login Method 
         * endpoint login
         * @authod Shivani Bansod
         * 
         */
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            //get user based on username/login
            var user = await userManager.FindByNameAsync(model.LoginId);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                // adding claims to exploit on Frontend
                var authClaims = new List<Claim>
                {
                    new Claim("Name", user.Name),
                    new Claim("LoginId", user.LoginId),
                    new Claim("UserTypeId", user.UserTypeId),

                    new Claim("UserId", user.UserId),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                // JWT token generator
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            return Unauthorized();
        }


        /*
      * register Method 
      * endpoint register
      * @authod Shivani Bansod
      * 
      */
        [HttpPost]

        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            // check if user exists
            var userExists = await userManager.FindByNameAsync(model.LoginId);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new  { Status = "Error", Message = "User already exists!" });
            // creating data to register
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.LoginId,
                LoginId = model.LoginId,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.LoginId,
                UserTypeId = model.UserTypeId,
                UserId = model.UserId,
                Name = model.Name,
            };
            // register method
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await roleManager.RoleExistsAsync(user.UserTypeId))
                await roleManager.CreateAsync(new IdentityRole(user.UserTypeId));
            

            if (await roleManager.RoleExistsAsync(user.UserTypeId))
            {
                await userManager.AddToRoleAsync(user, user.UserTypeId);
            }

            return Ok(new { Status = "Success", Message = "User created successfully!" });
        }
    }
}