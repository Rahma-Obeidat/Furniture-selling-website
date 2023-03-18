using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Masters.Models
{
    public class User:IdentityUser
    {
        [Key]
        string id { get; set; }
        public string RoleId { get; set; }

    }
}
