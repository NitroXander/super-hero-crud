﻿using System.ComponentModel.DataAnnotations;

namespace SuperHeros.DTOs.Requests
{
    public class CreateHeroRequest
    {
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
