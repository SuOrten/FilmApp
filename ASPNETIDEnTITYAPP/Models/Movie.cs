namespace ASPNETIDEnTITYAPP.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string? Poster_Path { get; set; }
        public double Vote_Average { get; set; }
    }
}
