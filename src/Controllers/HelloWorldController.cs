using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MlNetCore.Models;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Localization;
using MlNetCore.Repositories.Database;
using MlNetCore.Repositories.Interfaces;
using MlNetCore.Models.Views;

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

        public IActionResult Welcome(string name)
        {
            ViewData["Message"] = _localizer["ContactPage"];
            return View();
        }

        public IActionResult NewMovie()
        {
            ViewData["Title"] = _localizer["NewMovie"];
            return View(new MovieViewModel());
        }
        public IActionResult CreateNewMovie(MovieViewModel viewMovie)
        {
            Movie movieModel = new Movie(viewMovie.Id, viewMovie.Title, viewMovie.ReleaseDate, 
                                        viewMovie.Genre, viewMovie.Price);
            UnitOfWork.MovieRepository.Add(movieModel);
            UnitOfWork.Complete();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var movie = UnitOfWork.MovieRepository.Get((int)id);
            if (movie == null)
                return NotFound();
            ViewData["Title"] = _localizer["EditMovie"];
            MovieViewModel model = new MovieViewModel();
            model.Id = movie.Id;
            model.Genre = movie.Genre;
            model.Price = movie.Price;
            model.Title = movie.Title;
            movie.ReleaseDate = movie.ReleaseDate;
            return View(model);
        }

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
