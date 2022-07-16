using System.ComponentModel.DataAnnotations;

namespace TravelBookingWebApi.Models
{
    public class JourneyRequest
    {
        [Key]
        public string? RequestId { get; set; }

        [Required(ErrorMessage = "Source is required")]
        public string? Source { get; set; }

        [Required(ErrorMessage = "Destination is required")]
        public string? Destination { get; set; }

        public string? UserId { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "Travel date is required")]
        public DateTime? TravelDate { get; set; }

        public DateTime? BookingTime { get; set; }

        public string? LoginId { get; set; }

        public string? TravelMode { get; set; }
        public string? CurrentStatus { get; set; }


        public JourneyRequest()
        {

        }
    }

    public class GetModel
    {
        public string? RequestId { get; set; }
    }
}


