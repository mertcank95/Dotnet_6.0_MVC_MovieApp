using System.ComponentModel.DataAnnotations;

namespace MovieApp.Models
{
    public class MovieGenre
    {
        [Key]
        public int MovieGenreId { get; set; }
        public string MovieGenreName { get; set; }

    }
}
