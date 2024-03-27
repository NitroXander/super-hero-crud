using System.ComponentModel.DataAnnotations;

namespace SuperHeros.DTOs.Requests
{
    public class UserLoginRequest
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
