using Microsoft.AspNetCore.Mvc.RazorPages;
using ASPNETIDEnTITYAPP.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

public class ProfileModel : PageModel
{
    private readonly UserManager<SampleUser> _userManager;

    public ProfileModel(UserManager<SampleUser> userManager)
    {
        _userManager = userManager;
    }

    public SampleUser LoggedInUser { get; set; } // Renamed from CurrentUser to avoid conflict
    public int? MovieGoal { get; set; }

    public async Task OnGetAsync()
    {
        // Use ClaimsPrincipal to get the logged-in user's ID
        var userId = _userManager.GetUserId(User); // 'User' now refers to ClaimsPrincipal

        if (!string.IsNullOrEmpty(userId))
        {
            // Fetch the user from the database using the ID
            LoggedInUser = await _userManager.FindByIdAsync(userId);

            if (LoggedInUser != null)
            {
                // Assign the MovieGoal property (if it exists)
                MovieGoal = LoggedInUser.MovieGoal;
            }
        }
    }
}
