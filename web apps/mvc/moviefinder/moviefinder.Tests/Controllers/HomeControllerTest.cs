using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using moviefinder;
using moviefinder.Controllers;
using moviefinder.Models;

//moq or entity framework libraries r required to emulate model
namespace moviefinder.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController controller;
        private ViewResult result;

        [TestInitialize]
        public void SetupContext()
        {
            controller = new HomeController();
            result = controller.Index() as ViewResult;
        }

        //test: is Model existed
        [TestMethod]
        public void IndexViewResultNotNull()
        {
           
            Assert.IsNotNull(result.Model);

        }
        //test: sending right view from action index
        [TestMethod]
        public void IndexViewEqualIndexCshtml()
        {

            Assert.AreEqual("Index", result.ViewName);
        }


    }

    [TestClass]
    public class SearchingMethodTest
    {
        private HomeController controller;
        IList<Movie> result;

        [TestInitialize]
        public void SetupContext()
        {
            controller = new HomeController();
            string title = "logan";
            DataRepository db = new DataRepository();
            var movies = db.GetMovies();
            var actors = db.GetActors();
            int DateRelease = 2017;
            string actorname = "jennifer lawrence";
            
            //replace params to choose which method should be tested: title/DataRelease/actorname
            result = controller.FinderByActorNameGenre(title, movies, actors);
        }

        [TestMethod]
        public void TestingSearchingByTitle()
        {
            Assert.AreEqual(1, result.Count);//there s only one "logan" movie in db, so list.count = 1
        }

        [TestMethod]
        public void TestingSearchingByDate()
        {
            Assert.AreEqual(2, result.Count);// two movies were released in 2017
        }

        [TestMethod]
        public void TestingSearchingByActor() // jennifer lawrence took part in two films: passengers and the hunger games
        {
            Assert.AreEqual(2, result.Count);
        }

    }
        

}
