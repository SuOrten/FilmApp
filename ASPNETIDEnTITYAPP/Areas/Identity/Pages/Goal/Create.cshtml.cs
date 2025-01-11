using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ASPNETIDEnTITYAPP.Areas.Identity.Data; // Your SampleUser class

public class CreateModel : PageModel
{
    private readonly DBContextSample _context;
    private readonly Microsoft.AspNetCore.Identity.UserManager<SampleUser> _userManager;

    [BindProperty]
    public int MovieGoal { get; set; }

    public CreateModel(DBContextSample context, Microsoft.AspNetCore.Identity.UserManager<SampleUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> OnPostAsync(int movieGoal)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            user.MovieGoal = movieGoal;
            await _context.SaveChangesAsync();
        }

        //return RedirectToAction("SelectGenres", "Genre");
        return RedirectToPage("/Profile/Edit");

    }
}
