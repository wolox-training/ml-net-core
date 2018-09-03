using System;

namespace MlNetCore.Models
{
    public class Movie
    {
        
        public Movie(int id, string title, DateTime releaseDate, string genre, decimal price, string rating)
        {
            this.Id = id;
            this.Title = title;
            this.ReleaseDate = releaseDate;
            this.Genre = genre;
            this.Price = price;
            this.Rating = rating;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }

        public string Rating { get; set; }
    }
}
