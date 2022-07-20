using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelBookingWebApi.Context;
using TravelBookingWebApi.Models;

namespace TravelBookingWebApi.Controllers
{

  /*
  * @authod Shivani Bansod
  * 
  */
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.context = context;
            this.roleManager = roleManager; 
        }


        /*
      * EditUser Method 
      * endpoint EditUser
      * @authod Shivani Bansod
      * 
      */
        [HttpPost]
        [Route("EditUser")]
        public async Task<IActionResult> EditUser([FromBody] EditModel model)
        {
            var user = await userManager.FindByEmailAsync(model.LoginId);
            user.ManagerUserId = model.ManagerUserId;
            user.Name = model.Name;
            user.UserTypeId = model.UserTypeId;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync(user.UserTypeId))
                    await roleManager.CreateAsync(new IdentityRole(user.UserTypeId));


                if (await roleManager.RoleExistsAsync(user.UserTypeId))
                {
                    await userManager.AddToRoleAsync(user, user.UserTypeId);
                }
                return Ok(new { Status = "Success", Message = "User updated successfully." });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new  { Status = "Error", Message = "User not updated." });
        }

        /*
      * GetUser Method 
      * endpoint GetUser
      * @authod Shivani Bansod
      * 
      */
        [HttpPost]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser([FromBody] DeleteModel model)
        {

            var user = await userManager.FindByEmailAsync(model.LoginId);
           
            if(user != null)
            {
                return Ok(user);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User not updated." });
        }

        /*
      * DeleteUser Method 
      * endpoint DeleteUser
      * @authod Shivani Bansod
      * 
      */
        [HttpPost]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteModel model)
        {
            var user = await userManager.FindByEmailAsync(model.LoginId);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new  { Status = "Error", Message = "User not found." });
            }
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok(new  { Status = "Success", Message = "User deleted successfully." });
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new  { Status = "Error", Message = "User not updated." });
        }

        /*
      * GetAllUsers Method 
      * endpoint GetAllUsers
      * @authod Shivani Bansod
      * 
      */
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return context.Users != null ? Ok(await context.Users.ToListAsync()) : StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User not found" });
        }

        [HttpGet]
        [Route("GetAllRequest")]
        public async Task<IActionResult> GetAllRequest()
        {
            return context.JourneyRequests != null ? Ok(await context.JourneyRequests.ToListAsync()) : StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User not found" });
        }



        /*
      * GetAllUsersBasedOnId Method 
      * endpoint GetAllUsersBasedOnId
      * @authod Shivani Bansod
      * 
      */
        [HttpGet]
        [Route("GetAllUsersBasedOnId")]

        public async Task<IActionResult> GetAllUsersForShow([FromQuery(Name = "LoginId")] string LoginId)
        {
            var user = await userManager.FindByEmailAsync(LoginId);
            string Role = user.UserTypeId.ToUpper();

            if(Role == "ADMIN")
            {
                return context.Users != null ? Ok(await context.Users.ToListAsync()) : StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Users not found" });
            }
            else if(Role == "MANAGER")
            {
                List<ApplicationUser> Users = new List<ApplicationUser>();
                var users = await userManager.GetUsersInRoleAsync("EMPLOYEE");
                foreach(var u in users)
                {
                    if(u.ManagerUserId == user.UserId)
                    {
                        Users.Add(u);
                    }
                }
                return Users != null ? Ok(Users) : StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Users not found" });

            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Error Fetching List." });
            }
            
        }
    }
}
