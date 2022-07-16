using System.ComponentModel.DataAnnotations;

namespace TravelBookingUI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string LoginId { get; set; }

        public bool RememberMe { get; set; }
    }
}
