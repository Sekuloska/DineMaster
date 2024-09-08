using ERestaurant.Domain.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Service
{
    public class AlbumService
    {
            private readonly HttpClient _httpClient;

            public AlbumService(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            public async Task<List<Album>> GetAllAlbumsAsync()
            {
                var response = await _httpClient.GetAsync("https://musicstoreweb20240902150809.azurewebsites.net/api/api/GetAllAlbums");
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var albums = JsonConvert.DeserializeObject<List<Album>>(jsonData);
                    return albums;
                }

                return new List<Album>();
            }
    }
}
