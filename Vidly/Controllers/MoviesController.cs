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
    [RoutePrefix("Movies")]
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


        [Route]
        public ActionResult Index() {
            if(User.IsInRole(RoleName.CanManageMovies))
                return View("MoviesView");

            return View("ReadOnlyMoviesView");
        }

        [Route("Details/{id}")]
        public ActionResult Details(int id) {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            return View("MovieDetailsView", movie);

        }

        [Route("New")]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };


            return View("MovieFormView", viewModel);
        }

        [Route("Edit/{id}")]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).Single(m => m.Id == id);

            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = genres
            };

            return View("MovieFormView", viewModel);
        }

        [HttpPost]
        [Route("Save")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };

                return View("MovieFormView", viewModel);
            }


            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var DbMovie = _context.Movies.Single(m => m.Id == movie.Id);
                DbMovie.Name = movie.Name;
                DbMovie.NumberInStock = movie.NumberInStock;
                DbMovie.GenreId = movie.GenreId;
                DbMovie.DateReleased = movie.DateReleased;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        /*public ActionResult Random()
        {
            Movie movie = new Movie() { Name = "Shrek" };
            List<Customer> customers = new List<Customer> {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" }
            };

            RandomMovieViewModel viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }*/

        /*[Route("movies/released/{year}/{month:regex(\\d{4}):range(1, 12)}")]
        public ActionResult ByReleaseYear(int year, int month)
        {
            return Content(year + "/" + month);
        }*/

        /*public ActionResult Edit(int id)
        {
            return Content("id=" + id);
        }

        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (pageIndex.HasValue)
                pageIndex = 1;
            if (String.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";

            return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        }*/
    }
}