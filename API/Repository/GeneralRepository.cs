using API.Context;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Repository
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class
        where Context : MyContext
    {
        private readonly MyContext myContext;
        private readonly DbSet<Entity> entities;


        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<Entity>();
        }

        public int Delete(Key key)
        {
            var temp = entities.Find(key);
            entities.Remove(temp);
            var result = myContext.SaveChanges();
            return result;
        }

        public IEnumerable<Entity> Get()
        {
            return entities.ToList();
        }

        public Entity Get(Key key)
        {
            var result = entities.Find(key);
            return result;
        }

        public int Insert(Entity entity)
        {
            entities.Add(entity);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Update(Entity entity)
        {
            myContext.Entry(entities).State = EntityState.Modified;
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
