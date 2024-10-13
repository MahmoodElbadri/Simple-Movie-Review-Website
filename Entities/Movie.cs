using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Movie
    {
        public int MovieId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? Title { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 3)]
        public string? Description { get; set; }
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
        public float AverageRating { get; set; }
        public List<Review>? Reviews { get; set; }
        public override string ToString()
        {
            return $"{Title}";
        }
    }
}
