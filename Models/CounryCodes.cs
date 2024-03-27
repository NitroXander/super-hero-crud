using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperHeros.Models
{
    [Table("counry_codes")]
    public class CounryCodes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Required]
        public string country_name { get; set; }

        [Required]
        public string country_code { get; set; }

        [Required]
        public string flag { get; set; }     
    }
}
