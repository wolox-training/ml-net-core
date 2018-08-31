using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MlNetCore.Models;
using Microsoft.AspNetCore.Mvc.Localization;
using MlNetCore.Repositories.Interfaces;
using MlNetCore.Models.Views;
using Microsoft.AspNetCore.Authorization;
using System;

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

        public IActionResult Index()
        {
            ViewData["Title"] = _localizer["MovieList"];
            return View(UnitOfWork.MovieRepository.GetAll().ToList());
        }

        [Authorize]
        public IActionResult Welcome(string name)
        {
            ViewData["Message"] = _localizer["ContactPage"];
            return View();
        }

        [Authorize]
        public IActionResult NewMovie()
        {
            ViewData["Title"] = _localizer["NewMovie"];
            return View(new MovieViewModel());
        }

        [Authorize]
        public IActionResult CreateNewMovie(MovieViewModel viewMovie)
        {
            Movie movieModel = new Movie(viewMovie.Id, viewMovie.Title, viewMovie.ReleaseDate, 
                                        viewMovie.Genre, viewMovie.Price);
            UnitOfWork.MovieRepository.Add(movieModel);
            UnitOfWork.Complete();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            try 
            {
                if (id == null)
                    throw new Exception("Not Found"); 
                var movie = UnitOfWork.MovieRepository.Get((int)id);
                if (movie == null)
                    throw new Exception("Not Found");
                ViewData["Title"] = _localizer["EditMovie"];
                MovieViewModel model = new MovieViewModel();
                model.Id = movie.Id;
                model.Genre = movie.Genre;
                model.Price = movie.Price;
                model.Title = movie.Title;
                movie.ReleaseDate = movie.ReleaseDate;
                return View(model);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] MovieViewModel model)
        {
            var movie = UnitOfWork.MovieRepository.Get((int)id);
            if (movie == null)
                return NotFound();
            movie.Genre = model.Genre;
            movie.ReleaseDate = model.ReleaseDate;
            movie.Price = model.Price;
            movie.Title = model.Title;
            UnitOfWork.MovieRepository.Update(movie);
            UnitOfWork.Complete();
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var movie = UnitOfWork.MovieRepository.Get((int)id);
            if (movie == null)
                return NotFound();
            MovieViewModel model = new MovieViewModel();
            model.Id = (int) id;
            return View(model);
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
            if(id == null)
                return NotFound();
            var movie = UnitOfWork.MovieRepository.Get((int) id);
            if (movie == null)
                return NotFound();
            ViewData["Title"] = _localizer["DetailsMovie"];
            MovieViewModel model = new MovieViewModel();
            model.Title = movie.Title;
            model.Genre = movie.Genre;
            model.ReleaseDate = movie.ReleaseDate;
            model.Price = movie.Price;
            return View(model);
        }
    }
}
