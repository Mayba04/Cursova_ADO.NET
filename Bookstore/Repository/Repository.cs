using Bookstore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public BookstoreDBContext context;
        internal DbSet<TEntity> dbSet;

        public Repository(BookstoreDBContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public TEntity GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int ID)
        {
            TEntity entityToDelete = dbSet.Find(ID);
            Delete(entityToDelete);
        }


        public void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public void Save()
        {
            context.SaveChanges();
        }

       

    }
}
