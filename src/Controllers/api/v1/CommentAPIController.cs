using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MlNetCore.Models;


namespace MlNetCore.Controllers.api.v1
{
    [Route("api/v1/CommentApiController")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    /*
        public IEnumerable<Comment> getComment()
        {
            
        }*/
    }
}
