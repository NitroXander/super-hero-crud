using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHeros.Models
{
    [Table("roles")]
    public class RoleModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Required]
        public string role_name { get; set; }

        [Required]
        public string role_description { get; set; }

        [Required]
        public string permissions { get; set; }
    }
}
