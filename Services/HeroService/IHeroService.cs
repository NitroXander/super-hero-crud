using SuperHeros.DTOs.Requests;
using SuperHeros.DTOs.Responces;

namespace SuperHeros.Services.HeroService
{
    public interface IHeroService
    {
        BaseResponce CreateHero(CreateHeroRequest request);

        BaseResponce HeroList();

        BaseResponce GetHeroById(long id);

        BaseResponce GetHeroByName(string name);

        BaseResponce UpdateHeroById(long id, UpdateHeroRequest request);

        BaseResponce DeleteHeroById(long id);
    }
}
