using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace ASPNETIDEnTITYAPP.Areas.Identity.Data;

// Add profile data for application users by adding properties to the SampleUser class
public class SampleUser : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? MovieGoal { get; set; }
    public string Likes { get; set; } // Stores movie genres or preferences
    public string Dislikes { get; set; } // Stores dislikes about movies
    public int? Age { get; set; } // Stores the user's age
    public string CountryOfBirth { get; set; } // Stores the country of birth
    public string ProfilePicturePath { get; set; }
}

