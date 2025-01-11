using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using ASPNETIDEnTITYAPP.Areas.Identity.Data;

public class EditProfileModel : PageModel
{
    private readonly UserManager<SampleUser> _userManager;

    public EditProfileModel(UserManager<SampleUser> userManager)
    {
        _userManager = userManager;
    }

    [BindProperty]
    public string Likes { get; set; }
    [BindProperty]
    public string Dislikes { get; set; }
    [BindProperty]
    public int? Age { get; set; }
    [BindProperty]
    public string CountryOfBirth { get; set; }
    [BindProperty]
    public IFormFile ProfilePicture { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        Likes = user.Likes;
        Dislikes = user.Dislikes;
        Age = user.Age;
        CountryOfBirth = user.CountryOfBirth;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var userId = _userManager.GetUserId(User);
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        user.Likes = Likes;
        user.Dislikes = Dislikes;
        user.Age = Age;
        user.CountryOfBirth = CountryOfBirth;

        // Handle profile picture upload
        if (ProfilePicture != null)
        {
            var filePath = Path.Combine("wwwroot/images", ProfilePicture.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ProfilePicture.CopyToAsync(stream);
            }
            user.ProfilePicturePath = $"/images/{ProfilePicture.FileName}";
        }

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            //return RedirectToPage("/Profile/Index");
            return RedirectToAction("SelectGenres", "Genre");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return Page();
    }
}
