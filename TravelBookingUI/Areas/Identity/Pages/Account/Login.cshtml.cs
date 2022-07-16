// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TravelBookingUI.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace TravelBookingUI.Areas.Identity.Pages.Account
{
    public class LoginDataModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private UserManager<ApplicationUser> _userManager;
        public LoginDataModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }
        public string BaseUrl = "http://localhost:41916";
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }


        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string LoginId { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }
        /*
       * Login Method 
       * endpoint Login
       * @authod Shivani Bansod
       * 
       */
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            Input.RememberMe = true;
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:8000/api/Authenticate/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //HTTP GET
                    var responseTask = client.PostAsJsonAsync("login", Input);
                    responseTask.Wait();

                    var res = responseTask.Result;
                    if (res.IsSuccessStatusCode)
                    {
                        var content =await res.Content.ReadAsStringAsync();

                        var auths = JsonConvert.DeserializeObject<AuthModel>(content);
                        AuthModel obj = new AuthModel();
                        obj = auths;
                        var handler = new JwtSecurityTokenHandler();
                        // read token
                        var jwt = handler.ReadJwtToken(auths.Token);
                        // get all the claims from token
                        var Name = jwt.Claims.First(claim => claim.Type == "Name").Value;
                        var UserTypeId = jwt.Claims.First(claim => claim.Type == "UserTypeId").Value;
                        var LoginId = jwt.Claims.First(claim => claim.Type == "LoginId").Value;
                        var UserId = jwt.Claims.First(claim => claim.Type == "UserId").Value;


                        //save in session
                        HttpContext.Session.SetString("Name", Name);

                        HttpContext.Session.SetString("UserTypeId", UserTypeId.ToUpper());

                        HttpContext.Session.SetString("LoginId", LoginId);
                        HttpContext.Session.SetString("UserId", UserId);
                        HttpContext.Session.SetString("Token", auths.Token);
                        return LocalRedirect(returnUrl);
                        /*HttpContext.Response.Cookies.Append("LoggedInCookie", content, new Microsoft.AspNetCore.Http.CookieOptions()
                        {
                            Path = "/"
                        });
                        var result = await _signInManager.PasswordSignInAsync(Input.LoginId, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            _logger.LogInformation("User logged in.");
                            return LocalRedirect(returnUrl);
                        }
                        return RedirectToPage("/Home/Index");*/
                    }
                    else //web api sent error response 
                    {
                        ErrorMessage = "Error ocured in login.";
                        ModelState.AddModelError(string.Empty, ErrorMessage);
                        return Page();
                    }
                }

            }
            ErrorMessage = "Please fill the form.";
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
