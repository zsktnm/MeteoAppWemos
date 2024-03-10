using MeteoApp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MeteoApp.Services
{
    public class WeatherGetter : IWeatherGetter
    {
        private readonly HttpClient client;
        private readonly ISettingsService settings;

        public WeatherGetter(HttpClient client, ISettingsService settings) 
        {
            this.client = client;
            this.settings = settings;
        }

        public async Task<Data?> GetDataAsync()
        {
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync($"http://{settings.LoadIpAddress()}/");
            }
            catch
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadFromJsonAsync<Data>();
        }
    }
}
