using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHeros.DTOs.Requests;
using SuperHeros.DTOs.Responces;
using SuperHeros.Services.HeroService;

namespace SuperHeros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly IHeroService heroService;

        public HeroController(IHeroService heroService)
        {
            this.heroService = heroService;
        }

        [HttpPost("addCreatedHero")]
        public BaseResponce CreateHero(CreateHeroRequest request)
        {
            return heroService.CreateHero(request);
        }

        [HttpGet("listAvailableHeros")]
        public BaseResponce HeroList()
        {
            return heroService.HeroList();
        }

        [HttpGet("searchHeroById/{id}")]
        public BaseResponce GetHeroById(long id)
        {
            return heroService.GetHeroById(id);
        }

        [HttpGet("searchHeroByName/{string}")]
        public BaseResponce GetHeroByName(string name)
        {
            return heroService.GetHeroByName(name);
        }

        [HttpDelete("deleteHeoById/{id}")]
        public BaseResponce DeleteHeroById(long id)
        {
            return heroService.DeleteHeroById(id);
        }

        [HttpPut("updateHeroById/{id}")]
        public BaseResponce UpdateHeroById(long id, UpdateHeroRequest request)
        {
            return heroService.UpdateHeroById(id, request);
        }
    }
}
