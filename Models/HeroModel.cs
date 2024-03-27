using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


// THIS IS THE HERO MODEL. HERE WE DEFINE THE STRUCTURE OF THE DATABASE TABLE, EACH TABLE NEED A SEPARATE MODEL
namespace SuperHeros.Models
{
    // NAME OF THE TABLE
    [Table("heros")]
    public class HeroModel
    {
        // PRIMARY KEY FOR THE TABLE AUTO INCREMENT INBUILT 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Required] 
        public string hero_name { get; set; }

        [Required]
        public string hero_description { get; set; }

        [Required]
        public string hero_power { get; set; }

        [Required]
        public string hero_type { get; set; }

        [Required]
        public string hero_status { get; set; }

        public string hero_image { get; set; }

    }
}
