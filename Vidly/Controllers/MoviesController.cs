using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();    
        }

        //[Route("Movies")]
        public ActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
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


        public ViewResult New()
        {
            var genres = _context.Genres.ToList();

            var viewmodel = new MovieFormViewModel
            {
                Genres = genres
            };

            return View("MovieForm", viewmodel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewmodel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewmodel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewmodel);
            }
            if (movie.Id == 0) //Se crea una nueva pelicula
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else  // Se actualiza los datos de una pelicula existente
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
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