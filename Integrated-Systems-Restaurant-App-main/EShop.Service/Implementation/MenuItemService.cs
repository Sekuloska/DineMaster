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
    public class MenuItemService : IMenuItemService
    {
        private readonly IRepository<MenuItem> _repository;
        public MenuItemService(IRepository<MenuItem> menuItemRepository)
        {
            _repository = menuItemRepository;
        }
        public void CreateNewMenuItem(MenuItem menuItem)
        {
           _repository.Insert(menuItem);
        }

        public void DeleteMenuItem(Guid? id)
        {
            var item=_repository.Get(id);
            _repository.Delete(item);
        }

        public List<MenuItem> GetAllMenuItems()
        {
            return _repository.GetAll().ToList();
        }

        public MenuItem GetDetailsMenuItem(Guid? id)
        {
            return _repository.Get(id);
        }

        public void UpdateNewMenuItem(MenuItem menuItem)
        {
            _repository.Update(menuItem);
        }
        public List<MenuItem> GetAllItemsInMenu(Guid? menuId)
        {
            return _repository.GetAll()
                .Where(item=>item.MenuId== menuId).ToList();
           
        }
    }
}
