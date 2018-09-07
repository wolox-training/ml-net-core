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
        public MovieRepository(DataBaseContext context) : base(context)
        {
        }

        public DataBaseContext MovieContext
        {
            get { return Context as DataBaseContext; }
        }

        public async Task<MovieVO> GetFilteredWithOrder(string movieGenre, 
                                    string searchString, string sortOrder, int? pageIndex)
        {
            IQueryable<string> genreQuery = from m in MovieContext.Movies
                                            orderby m.Genre
                                            select m.Genre;
            var movies = from m in MovieContext.Movies
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                pageIndex = 1;
                movies = movies.Where(s => s.Title.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }
            switch (sortOrder)
            {
                case "title_desc":
                    movies = movies.OrderByDescending(s => s.Title);
                    break;
                case "reldate_desc":
                    movies = movies.OrderByDescending(s => s.ReleaseDate);
                    break;
                case "genre_desc":
                    movies = movies.OrderByDescending(s => s.Genre);
                    break;
                case "Date":
                    movies = movies.OrderBy(s => s.ReleaseDate);
                    break;
                case "Genre":
                    movies = movies.OrderBy(s => s.Genre);
                    break;
                default:
                    movies = movies.OrderBy(s => s.Title);
                    break;
            }
            MovieVO vo = new MovieVO();
            int pageSize = 4;
            vo.Movies = await PaginatedList<Movie>.CreateAsync (
                 movies.AsNoTracking(), pageIndex ?? 1, pageSize); 
            vo.Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            return vo;
        }

        public Movie GetMovieWithComments(int id) {
            return Context.Movies.Where(m => m.Id == id).Include(m => m.Comments).FirstOrDefault();
        }


        public void RemoveMovieCascade(Movie movie)
        {
            Context.Set<Comment>().RemoveRange(movie.Comments);
            Remove(movie);
        }
    }
}
