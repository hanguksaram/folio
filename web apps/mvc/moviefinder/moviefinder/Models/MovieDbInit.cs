using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace moviefinder.Models
{
    //Code first template
    //initialization database with test movie's and actor's state data
    //set initialization in Global.asax
    /// <summary>
    /// Code first db initializer.
    /// </summary>
    /// <seealso cref="System.Data.Entity.DropCreateDatabaseAlways{moviefinder.Models.MovieContext}" />
    public class MovieDbInit : DropCreateDatabaseAlways<MovieContext>
    {
        protected override void Seed(MovieContext context)
        {
            Movie m1 = new Movie { movieId = 1, name = "Guardians of the Galaxy", genre = "Action", movieYear = 2017 };
            Movie m2 = new Movie { movieId = 2, name = "Logan", genre = "Action", movieYear = 2017 };
            Movie m3 = new Movie { movieId = 3, name = "Passengers", genre = "Drama", movieYear = 2016 };
            Movie m4 = new Movie { movieId = 4, name = "The Hunger Games", genre = "Fantasy", movieYear = 2012 };
            Movie m5 = new Movie { movieId = 5, name = "La La Land", genre = "Drama", movieYear = 2016 };

            context.Movies.Add(m1);
            context.Movies.Add(m2);
            context.Movies.Add(m3);
            context.Movies.Add(m4);
            context.Movies.Add(m5);
             //someone acted in several films, some films has several actors
            Actor a1 = new Actor { actorId = 1, name = "Chris Pratt", movies = new List<Movie>() { m1, m3 } };
            Actor a2 = new Actor { actorId = 2, name = "Jennifer Lawrence", movies = new List<Movie>() { m3, m4 } };
            Actor a3 = new Actor { actorId = 3, name = "Hugh Jackman", movies = new List<Movie>() { m2 } };
            Actor a4 = new Actor { actorId = 4, name = "Bradley Cooper", movies = new List<Movie>() { m1 } };
            Actor a5 = new Actor { actorId = 5, name = "Ryan Gosling", movies = new List<Movie>() { m5 } };
            Actor a6 = new Actor { actorId = 6, name = "Emma Stone", movies = new List<Movie>() { m5 } };

            context.Actors.Add(a1);
            context.Actors.Add(a2);
            context.Actors.Add(a3);
            context.Actors.Add(a4);
            context.Actors.Add(a5);
            context.Actors.Add(a6);

            base.Seed(context);

        }
    }
}