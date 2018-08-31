using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MlNetCore.Models;
using MlNetCore.Models.VO;
using MlNetCore.Repositories.Database;
using MlNetCore.Repositories.Interfaces;

namespace MlNetCore.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(DbContext context) : base(context)
        {
        }

        public DataBaseContext MovieContext
        {
            get { return Context as DataBaseContext; }
        }

        public async Task<MovieVO> GetFiltered(string movieGenre, string searchString)
        {
            IQueryable<string> genreQuery = from m in MovieContext.Movies
                                            orderby m.Genre
                                            select m.Genre;
            var movies = from m in MovieContext.Movies
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }
            MovieVO vo = new MovieVO();
            vo.Movies = await movies.ToListAsync();
            vo.Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            return vo;
        }
    }
}
