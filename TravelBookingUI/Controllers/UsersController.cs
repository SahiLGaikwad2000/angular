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

namespace TravelBookingUI.Controllers
{
    public class UsersController : Controller
    {

        public UsersController()
        {
        }
        // GET: AccountController


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
                    var data = obj.FindAll(u => (u.UserTypeId != "ADMIN"));
                    return View(data);
                }
                else //web api sent error response 
                {
                    //log response status here..


                    return View("ListUsers");
                }
            }
        }
        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountController/Create
        public async Task<IActionResult> EditUser(string id)
        {

     
                DeleteModel dl = new DeleteModel();
                dl.LoginId = id;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:8000/api/User/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HTTP GET
                    var responseTask = client.PostAsJsonAsync("GetUser", dl);
                    responseTask.Wait();
                    
                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {

                        var content = await result.Content.ReadAsStringAsync();
                    
                        var user = JsonConvert.DeserializeObject<ApplicationUser>(content);
                        ApplicationUser obj = new ApplicationUser();

                        obj = user;
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //HTTP GET

                        var responseTask1 = client.GetAsync("GetAllUsers");
                        responseTask.Wait();

                        var result1 = responseTask1.Result;
                        if (result1.IsSuccessStatusCode)
                        {

                            var content1 = await result1.Content.ReadAsStringAsync();

                            var users = JsonConvert.DeserializeObject<List<ApplicationUser>>(content1);
                            List<ApplicationUser> obj1 = new List<ApplicationUser>();
                            obj1 = users;
                            ViewBag.ManagerList = users.FindAll(u => u.UserTypeId.ToUpper() == "MANAGER");
                            return View(obj);
                        }
                        else //web api sent error response 
                        {
                            //log response status here..


                            return View("ListUsers");
                        }
                        return View(obj);
                    }
                        else //web api sent error response 
                        {
                        //log response status here..


                        return View("ListUsers");
                    }
                }

            return View("ListUsers");
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        // POST: TravelRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<IActionResult> Edit(string id, [Bind("ManagerUserId,UserTypeId,Name,UserName,LoginId")] EditModel user)
        {
           
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:8000/api/User/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                //HTTP GET
                var responseTask = client.PostAsJsonAsync("Edituser", user);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("ListUsers");
                }
                return RedirectToAction("ListUsers");
            }

        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            DeleteModel dl = new DeleteModel();
            dl.LoginId = id;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:8000/api/User/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HTTP GET
                var responseTask = client.PostAsJsonAsync("GetUser", dl);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var content = await result.Content.ReadAsStringAsync();

                    var user = JsonConvert.DeserializeObject<ApplicationUser>(content);
                    ApplicationUser obj = new ApplicationUser();

                    obj = user;
                    return View(obj);
                }
                return NotFound();
            }

            return NotFound();

        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            DeleteModel dl = new DeleteModel();
            dl.LoginId = id;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:8000/api/User/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HTTP GET
                var responseTask = client.PostAsJsonAsync("DeleteUser", dl);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {


                    return RedirectToAction("ListUsers");
                }
                return NotFound();
            }
            return View();
        }
        public async Task<IActionResult> Logout(string id)
        {
            HttpContext.Session.SetString("Name", "");

            HttpContext.Session.SetString("UserTypeId", "");

            HttpContext.Session.SetString("LoginId", "");
            HttpContext.Session.SetString("UserId", "");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home", new { area = "" });

        }
    }
}
