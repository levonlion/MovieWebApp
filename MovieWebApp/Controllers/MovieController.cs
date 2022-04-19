using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieWebApp.Data;
using MovieWebApp.Entities;
using MovieWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace MovieWebApp.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        private ApplicationDbContext _context;

        private string CurrentUserId;
        public MovieController(ApplicationDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            CurrentUserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Movies([FromQuery]short orderBy = (short)OrderBy.OrderName)
        {
            var movies = GetMovies((OrderBy)orderBy);
            return View(movies);
        }

        public IActionResult Movie(int id)
        {
            var movie = GetMovie(id);
            return View(movie);
        }

        public IActionResult Actors()
        {
            var actors = GetActors();
            return View(actors);
        }

        public IActionResult Actor(int id)
        {
            var actor = GetActor(id);
            return View(actor);
        }

        [HttpPost]
        public IActionResult LikeMovie([FromForm]int movieId)
        {
            var isExistingMovie = _context.Movies.Any(s => s.Id == movieId);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var likedMovie = _context.MovieRatings.FirstOrDefault(s => s.MovieId == movieId && s.UserId == userId);

            if (userId != null & isExistingMovie)
            {
                if (likedMovie == null)
                {
                    _context.MovieRatings.Add(new MovieRating
                    {
                        MovieId = movieId,
                        UserId = userId
                    });
                }
                else
                {
                    _context.MovieRatings.Remove(likedMovie);
                }
                _context.SaveChanges();

            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public IActionResult LikeActor(int actorId)
        {
            var isExistingActor = _context.Actors.Any(s => s.Id == actorId);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var likedActor = _context.ActorRatings.FirstOrDefault(s => s.ActorId == actorId && s.UserId == userId);

            if (userId != null & isExistingActor)
            {
                if (likedActor == null)
                {
                    _context.ActorRatings.Add(new ActorRating
                    {
                        ActorId = actorId,
                        UserId = userId
                    });
                }
                else
                {
                    _context.ActorRatings.Remove(likedActor);
                }
                _context.SaveChanges();
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IEnumerable<MovieModel> GetMovies(OrderBy orderBy)
        {
            var dbContext = _context.Movies.Include(s => s.MovieRatings);

            IQueryable<Movie> movies = null;
            switch (orderBy)
            {
                case OrderBy.OrderDate:
                    movies = dbContext.OrderBy(s => s.Date);
                    break;
                case OrderBy.OrderName:
                    movies = dbContext.OrderBy(s => s.Name);
                    break;
                case OrderBy.OrderRating:
                    movies = dbContext.OrderByDescending(s => s.MovieRatings.Count);
                    break;
                default:
                    throw new Exception("Not found how to order");
            }
            var mT = movies?.ToList();

            return mT?.Select(s=>new MovieModel { 
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Date = s.Date,
                IsLiked = s.MovieRatings.Any(s=>s.UserId == CurrentUserId),
                LikeCount = s.MovieRatings.Count(),
            });
        }

        public IEnumerable<ActorModel> GetActors()
        {
            var actors = _context.Actors.Include(s=>s.ActorRatings).OrderByDescending(s => s.ActorRatings.Count).ToList();
            return actors?.Select(s => new ActorModel
            {
                Id = s.Id,
                Name = s.Name,
                IsLiked = s.ActorRatings.Any(s => s.UserId == CurrentUserId),
                LikeCount = s.ActorRatings.Count()
            });
        }

        public ActorModel GetActor(int id)
        {
            var actor =  _context.Actors.Include(s => s.ActorMovies).ThenInclude(s => s.Movie).Include(s => s.ActorRatings).SingleOrDefault(s => s.Id == id);
            return new ActorModel
            {
                Id = actor.Id,
                Name = actor.Name,
                Movies = actor.ActorMovies.Select(s => new MovieModel { Id = s.Movie.Id, Name = s.Movie.Name }).ToList(),
                IsLiked = actor.ActorRatings.Any(s => s.UserId == CurrentUserId),
                LikeCount = actor.ActorRatings.Count()
            };
        }

        public MovieModel GetMovie(int id)
        {
            var movie =  _context.Movies.Include(s => s.ActorMovies).ThenInclude(s => s.Actor).Include(s => s.MovieRatings).SingleOrDefault(s => s.Id == id);
            return new MovieModel
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                Date = movie.Date,
                Actors = movie.ActorMovies.Select(s => new ActorModel {Id = s.Id,Name = s.Actor.Name }).ToList(),
                IsLiked = movie.MovieRatings.Any(s => s.UserId == CurrentUserId),
                LikeCount = movie.MovieRatings.Count()
            };
        }
    }
}
