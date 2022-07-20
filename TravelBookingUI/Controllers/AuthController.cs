 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using TravelBookingUI.Models;
using TravelBookingUI.Context;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Authentication;

namespace TravelBookingUI.Controllers
{
    public class AuthController : Controller
    {

        public AuthController()
        {
 
        }
        // GET: AccountController
        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public async Task OnGetRegister(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }


        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.SetString("Name", "");

            HttpContext.Session.SetString("UserTypeId", "");

            HttpContext.Session.SetString("LoginId", "");
            HttpContext.Session.SetString("UserId", "");
            HttpContext.Session.Clear();
            return View();
        }
        public async Task<IActionResult> ListUsers()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:8000/api/User/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HTTP GET
                var responseTask = client.GetAsync("GetAllUsers");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var content = await result.Content.ReadAsStringAsync();

                    var users = JsonConvert.DeserializeObject<List<ApplicationUser>>(content);
                    List<ApplicationUser> obj = new List<ApplicationUser>();
                    obj = users;

                    return View(obj);
                }
                else //web api sent error response 
                {
                    //log response status here..


                    return View("ListUsers");
                }
            }
        }
    }
}