using Microsoft.AspNetCore.Mvc.Rendering;
using MlNetCore.Models;
using System.Collections.Generic;

namespace MlNetCore.Models.Views
{
    public class MovieGenreViewModel
    {
        public List<Movie> movies;
        public SelectList genres;
        public string movieGenre { get; set; }
    }
}