using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using AutoMapper;
using Vidly.DTO;
using System.Data.Entity;

namespace Vidly.Controllers.API
{
    public class MoviesController : ApiController
    {
        ApplicationDbContext _context;

        public MoviesController() {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public IHttpActionResult GetMovies(string query = null)
        {
            var moviesQ = _context.Movies.Include(m => m.Genre);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQ = moviesQ.Where(m => m.Name.Contains(query));

            moviesQ = moviesQ.Where(m => m.NumberAvailable > 0);

            var moviesDTO = moviesQ.ToList().Select(Mapper.Map<Movie, MoviesDTO>);

            return Ok(moviesDTO);
        }

        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie(MoviesDTO movieDTO)
        {
            movieDTO.DateAdded = DateTime.Now;

            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MoviesDTO, Movie>(movieDTO);

            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDTO.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDTO);
        }

        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateMovie(int id, MoviesDTO movieDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            Mapper.Map(movieDTO, movie);

            _context.SaveChanges();

            return Ok(movieDTO);
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovie(int id)
        {
            var DbMovie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (DbMovie == null)
                return NotFound();

            _context.Movies.Remove(DbMovie);

            _context.SaveChanges();

            return Ok();
        }

    }
}
