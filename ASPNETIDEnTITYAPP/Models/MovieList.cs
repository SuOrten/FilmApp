using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPNETIDEnTITYAPP.Models
{
    public class MovieList
    {
        public int Id { get; set; } // Primary key

        [Required]
        public string Name { get; set; } // Name of the list

        public string UserId { get; set; } // User who owns the list

        // Navigation property for the movies in this list
        public List<MovieInList> Movies { get; set; }
    }

    public class MovieInList
    {
        public int Id { get; set; } // Primary key

        public int MovieListId { get; set; } // FK to MovieList
        public MovieList MovieList { get; set; } // Navigation property

        public int MovieId { get; set; } // The movie's ID
        public string Title { get; set; }
        public string? PosterPath { get; set; } = string.Empty;
        public double VoteAverage { get; set; }
    }
}
