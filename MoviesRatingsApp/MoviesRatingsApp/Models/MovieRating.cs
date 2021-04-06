using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesRatingsApp.Models
{
    public class MovieRating
    {
        [Key]
        public int Id { get; set; }
        public int MovieID { get; set; }

        [Range(1, 10,
           ErrorMessage = "Rating must be between 1 and 10")]
        public int Rating { get; set; }
    }
}
