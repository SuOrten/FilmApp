namespace ASPNETIDEnTITYAPP.Models
{
    public class UserGenre
    {
        public int Id { get; set; } // Primary key
        public string UserId { get; set; }
        public int GenreId { get; set; } // FK to Genre

        // Navigation properties
        public Genre Genre { get; set; }
    }
}
