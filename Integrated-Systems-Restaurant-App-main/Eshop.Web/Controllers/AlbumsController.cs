using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERestaurant.Domain.Domain;
using ERestaurant.Repository;
using ERestaurant.Service;
using ERestaurant.Service.Implementation;

namespace ERestaurant.Web.Controllers
{
    public class AlbumsController : Controller
    {

            private readonly AlbumService _albumService;

            public AlbumsController(AlbumService albumService)
            {
                _albumService = albumService;
            }
            public async Task<IActionResult> Index()
            {
                var albums = await _albumService.GetAllAlbumsAsync();

                return View(albums);
            }
    }
}
