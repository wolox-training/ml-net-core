using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MlNetCore.Models.VO
{
    public class MovieVO
    {
        public SelectList Genres
        {
            get; set;
        }

        public List<Movie> Movies
        {
            get; set;
        }
    }
}
