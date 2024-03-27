using System.ComponentModel.DataAnnotations;

namespace SuperHeros.DTOs
{
    public class RoleDTO
    {
        [Required]
        public long id { get; set; }

        [Required]
        public string role_name { get; set; }

        [Required]
        public string role_description { get; set; }

        [Required]
        public string permissions { get; set; }
    }
}
