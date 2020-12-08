using Newtonsoft.Json;
using PlayDate_App.Data.APIData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlayDate_App.Services
{
    public class GoogleMapsService
    {
        public async Task<GeocodeLocation> GetLatLng(string address)
        {
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={APIKeys.GoogleApi}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonResult = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                GeocodeLocation custLocationData = JsonConvert.DeserializeObject<GeocodeLocation>(jsonResult);
                return custLocationData;

            }
            else
            {
                return new GeocodeLocation();
            }
        }
    }
}
