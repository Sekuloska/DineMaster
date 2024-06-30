using Restaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Service.Interface
{
    public interface IMenuItemService
    {
        List<MenuItem> GetAllMenuItems();
        MenuItem GetDetailsMenuItem(Guid? id);
        void CreateNewMenuItem(MenuItem menuItem);
        void UpdateNewMenuItem(MenuItem menuItem);
        void DeleteMenuItem(Guid? id);
        List<MenuItem> GetAllItemsInMenu(Guid? menuId);
    }
}
