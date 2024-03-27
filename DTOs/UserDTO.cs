using System.ComponentModel.DataAnnotations;

namespace SuperHeros.DTOs
{
    public class UserDTO
    {
        [Required]
        public string first_name { get; set; }

        [Required]
        public string last_name { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string country { get; set; }

        [Required]
        public string state { get; set; }

        [Required]
        public string city { get; set; }

        [Required]
        public string address { get; set; }

        [Required]
        public string country_code { get; set; }

        [Required]
        public string phone_number { get; set; }

        [Required]
        public string password { get; set; }

        public long role { get; set; }

        public DateTime created_at { get; set; }

        public DateTime updated_at { get; set; }
    }
}
