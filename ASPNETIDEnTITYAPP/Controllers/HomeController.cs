using ASPNETIDEnTITYAPP.Areas.Identity.Data;
using ASPNETIDEnTITYAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASPNETIDEnTITYAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DBContextSample _context;

        public HomeController(ILogger<HomeController> logger, DBContextSample context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            /*
            // Get the logged-in user's ID
            var userId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            // Fetch all movie lists created by the user
            var movieLists = _context.MovieLists
                .Where(ml => ml.UserId == userId)
                .ToList();
            */
            return View();
        }
        public async Task<IActionResult> MovieListHome()
        {
            // Get the logged-in user's ID
            //var userId = User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            // Fetch all movie lists created by the user
            var movieList = await _context.MovieLists
                .Where(ml => ml.UserId == userId)
                .Select(ml => ml.Id) // Sadece MovieList.Id deðerlerini al
                .ToListAsync();
            int movieId = movieList[0];
            var movieLists = await _context.MovieInLists
                .Where(mil => mil.MovieListId == movieId) // MovieListId eþleþmesi
                .ToListAsync();

            return View(movieLists);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
