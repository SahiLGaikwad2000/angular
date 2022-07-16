using Microsoft.AspNetCore.Identity;
using System;

using System.ComponentModel.DataAnnotations.Schema;

namespace TravelBookingWebApi.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Column(TypeName = "nvarchar(256)")]
        public string? Name { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string? UserTypeId { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string? ManagerUserId { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string? UserId { get; set; }

        [Column(TypeName = "nvarchar(256)")]
        public string? LoginId { get; set; }
        public ApplicationUser()
        {

        }
    }
}
