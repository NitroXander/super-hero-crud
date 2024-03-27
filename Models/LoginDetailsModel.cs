using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHeros.Models
{
    [Table("login_data")]
    public class LoginDetailsModel
    {
        [Required]
        public long id { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string jwt_token { get; set; }

        public string role { get; set; }

        public string permissions { get; set; }

    }
}
