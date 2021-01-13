using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SimpleBlogBlazor.UI.Pages
{
    public partial class FetchData
    {
        private WeatherForecast[] forecasts;
        protected override async Task OnInitializedAsync()
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "WeatherForecast"))
            {
                var tokenResult = await TokenProvider.RequestAccessToken();

                if (tokenResult.TryGetToken(out var token))
                {
                    requestMessage.Headers.Authorization =
                      new AuthenticationHeaderValue("Bearer", token.Value);
                    var response = await Http.SendAsync(requestMessage);
                    forecasts = await response.Content.ReadFromJsonAsync<WeatherForecast[]>();
                }
            }
        }

        public class WeatherForecast
        {
            public DateTime Date { get; set; }

            public int TemperatureC { get; set; }

            public string Summary { get; set; }

            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }
    }
}
