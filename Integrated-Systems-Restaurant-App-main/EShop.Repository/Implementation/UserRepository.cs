using ERestaurant.Repository.Interface;
using EShop.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<CostumerUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<CostumerUser>();
        }
        public void Delete(CostumerUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public CostumerUser Get(string? id)
        {
            return entities
                .Include(z => z.ShoppingCart)
                .Include("ShoppingCart.ItemInShoppingCarts")
                .Include("ShoppingCart.ItemInShoppingCarts.MenuItem")
                .SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<CostumerUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(CostumerUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(CostumerUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
    }
}
