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
        private IUnitOfWork _unitOfWork { get; }
        public HelloWorldController(IUnitOfWork unitOfWork, IHtmlLocalizer<HelloWorldController> localizer)
        {
            this._unitOfWork = unitOfWork;
            this._localizer = localizer;
        }

        public IActionResult Index()
        {
            return View(this._unitOfWork.MovieRepository.GetAll().ToList());
        }

        public IActionResult Welcome(string name)
        {
            ViewData["Message"] = _localizer["ContactPage"];
            return View();
        }

        public IActionResult NewMovie()
        {
            return View(new MovieViewModel());
        }
        public IActionResult CreateNewMovie(MovieViewModel viewMovie)
        {
            Movie movieModel = new Movie(viewMovie.Id, viewMovie.Title, viewMovie.ReleaseDate, 
                                        viewMovie.Genre, viewMovie.Price);
            this._unitOfWork.MovieRepository.Add(movieModel);
            this._unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}
