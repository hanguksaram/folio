using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Linq;

namespace moviefinder.Models
{
    public class MovieContext: DbContext
    {
        public MovieContext()
        //tweak connection link in web.config
        : base("DefaultConnection")
        {
        }
        //two table database realization
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //creating third many-to-many data table, which connecting actor movie entities
            modelBuilder.Entity<Movie>().HasMany(a => a.actors)
                .WithMany(m => m.movies)
                .Map(t => t.MapLeftKey("movieId")
                .MapRightKey("actorId")
                .ToTable("MovieActor"));

        }
    }
}
