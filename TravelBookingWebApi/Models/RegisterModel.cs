using System.ComponentModel.DataAnnotations;

namespace TravelBookingWebApi.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        public string UserTypeId { get; set; }

        [Required(ErrorMessage = "User Id is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string LoginId { get; set; }
    }
}
