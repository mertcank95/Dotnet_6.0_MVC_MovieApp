using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;
using Newtonsoft.Json;

namespace MovieApp.Controllers
{
    public class ImdbController : Controller
    {
        private readonly HttpClient _httpClient;

        public ImdbController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("authorization", "apikey 2aGBAQRR0mqLJHREgRusXw:33qpQQxbLSDH8VgFnqFveb");
        }

        public IActionResult Imdb()
        {
            return View();
        }


        public async Task<IActionResult> MovieData(string movieName)
        {
            if (string.IsNullOrEmpty(movieName))
                movieName = "inception";

            string url = "https://api.collectapi.com/imdb/imdbSearchByName?query=" + movieName;

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();

                dynamic data = JsonConvert.DeserializeObject<dynamic>(json);

                if (data.success == true)
                {
                    List<Movie> movieList = new List<Movie>();

                    foreach (var result in data.result)
                    {
                        Movie movie = new Movie
                        {
                            Title = result.Title,
                            Year = result.Year,
                            ImdbID = result.ImdbID,
                            Type = result.Type,
                            Poster = result.Poster
                        };

                        movieList.Add(movie);
                    }

                    ViewBag.MovieDataList = movieList;
                }
                else
                {
                    ViewBag.ErrorMessage = "Film verileri alınamadı.";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "API isteği başarısız oldu.";
            }

            return View("Imdb");
        }




    }
}
