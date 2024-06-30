using Restaurant.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Service.Interface
{
    public interface IMenuService
    {
        List<Menu> GetAllMenues();
        Menu GetDetailsMenu(Guid? id);
        void CreateNewMenu(Menu menu);
        void UpdateNewMenu(Menu menu);
        void DeleteMenu(Guid? id);
        List<Menu> GetMenusForRestaurant(Guid restaurantId);
    }
}
