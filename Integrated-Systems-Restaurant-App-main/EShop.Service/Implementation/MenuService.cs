using ERestaurant.Repository.Interface;
using ERestaurant.Service.Interface;
using Restaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Service.Implementation
{
    public class MenuService : IMenuService
    {
        private readonly IRepository<Menu> _menuRepository;

        public MenuService(IRepository<Menu> menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public void CreateNewMenu(Menu menu)
        {
            _menuRepository.Insert(menu);
        }

        public void DeleteMenu(Guid? id)
        {
            var menu = _menuRepository.Get(id);
            _menuRepository.Delete(menu);
        }

        public List<Menu> GetAllMenues()
        {
            return _menuRepository.GetAll().ToList();
        }

        public Menu GetDetailsMenu(Guid? id)
        {
            return _menuRepository.Get(id);
        }

        public void UpdateNewMenu(Menu menu)
        {
            _menuRepository.Update(menu);
        }
        public List<Menu> GetMenusForRestaurant(Guid restaurantId)
        {
            return _menuRepository.GetAll()
                .Where(menu=> menu.RestaurantId== restaurantId).ToList();
        }
    }
}
