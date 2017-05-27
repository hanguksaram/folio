using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace moviefinder.Models
{
    public interface IRepository: IDisposable
    {
        IQueryable<Actor> GetActors();
        IQueryable<Movie> GetMovies();
        void CheckProxy(bool state); 
    }
    /// <summary>
    /// Pattern Repository implementation to enable model tests.
    /// </summary>
    /// <seealso cref="moviefinder.Models.IRepository" />
    public class DataRepository: IRepository
    {
        /// <summary>
        /// The database
        /// </summary>
        private MovieContext db;
        public void CheckProxy(bool state)
        {
            db.Configuration.ProxyCreationEnabled = state;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DataRepository"/> class.
        /// </summary>
        public DataRepository()
        {
            this.db = new MovieContext();
        }

        /// <summary>
        /// Gets the actors.
        /// </summary>
        /// <returns>List&lt;Actor&gt;.</returns>
        public IQueryable<Actor> GetActors()
        {
            return from a in db.Actors
                   select a;
        }

        /// <summary>
        /// Gets the movies.
        /// </summary>
        /// <returns>List&lt;Movie&gt;.</returns>
        public IQueryable<Movie> GetMovies()
        {
            return from m in db.Movies
                   select m;
        }

        /// <summary>
        /// The disposed
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}