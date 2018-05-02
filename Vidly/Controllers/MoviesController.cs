using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        [Route("Movies")]
        public ActionResult Index()
        {
            var movies = GetMovies();

            return View(movies);
        }

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>()
            {
                new Movie { Id = 1, Name ="Shrek" },
                new Movie { Id = 2, Name = "Wall-e" }
            };
        }
     


        // GET: Movies/Random
        public ActionResult Random()
        {
            //var movie = new List<Movie>() { new Movie { Name = "Shrek!" } };
            //var customers = new List<Customer>
            //{
            //    new Customer { Name = "Customer 1" },
            //    new Customer { Name = "Customer 2" },
            //};

            //var viewModel = new RandomMovieViewModel
            //{
            //    Movies = movie,
            //    Customers = customers
            //};
            ////ViewData["Movie"] = movie;
            ////ViewBag.Movie = movie;

            //return View(viewModel);
            return View();
        }

        public ActionResult Edit(int id)
        {
            return Content("id = " + id);
        }

        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    Si pageIndex NO tiene un valor   
        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;

        //    if (String.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "Name";

        //    Returna un simple contenido with pageIndex = 1st parameter & sortBy = 2st parameter;
        //    return Content(String.Format("pageIndex = {0} & sortBy = {1}", pageIndex, sortBy));
        //}

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate( int year, int month)
        {
            return Content(year + "/" + month);
        }
    }
}