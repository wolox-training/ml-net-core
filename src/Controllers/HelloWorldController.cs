using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MlNetCore.Models;
using Microsoft.AspNetCore.Mvc.Localization;
using MlNetCore.Repositories.Interfaces;
using MlNetCore.Models.Views;
using Microsoft.AspNetCore.Authorization;
using System;
using MlNetCore.Models.VO;
using System.Net.Mail;
using System.Net;
using MlNetCore.Mail;

namespace MlNetCore.Controllers
{
    public class HelloWorldController : Controller
    {
        private readonly IHtmlLocalizer<HelloWorldController> _localizer;
        private readonly IUnitOfWork _unitOfWork;
        private IUnitOfWork UnitOfWork { get{ return this._unitOfWork; } }

        public HelloWorldController(IUnitOfWork unitOfWork, IHtmlLocalizer<HelloWorldController> localizer)
        {
            this._unitOfWork = unitOfWork;
            this._localizer = localizer;
        }

        public IActionResult Index(string movieGenre, string searchString, string sortOrder, int? pageIndex)
        {
            ViewData["Title"] = _localizer["MovieList"];
            var movieGenreVM = new MovieGenreViewModel();
            movieGenreVM.TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            movieGenreVM.DateSort = sortOrder == "Date" ? "reldate_desc" : "Date";
            movieGenreVM.GenreSort = sortOrder == "Genre" ? "genre_desc" : "Genre";
            movieGenreVM.CurrentSort = sortOrder;
            movieGenreVM.CurrentFilter = searchString;
            MovieVO movieVO = UnitOfWork.MovieRepository.GetFilteredWithOrder(movieGenre, searchString, sortOrder, pageIndex).Result;
            movieGenreVM.Genres = movieVO.Genres;
            movieGenreVM.Movies = movieVO.Movies;
            return View(movieGenreVM);
        }

        [Authorize]
        public IActionResult Welcome(string name)
        {
            ViewData["Message"] = _localizer["ContactPage"];
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult NewMovie()
        {
            ViewData["Title"] = _localizer["NewMovie"];
            return View(new MovieViewModel());
        }

        [Authorize]
        [HttpPost]
        public IActionResult NewMovie(MovieViewModel viewMovie)
        {
            if(ModelState.IsValid)
            {
                Movie movieModel = new Movie(viewMovie.Title, viewMovie.ReleaseDate, 
                                        viewMovie.Genre, viewMovie.Price, viewMovie.Rating);
                UnitOfWork.MovieRepository.Add(movieModel);
                UnitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(viewMovie);            
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            try
            {
                ViewData["Title"] = _localizer["EditMovie"];
                return View(EditViewSetup(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] MovieViewModel model)
        {
            var movie = UnitOfWork.MovieRepository.Get((int)id);
            if (movie == null)
                return NotFound();
            movie.Genre = model.Genre;
            movie.ReleaseDate = model.ReleaseDate;
            movie.Price = model.Price;
            movie.Title = model.Title;
            movie.Rating = model.Rating;
            UnitOfWork.MovieRepository.Update(movie);
            UnitOfWork.Complete();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                    throw new Exception("Not Found");
                var movie = UnitOfWork.MovieRepository.Get((int)id);
                if (movie == null)
                    throw new Exception("Not Found");
                MovieViewModel model = new MovieViewModel();
                model.Id = (int)id;
                return View(model);
            }
            catch(Exception)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = UnitOfWork.MovieRepository.Get((int)id);
            if (movie == null)
                return NotFound();
            UnitOfWork.MovieRepository.Remove(movie);
            UnitOfWork.Complete();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Details(int? id)
        {
            try
            {
                return View(DetailViewSetup(id));
            }
            catch(Exception)
            {
                return NotFound();
            }
        }

        public IActionResult Send(int? id)
        {
            Mailer.Send("lorant.tester@gmail.com", "subject", "the body");
            return RedirectToAction("Index");
        }

        public MovieViewModel DetailViewSetup(int? id)
        {
            if (id == null)
                throw new Exception("Not Found");
            var movie = UnitOfWork.MovieRepository.GetMovieWithComments((int)id);
            if(movie == null)
                throw new Exception("Not Found");
            ViewData["Title"] = _localizer["DetailsMovie"];
            return new MovieViewModel(movie.Id, movie.Title, movie.ReleaseDate,
                                    movie.Price, movie.Rating, movie.Comments);
        }

        public MovieViewModel EditViewSetup(int? id)
        {
            if (id == null)
                throw new Exception("Not Found");
            var movie = UnitOfWork.MovieRepository.Get((int)id);
            if (movie == null)
                throw new Exception("Not Found");
            ViewData["Title"] = _localizer["EditMovie"];
            return new MovieViewModel(movie.Id, movie.Title, movie.ReleaseDate, 
                                    movie.Price, movie.Rating);
        }

        public IActionResult NewComment(MovieViewModel model)
        {
            Comment comment = new Comment();
            comment.Movie = UnitOfWork.MovieRepository.Get(model.Id);
            comment.Text = model.Comment;
            UnitOfWork.CommentRepository.Add(comment);
            UnitOfWork.Complete();
            return RedirectToAction("Details", new { id = model.Id });
        }
    }
}
