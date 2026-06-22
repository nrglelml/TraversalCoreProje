using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TraversalCoreProje.Areas.Admin.Models;

namespace TraversalCoreProje.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class BookingHotelSearchController : BaseAdminController
    {
        private readonly string apiKey = "f6e6f3b192mshd8d9c354404e70fp1f402ejsn3a919764ef3b";
        private readonly string apiHost = "booking-com15.p.rapidapi.com";

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                return View();
            }

            var destinations = await SearchDestination(city);

            if (destinations == null || destinations.Count == 0)
            {
                ViewBag.Message = "Şehir bulunamadı.";
                return View();
            }

            var firstMatch = destinations[0];
            return RedirectToAction("HotelList", new { dest_id = firstMatch.dest_id, cityName = firstMatch.name });
        }

        public async Task<IActionResult> HotelList(string dest_id, string cityName)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{apiHost}/api/v1/hotels/searchHotels?dest_id={dest_id}&search_type=CITY&arrival_date=2026-07-01&departure_date=2026-08-01&adults=2&children_age=0%2C17&room_qty=1&page_number=1&units=metric&temperature_unit=c&languagecode=tr&currency_code=TRY"),
                Headers =
                {
                    { "x-rapidapi-key", apiKey },
                    { "x-rapidapi-host", apiHost },
                },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<BookingHotelSearchViewModel.Rootobject>(body);

                ViewBag.CityName = cityName;
                return View(values?.data?.hotels ?? new BookingHotelSearchViewModel.Hotel[0]);
            }
        }
        // ---------------- HELPER ----------------
        private async Task<List<BookingDestinationSearchViewModel.Datum>> SearchDestination(string city)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://{apiHost}/api/v1/hotels/searchDestination?query={city}"),
                Headers =
                {
                    { "x-rapidapi-key", apiKey },
                    { "x-rapidapi-host", apiHost },
                },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<BookingDestinationSearchViewModel>(body);
                return result?.data;
            }
        }
    }
}