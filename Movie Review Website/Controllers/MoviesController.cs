using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceContracts;

namespace Movie_Review_Website.Controllers
{
    //[Authorize]
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
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("MoviesController - Index method invoked");
            IEnumerable<Movie> movies = await _movieService.GetAllMoviesAsync();
            return View(movies);
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
        public async Task<IActionResult> Edit(Movie movie)
        {
            _logger.LogInformation($"MoviesController - Edit method invoked with id: {movie.MovieId}");
            if (ModelState.IsValid)
            {
                _logger.LogInformation($"Movie updated: {movie.Title}");
                await _movieService.UpdateMovieAsync(movie);
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogWarning($"Movie not updated: {movie.Title}");
                ViewBag.genres = await _genreService.GetAllGenresAsync();
                ViewBag.errors = ModelState.Values.SelectMany(tmp => tmp.Errors).Select(tmp => tmp.ErrorMessage).ToList();
            }
            return View(movie);
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
        public async Task<IActionResult> Add(Movie movie)
        {
            if (movie.Title.IsNullOrEmpty() || !ModelState.IsValid)
            {
                _logger.LogError("One or More validation error occurs");
                ViewBag.genres = await _genreService.GetAllGenresAsync();
                return View(movie);
            }
            _logger.LogInformation($"Adding a movie with title {movie.Title}");
            await _movieService.AddMovieAsync(movie);
            return RedirectToAction("Index");
        }
    }
}
