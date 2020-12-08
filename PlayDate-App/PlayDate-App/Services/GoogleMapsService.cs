using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlayDate_App.Services
{
    public class GoogleMapsService
    {
        public async Task GetLatLng(string address)
        {
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={APIKeys.GoogleApi}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonResult = await response.Content.ReadAsStringAsync();
        }
    }
}
