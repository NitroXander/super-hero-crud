using SuperHeros.DTOs;
using SuperHeros.DTOs.Requests;
using SuperHeros.DTOs.Responces;
using SuperHeros.Models;

namespace SuperHeros.Services.HeroService
{
    public class HeroService : IHeroService 
    {
        private readonly ApplicationDbContext context;

        public HeroService(ApplicationDbContext applicationDbContext) 
        { 
            context = applicationDbContext;
        }

        public BaseResponce CreateHero(CreateHeroRequest request)
        {
            BaseResponce response;

            try
            {
                HeroModel newHero = new HeroModel
                {
                    hero_description = request.hero_description,
                    hero_power = request.hero_power,
                    hero_type = request.hero_type,
                    hero_image = request.hero_image,
                    hero_name = request.hero_name,
                    hero_status = request.hero_status
                    
                };


                
                context.Add(newHero);
                context.SaveChanges();
                
                response = new BaseResponce
                {
                    status = StatusCodes.Status200OK,
                    data = new { message = "Hero created successfully" }
                };

                return response;
            }
            catch (Exception ex)
            {
                response = new BaseResponce
                {
                    status = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server Error : " + ex.Message }
                };

                return response;
            }
        }

        public BaseResponce HeroList()
        {
            BaseResponce response;

            try
            {
                List<HeroDTO> heroes = new List<HeroDTO>();

                using (context)
                {
                    context.Heroes.ToList().ForEach(hero => heroes.Add(new HeroDTO {
                        id = hero.id,
                        hero_name = hero.hero_name,
                        hero_description = hero.hero_description,
                        hero_type = hero.hero_type,
                        hero_power = hero.hero_power,
                        hero_status = hero.hero_status,
                        hero_image = hero.hero_image
                    }));
                }

                response = new BaseResponce
                {
                    status = StatusCodes.Status200OK,
                    data = new { heroes }
                };

                return response;
            }
            catch (Exception ex)
            {
                response = new BaseResponce
                {
                    status = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server Error : " + ex.Message }
                };

                return response;
            }
        }

        public BaseResponce GetHeroById(long id)
        {
            BaseResponce response;

            try
            {
                HeroDTO hero = new HeroDTO();

                using(context)
                {
                    HeroModel findHero = context.Heroes.Where(hero => hero.id == id).FirstOrDefault();

                    if(findHero != null)
                    {
                        hero.id = findHero.id;
                        hero.hero_name = findHero.hero_name;
                        hero.hero_description = findHero.hero_description;
                        hero.hero_type = findHero.hero_type;
                        hero.hero_power = findHero.hero_power;
                        hero.hero_status = findHero.hero_status;
                        hero.hero_image = findHero.hero_image;
                    }
                    else
                    {
                        hero = null;
                    }
                }

                if(hero != null)
                {
                    response = new BaseResponce
                    {
                        status = StatusCodes.Status200OK,
                        data = new { hero }
                    };
                }
                else
                {
                    response = new BaseResponce
                    {
                        status = StatusCodes.Status404NotFound,
                        data = new { message = "Hero not found" }
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                response = new BaseResponce
                {
                    status = StatusCodes.Status500InternalServerError,
                    data = new { message = "Interal Server Error : " + ex.Message }
                };

                return response;
            }
        }

        public BaseResponce GetHeroByName(string name)
        {
            BaseResponce response;

            try
            {
                HeroDTO hero = new HeroDTO();

                using (context)
                {
                    HeroModel findHero = context.Heroes.Where(hero => hero.hero_name == name).FirstOrDefault();

                    if (findHero != null)
                    {
                        hero.id = findHero.id;
                        hero.hero_name = findHero.hero_name;
                        hero.hero_description = findHero.hero_description;
                        hero.hero_type = findHero.hero_type;
                        hero.hero_power = findHero.hero_power;
                        hero.hero_status = findHero.hero_status;
                        hero.hero_image = findHero.hero_image;
                    }
                    else
                    {
                        hero = null;
                    }
                }

                if (hero != null)
                {
                    response = new BaseResponce
                    {
                        status = StatusCodes.Status200OK,
                        data = new { hero }
                    };
                }
                else
                {
                    response = new BaseResponce
                    {
                        status = StatusCodes.Status404NotFound,
                        data = new { message = "Hero not found" }
                    };
                }

                return response;
            }
            catch (Exception ex)
            {
                response = new BaseResponce
                {
                    status = StatusCodes.Status500InternalServerError,
                    data = new { message = "Interal Server Error : " + ex.Message }
                };

                return response;
            }
        }

        public BaseResponce UpdateHeroById(long id, UpdateHeroRequest hero)
        {
            BaseResponce response;

            try
            {
                using(context)
                {
                    HeroModel findHero = context.Heroes.Where(hero => hero.id == id).FirstOrDefault();

                    if(findHero != null)
                    {
                        findHero.hero_name = hero.hero_name;
                        findHero.hero_description = hero.hero_description;
                        findHero.hero_power = hero.hero_power;
                        findHero.hero_status = hero.hero_status;
                        findHero.hero_image = hero.hero_image;
                        findHero.hero_type = hero.hero_type;

                        context.SaveChanges();

                        response = new BaseResponce
                        {
                            status = StatusCodes.Status200OK,
                            data = new { message = "Hero updated successfully" }
                        }; 
                        
                    }
                    else
                    {
                        response = new BaseResponce
                        {
                            status = StatusCodes.Status404NotFound,
                            data = new { message = "Hero Not Found" }
                        };
                    }
                }
            }
            catch(Exception ex)
            {
                response = new BaseResponce
                {
                    status = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server Error : " + ex.Message }
                };

            }
            return response;
        }

        public BaseResponce DeleteHeroById(long id)
        {
            BaseResponce response;

            try
            {
                using (context)
                {
                    HeroModel findHero = context.Heroes.Where(hero => hero.id == id).FirstOrDefault();

                    if(findHero != null)
                    {
                        context.Heroes.Remove(findHero);
                        context.SaveChanges();

                        response = new BaseResponce
                        {
                            status = StatusCodes.Status200OK,
                            data = new { message = "Hero deleted successfully" }
                        };
                    }
                    else
                    {
                        response = new BaseResponce
                        {
                            status = StatusCodes.Status404NotFound,
                            data = new { message = "Hero Not Found" }
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                response = new BaseResponce
                {
                    status = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server Error : " + ex.Message }
                };

            }
            return response;
        }
    }
}
