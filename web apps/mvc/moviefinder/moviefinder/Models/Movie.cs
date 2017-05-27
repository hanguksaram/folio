using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace moviefinder.Models
{
    /// <summary>
    /// Dbase movie entity.
    /// </summary>
    public class Movie
    { //init enteties state
        public int movieId { get; set; }
        public string name { get; set; }
        public string genre { get; set; }
        public int movieYear { get; set; }

        //realization many-to-many entity's relationship movie - actors
        public virtual ICollection<Actor> actors { get; set; }

        public Movie()
        {
            actors = new List<Actor>();
        }
    }
}