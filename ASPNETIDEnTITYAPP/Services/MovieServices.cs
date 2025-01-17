using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using ASPNETIDEnTITYAPP.Models;

public class MovieService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public MovieService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["TMDb:ApiKey"];
    }

    public async Task<List<Movie>> GetMoviesByGenresAsync(List<int> genreIds)
    {
        string genres = string.Join(",", genreIds);
        var response = await _httpClient.GetAsync($"https://api.themoviedb.org/3/discover/movie?api_key={_apiKey}&with_genres={genres}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(content);
        var movies = json["results"]!.ToObject<List<Movie>>();

        return movies ?? new List<Movie>();
    }


    public async Task<Movie> GetMovieDetailsAsync(int movieId)
    {
        var response = await _httpClient.GetAsync($"https://api.themoviedb.org/3/movie/{movieId}?api_key={_apiKey}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var movie = JObject.Parse(content).ToObject<Movie>();

        return movie;
    }

}
