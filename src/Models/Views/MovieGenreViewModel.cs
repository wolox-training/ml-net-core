using Microsoft.AspNetCore.Mvc.Rendering;
using MlNetCore.Models;
using MlNetCore.Repositories;
using System.Collections.Generic;

namespace MlNetCore.Models.Views
{
    public class MovieGenreViewModel
    {
        public PaginatedList<Movie> Movies;
        public SelectList Genres;
        public string MovieGenre { get; set; }

        public string TitleSort { get; set; }
        public string DateSort { get; set; }
        public string GenreSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
    }
}
