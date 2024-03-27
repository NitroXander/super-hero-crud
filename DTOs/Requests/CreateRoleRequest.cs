using System.ComponentModel.DataAnnotations;

namespace SuperHeros.DTOs.Requests
{
    public class CreateRoleRequest
    {
        [Required]
        public string role_name { get; set; }

        [Required]
        public string role_description { get; set; }

        [Required]
        public string permissions { get; set; }
    }
}
