using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieWebApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        

        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorMovie> ActorMovies { get; set; }
        public DbSet<ActorRating> ActorRatings { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
