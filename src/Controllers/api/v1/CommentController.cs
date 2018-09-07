using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MlNetCore.Models;
using MlNetCore.Models.Views;
using MlNetCore.Repositories.Interfaces;

namespace MlNetCore.Controllers.api.v1
{
    [Route("api/v1/[controller]")]
    public class CommentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private IUnitOfWork UnitOfWork { get { return this._unitOfWork; } }

        public CommentController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpPost("NewComment")]
        public JsonResult NewComment(int movieId, string viewComment)
        {
            Comment comment = new Comment();
            comment.Movie = UnitOfWork.MovieRepository.Get(movieId);
            comment.Text = viewComment;
            UnitOfWork.CommentRepository.Add(comment);
            UnitOfWork.Complete();
            return  new JsonResult(new {result = true});
        }

        [HttpGet("ListComments/{id}")]
        public JsonResult ListComments(int id)
        {
            return Json(UnitOfWork.CommentRepository.GetAllCommentsOfMovie(id));
        }
    }
}
