// ***********************************************************************
// Assembly         : smartmoviefinder
// Author           : hanguksaram
// Created          : 05-20-2017
//
// Last Modified By : hanguksaram
// Last Modified On : 05-22-2017
// ***********************************************************************
// <copyright file="HomeController.cs" company="">
//     © , 2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using moviefinder.Models;
using System.Data.Linq;

namespace moviefinder.Controllers
{

    //TODO: FIX design view, Site.css and bootstrap dosnt implement expected design of index's view, but it worked earlier
    //bundle config and _Layout have right links.
    /// <summary>
    /// modern looking single page application searching movies from database "Smart Movie's Finder".
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class HomeController : Controller
    {   
        IQueryable<Movie> movis;
        IQueryable<Actor> actors;
        IRepository repo;
        IList<Movie> movs;

        /// <summary>
        /// Initializes database.
        /// </summary>
        public HomeController()
        {
            repo = new DataRepository();
            //disable proxy creation and lazy loading to prevent circular reference during json sereal
            repo.CheckProxy(false);
        }
        /// <summary>
        /// release resources(close db connection) to improve data perfomance
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            repo.Dispose();
            base.Dispose(disposing);
        }


        /// <summary>
        /// Method Searching by movie's release date.
        /// </summary>
        /// <param name="year">Release year inputted from user.</param>
        /// <param name="movie">Movie's list from db.</param>
        /// <returns>Movies ordered list;.</returns>
        public IList<Movie> FinderByDate(int year, IQueryable<Movie> movie)//using public modifier to get access from testing classes
        {
            return movie.Where(m => m.movieYear == year)
                        .ToList();
        }
        /// <summary>
        /// Method searching by Actor, Title, Genre.
        /// incoming data could be part or full NAME or/and LASTNAME of actor, part or full movie's TITLE,  GENRE
        /// </summary>
        /// <param name="id">Data inputted from client side.</param>
        /// <param name="movie">movie list from db</param>
        /// <param name="actor">actor list from db</param>
        /// <returns>ordered Movie's list</returns>
        public IList<Movie> FinderByActorNameGenre(string id, IQueryable<Movie> movie, IQueryable<Actor> actor)
        {
            var movies = movie.Where(m => m.genre == id
                       || m.name.Contains(id))
                       .ToList(); //immediate execution of query

            //searching by actor's name
            return movies.Count == 0 ? movie.Where(m =>
                    m.actors.All(a =>
                        m.actors.Any(an =>
                            an.name.Contains(id))))
                            .ToList() : movies;
        }
        public ActionResult Index()
        {
            return View("Index");
        }

        /// <summary>
        /// main method calling from asynhronous AJAX's requests.
        /// </summary>
        /// <param name="id">input value sending from view form.</param>
        /// <returns>lists of movies in json form ordered by data request or empty list.</returns>
        public ActionResult JsonMovieData(string id)
        {
            movis = repo.GetMovies();
            actors = repo.GetActors();
                        
            if (!String.IsNullOrEmpty(id))
            {
                //searching movies by Year, maybe this is kostbl/l D=, but i couldnt figure out better solution besides this one =D
                try
                {
                    int year = int.Parse(id);
                    movs = FinderByDate(year, movis);
                    //movs = movis.Where(m => m.movieYear == year)
                        //.ToList();
                }

                catch (FormatException e)
                {
                    movs = FinderByActorNameGenre(id, movis, actors);
                    
                }

                //TODO: implement immediate query execution before serialize data to json, DONE
            }
            return Json(movs, JsonRequestBehavior.AllowGet);//allow json to response on GET requests
            //TODO: avoid a circular reference while serializing movis to json, DONE
        }
    }
}