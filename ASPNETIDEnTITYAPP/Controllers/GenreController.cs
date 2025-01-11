using ASPNETIDEnTITYAPP.Areas.Identity.Data;
using ASPNETIDEnTITYAPP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ASPNETIDEnTITYAPP.Controllers
{
    [Authorize] // Ensure only logged-in users can access
    public class GenreController : Controller
    {
        private readonly DBContextSample _context;

        public GenreController(DBContextSample context)
        {
            _context = context;
        }

        // Display the list of genres for the user to select
        public IActionResult SelectGenres()
        {
            var genres = _context.Genres.ToList(); // Fetch all genres from the database
            return View(genres); // Pass genres to the view
        }

        // Save the genres selected by the user
        [HttpPost]
        public IActionResult SaveUserGenres(List<int> selectedGenreIds)
        {
            // Get the logged-in user's ID (retrieved as a string)
            var userId = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

            if (userId == null)
            {
                return Unauthorized(); // Return an unauthorized status if user ID is not found
            }

            // Remove existing genres for the user
            var existingUserGenres = _context.UserGenres.Where(ug => ug.UserId == userId);
            _context.UserGenres.RemoveRange(existingUserGenres);

            // Add the new genres selected by the user
            foreach (var genreId in selectedGenreIds)
            {
                _context.UserGenres.Add(new UserGenre
                {
                    UserId = userId, // Use the string UserId directly
                    GenreId = genreId
                });
            }

            _context.SaveChanges(); // Save changes to the database
            return RedirectToAction("Index", "Home"); // Redirect to the home page or another appropriate page
        }
    }
}
