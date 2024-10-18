using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Movie_Review_Website.Filters;
using ServiceContracts;

namespace Movie_Review_Website.Controllers;

[Authorize]
[Route("[controller]/[action]")]
public class MoviesController : Controller
{
    private readonly ILogger<MoviesController> _logger;
    private readonly IMovieService _movieService;
    private readonly IGenreService _genreService;
    public MoviesController(ILogger<MoviesController> logger, IMovieService movieService, IGenreService genreService)
    {
        _logger = logger;
        _movieService = movieService;
        _genreService = genreService;
    }

    [HttpGet]
    [Route("/")]
    [ValidateSearch]
    public async Task<IActionResult> Index(string? searchString)
    {
        _logger.LogInformation("MoviesController - Index method invoked");

        if (!string.IsNullOrEmpty(searchString))
        {
            _logger.LogDebug($"Search string provided: {searchString}");

            // Only load movies if search string is present
            IEnumerable<Movie> movies = await _movieService.GetAllMoviesAsync();
            Movie? movie = movies.FirstOrDefault(tmp => tmp.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase));

            if (movie == null)
            {
                _logger.LogWarning("No movie found for the search string.");
                return View("NotFound");
            }

            return View("SearchResult", movie);
        }

        // If no search string, return all movies
        IEnumerable<Movie> allMovies = await _movieService.GetAllMoviesAsync();
        _logger.LogDebug("All movies loaded.");
        return View(allMovies);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        _logger.LogInformation($"MoviesController - Edit method invoked with id: {id}");
        Movie? movie = await _movieService.GetMovieByIdAsync(id);
        if (movie == null)
        {
            _logger.LogWarning($"Movie not found: {id}");
            return View("Error");
        }
        _logger.LogInformation($"Movie found: {movie.Title}");
        ViewBag.genres = await _genreService.GetAllGenresAsync();
        ViewBag.errors = new List<string>(); // Initialize ViewBag.errors
        return View(movie);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Movie movie,IFormFile? ImagePath)
    {
        if (ModelState.IsValid)
        {
            if (ImagePath != null && ImagePath.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/images", ImagePath.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImagePath.CopyToAsync(stream);
                }
                movie.ImagePath = "/images/" + ImagePath.FileName;
            }
            try
            {
               await _movieService.UpdateMovieAsync(movie);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(movie.MovieId))
                {
                    return View("NotFound");
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }
    private bool MovieExists(int id)
    {
        return _movieService.GetAllMoviesAsync().Result.Any(e => e.MovieId == id);
    }


    public async Task<IActionResult> Delete(int id)
    {
        _logger.LogWarning($"MoviesController - Delete method invoked with id: {id}");
        Movie? movie = await _movieService.GetMovieByIdAsync(id);
        if (movie == null)
        {
            _logger.LogError($"Movie Not found");
            return View("Error");
        }
        else
        {
            _logger.LogInformation($"Movie Deleted");
            await _movieService.DeleteMovieAsync(id);
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        _logger.LogInformation("Rendering The View");
        ViewBag.genres = await _genreService.GetAllGenresAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(Movie movie, IFormFile? ImagePath)
    {
        if (ModelState.IsValid)
        {
            if (ImagePath != null && ImagePath.Length > 0)
            {
                var filePath = Path.Combine("wwwroot/images", ImagePath.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImagePath.CopyToAsync(stream);
                }
                movie.ImagePath = "/images/" + ImagePath.FileName;
            }

            await _movieService.AddMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.genres = await _genreService.GetAllGenresAsync();
        return View(movie);
    }
}
