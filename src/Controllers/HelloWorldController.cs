using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MlNetCore.Models;
using Microsoft.AspNetCore.Mvc.Localization;
using MlNetCore.Repositories.Interfaces;
using MlNetCore.Models.Views;
using Microsoft.AspNetCore.Authorization;
using System;
using MlNetCore.Models.VO;

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

        public IActionResult Index(string movieGenre, string searchString)
        {
            ViewData["Title"] = _localizer["MovieList"];
            var movieGenreVM = new MovieGenreViewModel();
            MovieVO movieVO = UnitOfWork.MovieRepository.GetFiltered(movieGenre, searchString).Result;
            movieGenreVM.genres = movieVO.Genres;
            movieGenreVM.movies = movieVO.Movies;
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
                return View(ViewModelSetup(id));
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
            SmtpClient client = new SmtpClient("mysmtpserver");
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("username", "password");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("lorant.mikolas@wolox.com.ar");
            mailMessage.To.Add("lorant.mikolas@wolox.com.ar");
            mailMessage.Body = "body";
            mailMessage.Subject = "subject";
            client.Send(mailMessage);
            try
            {
                return View(ViewModelSetup(id));
            }
            catch(Exception)
            {
                return NotFound();
            }
        }

        public MovieViewModel ViewModelSetup(int? id)
        {
            if (id == null)
                throw new Exception("Not Found");
            var movie = UnitOfWork.MovieRepository.Get((int)id);
            if (movie == null)
                throw new Exception("Not Found");
            ViewData["Title"] = _localizer["DetailsMovie"];
            MovieViewModel model = new MovieViewModel();
            model.Id = movie.Id;
            model.Title = movie.Title;
            model.Genre = movie.Genre;
            model.ReleaseDate = movie.ReleaseDate;
            model.Price = movie.Price;
            model.Rating = movie.Rating;
            return model;
        }
    }
}
