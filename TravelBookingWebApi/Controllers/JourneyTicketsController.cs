using TravelBookingWebApi.Context;
using TravelBookingWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace TravelBookingWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class JourneyTicketsController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public JourneyTicketsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }


        [HttpPost]
        [Route("GetAllRequests")]
        public async Task<IActionResult> GetAllRequests([FromBody] DeleteModel model)
        {

            var user = await userManager.FindByEmailAsync(model.LoginId);
            List<JourneyRequest> requests = new List<JourneyRequest>();
            requests = _context.JourneyRequests != null ?
                      await _context.JourneyRequests.ToListAsync() : null;
            var users = _context.Users != null? await _context.Users.ToListAsync(): null;
         
            if (user != null)
            {

                if (user.UserTypeId.ToUpper() == "MANAGER")
                {
                    var reportees = users.FindAll(u => u.ManagerUserId == user.UserId);
                    List<JourneyRequest> finalRequests = new List<JourneyRequest>();
                    foreach(var req in requests)
                    {
                        foreach(var us in reportees)
                        {
                            if(req.UserId == us.UserId || req.UserId == user.UserId)
                            {
                                finalRequests.Add(req);
                            }
                        }
                    }
                    return Ok(finalRequests.OrderBy(x => x.TravelDate).ToList());
                }

                else if (user.UserTypeId.ToUpper() == "EMPLOYEE")
                {
                    List<JourneyRequest> finalRequests = new List<JourneyRequest>();
                    foreach (var req in requests)
                    {
                      
                            if (req.UserId == user.UserId)
                            {
                                finalRequests.Add(req);
                            }

                    }
                    return Ok(finalRequests.OrderBy(x => x.TravelDate).ToList());
                }
                else
                {
                    return Ok(requests.OrderBy(x => x.TravelDate).ToList());
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User not authenticated" });



        }

        [HttpPost]
        [Route("CreateRequest")]
        public async Task<IActionResult> CreateRequest([FromBody] JourneyRequest model)
        {
            List<JourneyRequest> requests = new List<JourneyRequest>();
            requests = _context.JourneyRequests != null ?
                      await _context.JourneyRequests.ToListAsync() : null;
            model.RequestId = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
            model.CurrentStatus = "SUBMITTED";
            _context.JourneyRequests.Add(model);

            await _context.SaveChangesAsync();
            return Ok(new { Status = "Success", Message = "Request Added" });
        }

        [HttpPost]
        [Route("EditRequest")]
        public async Task<IActionResult> EditRequest([FromBody] JourneyRequest model)
        {

            List<JourneyRequest> JourneyRequests = new List<JourneyRequest>();

            JourneyRequests = _context.JourneyRequests != null ?
                      await _context.JourneyRequests.ToListAsync() : null;

            var request = JourneyRequests.Find(r => r.RequestId == model.RequestId);
            if (request == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Jouney request not found." });

            }

            request.CurrentStatus = model.CurrentStatus;

            _context.JourneyRequests.Update(request);

            await _context.SaveChangesAsync();
            return Ok(new { Status = "Success", Message = "Request Edited" });
        }
        [HttpPost]
        [Route("GetRequestDetails")]
        public async Task<IActionResult> GetRequestDetails([FromBody] GetModel Model)
        {
            List<JourneyRequest> requests = new List<JourneyRequest>();
            requests = _context.JourneyRequests != null ?
                      await _context.JourneyRequests.ToListAsync() : null;
            var request = requests.Find(r => r.RequestId.ToString() == Model.RequestId.ToString());
            return request != null ? Ok(request) : StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Jouney request not found." });
        }
        [HttpPost]
        [Route("DeleteRequest")]
        public async Task<IActionResult> DeletRequest([FromBody] GetModel model)
        {

            List<JourneyRequest> JourneyRequests = new List<JourneyRequest>();

            JourneyRequests = _context.JourneyRequests != null ?
                      await _context.JourneyRequests.ToListAsync() : null;

            var request = JourneyRequests.Find(r => r.RequestId == model.RequestId);
            if (request == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "Jouney request not found." });

            }

            _context.JourneyRequests.Remove(request);

            await _context.SaveChangesAsync();
            return Ok(new { Status = "Success", Message = "Request Deleted" });
        }

    }
}
