using Microsoft.AspNetCore.Mvc;
using ASPNETIDEnTITYAPP.Models;
using System.Threading.Tasks;
using ASPNETIDEnTITYAPP.Areas.Identity.Data;

public class GenreController : Controller
{
    private readonly GenreService _genreService;
    private readonly MovieService _movieService; // Add this line
    private readonly DBContextSample _context;

   
    public GenreController(GenreService genreService, MovieService movieService, DBContextSample dBContextSample)
    {
        _genreService = genreService;
        _movieService = movieService; // Assign the MovieService here
        _context = dBContextSample;
    }

    [HttpGet]
    public async Task<IActionResult> SelectGenres()
    {
        var genres = await _genreService.GetGenresAsync();
        return View(genres);
    }

    [HttpGet]
    public async Task<IActionResult> ShowMovies(List<int> genreIds)
    {
        var movies = await _movieService.GetMoviesByGenresAsync(genreIds);
        return View(movies);
    }


    [HttpPost]
    public IActionResult SaveUserGenres(List<int> selectedGenreIds)
    {
        // Save user-selected genres in the database or session
        // TODO: Implement database logic to store the user's selected genres

        // Redirect to movie display after genres are saved
        return RedirectToAction("ShowMovies");
    }


    [HttpPost]
    public async Task<IActionResult> SaveMoviesToList(List<int> selectedMovieIds)
    {
        // Get the logged-in user's ID
        //var userId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        var userId = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        if (userId == null)
        {
            return Unauthorized(); // Return unauthorized if the user is not logged in
        }

        // Create a new movie list
        var movieList = new MovieList
        {
            Name = "My Movie List", // You can customize this name or let the user provide it
            UserId = userId,
            Movies = new List<MovieInList>()
        };

        // Fetch movie details from TMDb API and add them to the list
        foreach (var movieId in selectedMovieIds)
        {
            // Ideally, fetch the movie details from the MovieService
            var movie = await _movieService.GetMovieDetailsAsync(movieId); // Add a method to fetch movie details

            movieList.Movies.Add(new MovieInList
            {
                MovieId = movie.Id,
                Title = movie.Title,
                PosterPath = movie.PosterPath ?? "Undefined",
                VoteAverage = movie.VoteAverage
            });
        }

        // Save the movie list to the database
        _context.MovieLists.Add(movieList);
        await _context.SaveChangesAsync();

        // Redirect to the home page
        return RedirectToAction("MovieListHome", "Home");
    }


}
