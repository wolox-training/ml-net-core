using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MlNetCore.Repositories;

namespace MlNetCore.Models.VO
{
    public class MovieVO
    {
        public SelectList Genres
        {
            get; set;
        }

        public PaginatedList<Movie> Movies
        {
            get; set;
        }
    }
}
