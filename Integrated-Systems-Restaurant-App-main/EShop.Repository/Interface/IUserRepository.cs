using EShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<CostumerUser> GetAll();
        CostumerUser Get(string? id);
        void Insert(CostumerUser entity);
        void Update(CostumerUser entity);
        void Delete(CostumerUser entity);
    }
}
