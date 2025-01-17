using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ASPNETIDEnTITYAPP.Models;

public class GenreService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GenreService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["TMDb:ApiKey"]; // Store the API key in appsettings.json
    }

    public async Task<List<Genre>> GetGenresAsync()
    {
        // TMDb API endpoint for movie genres
        var response = await _httpClient.GetAsync($"https://api.themoviedb.org/3/genre/movie/list?api_key={_apiKey}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(content);
        var genres = json["genres"]!.ToObject<List<Genre>>();

        return genres ?? new List<Genre>();
    }
}
