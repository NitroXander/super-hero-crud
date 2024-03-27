﻿using SuperHeros.Models;
using Microsoft.EntityFrameworkCore;

namespace SuperHeros
{
    // DbContext is from EntityFramework inbuild class we need to inherit it to use it in our project
    public class ApplicationDbContext : DbContext
    {
        // constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public virtual DbSet<HeroModel> Heroes { get; set; }
        public virtual DbSet<UserModel> Users { get; set; }
        public virtual DbSet<RoleModel> Roles { get; set; }
        public virtual DbSet<CounryCodes> CounryCodes { get; set; }
        public virtual DbSet<LoginDetailsModel> LoginDetails { get; set; }
    }

}
