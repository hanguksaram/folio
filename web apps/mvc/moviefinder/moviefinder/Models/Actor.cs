using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace moviefinder.Models
{
    /// <summary>
    /// Dbase actor entity.
    /// </summary>
    public class Actor
    {   //init enteties state
        public int actorId { get; set; }
        public string name { get; set; }
        

        //realization many-to-many entity's relationship actor-movies 
        public virtual ICollection<Movie> movies { get; set; }

        public Actor()
        {
            movies = new List<Movie>();
        }
    }
}