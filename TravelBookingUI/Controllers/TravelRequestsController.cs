using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using TravelBookingUI.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using TravelBookingUI.Context;

namespace TravelBookingUI.Controllers
{
    public class TravelRequestsController : Controller
    {

        public TravelRequestsController()
        {
          
        }
        // GET: TravelRequests
        public async Task<IActionResult> Index(string id)
        {
            var LoginId = HttpContext.Session.GetString("LoginId");
            var user = new { LoginId= LoginId};
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:8000/api/JourneyTickets/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HTTP GET
                var responseTask = client.PostAsJsonAsync("GetAllRequests", user);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var content = await result.Content.ReadAsStringAsync();

                    var request = JsonConvert.DeserializeObject<List<JourneyRequest>>(content);
                    List<JourneyRequest> obj = new List<JourneyRequest>();
                    obj = request;
                    return View(obj);
                }
               

                    return View();
               
            }
            return View();
        }


        // GET: TravelRequests/Details/5
        public async Task<IActionResult> Details(string Id)
        {
            var request = new { RequestId = Id };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:8000/api/JourneyTickets/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HTTP GET
                var responseTask = client.PostAsJsonAsync("GetRequestDetails", request);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var r = JsonConvert.DeserializeObject<JourneyRequest>(content);
                    JourneyRequest req = new JourneyRequest();
                    req = r;
                    return View(r);
                }


                return View();

            }
            return View();
        }

        // GET: TravelRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TravelRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Source,Destination,TravelDate,TravelMode")] JourneyRequest travelRequests)
        {
            DateTime today = DateTime.Today;
          
            travelRequests.LoginId = HttpContext.Session.GetString("LoginId");
            travelRequests.UserId = HttpContext.Session.GetString("UserId");
            travelRequests.CurrentStatus = "SUBMITTED";
            travelRequests.Name = HttpContext.Session.GetString("Name");
            if (travelRequests.TravelDate < today)
            {
                ModelState.AddModelError("dateerror", "Please select a valid date");
                return View(travelRequests);
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:8000/api/JourneyTickets/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HTTP GET
                var responseTask = client.PostAsJsonAsync("CreateRequest", travelRequests);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }


                return View();

            }
            return View();
        }

        // GET: TravelRequests/Edit/5
        public async Task<IActionResult> Edit(string Id)
        {
            var request = new { RequestId = Id};
           using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:8000/api/JourneyTickets/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HTTP GET
                var responseTask = client.PostAsJsonAsync("GetRequestDetails", request);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var r = JsonConvert.DeserializeObject<JourneyRequest>(content);
                    JourneyRequest req = new JourneyRequest();
                    req = r;
                    req.TravelDate = (DateTime)r.TravelDate;
                    return View(r);
                }


                return View();

            }
            return View();
        }

        // POST: TravelRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string RequestId, [Bind("RequestId,Source,Destination,UserId,TravelTime,TravelMode,CurrentStatus")] JourneyRequest travelRequests)
        {
            var request = new { RequestId = RequestId };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:8000/api/JourneyTickets/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HTTP GET

                var responseTask = client.PostAsJsonAsync("GetRequestDetails", request);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var r = JsonConvert.DeserializeObject<JourneyRequest>(content);
                    JourneyRequest req = new JourneyRequest();
                    req = r;
                    req.TravelDate = (DateTime)r.TravelDate;
                    req.CurrentStatus = travelRequests.CurrentStatus;
                    var responseTask1 = client.PostAsJsonAsync("EditRequest", req);
                    responseTask.Wait();

                    var result1 = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    return View();
                }


               


                return View();

            }
        }

        // GET: TravelRequests/Delete/5
        public async Task<IActionResult> Delete(string Id)
        {

            var request = new { RequestId = Id };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:8000/api/JourneyTickets/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HTTP GET
                var responseTask = client.PostAsJsonAsync("GetRequestDetails", request);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var r = JsonConvert.DeserializeObject<JourneyRequest>(content);
                    JourneyRequest req = new JourneyRequest();
                    req = r;
                    return View(r);
                }


                return View();

            }
            return View();
        }

        // POST: TravelRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string RequestId)
        {
            var request = new { RequestId = RequestId };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:8000/api/JourneyTickets/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HTTP GET
                var responseTask = client.PostAsJsonAsync("DeleteRequest", request);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }


                return View();

            }
            return View();
        }

       
    }
}
