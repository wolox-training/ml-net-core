using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MlNetCore.Models;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Localization;

namespace MlNetCore.Controllers
{
    public class HelloWorldController : Controller
    {
        private readonly IHtmlLocalizer<HelloWorldController> _localizer;
        
        public HelloWorldController(IHtmlLocalizer<HelloWorldController> localizer)
        {
            this._localizer = localizer;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome(string name)
        {
            ViewData["Message"] = _localizer["ContactPage"];
            return View();
        }
    }
}
