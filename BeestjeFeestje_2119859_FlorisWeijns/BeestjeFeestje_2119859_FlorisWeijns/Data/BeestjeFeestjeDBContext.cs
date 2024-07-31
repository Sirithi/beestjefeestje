﻿using BeestjeFeestje_2119859_FlorisWeijns.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeestjeFeestje_2119859_FlorisWeijns.Data
{
    public class BeestjeFeestjeDBContext(DbContextOptions<BeestjeFeestjeDBContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<AType> Types { get; set; }

        public DbSet<Farm> Farms { get; set; }
    }
}
