using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities;

public class MovieWithReviewsAndGenreDto
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string GenreName { get; set; }
    public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>(); // List of reviews
    public float? Rating { get; set; }
}

public class ReviewDto
{
    public int ReviewId { get; set; }
    public string UserId { get; set; }
    public float? Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}
