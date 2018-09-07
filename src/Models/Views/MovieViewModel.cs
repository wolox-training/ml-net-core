using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MlNetCore.Models;
namespace MlNetCore.Models.Views
{
    public class MovieViewModel
    {
        public MovieViewModel()
        {
        }

        public MovieViewModel(int Id, string Title, DateTime ReleaseDate,
                            decimal Price, string Rating,
                            ICollection<Comment> Comments = null ) {
            this.Id = Id;
            this.Title = Title;
            this.ReleaseDate = ReleaseDate;
            this.Price = Price;
            this.Comments = Comments;
        }
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Release Date"), DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [Required]
        [StringLength(30)]
        public string Genre { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        [StringLength(5)]
        [Required]
        public string Rating { get; set; }

        public string Comment { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
