using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TodoListAPI.Criteria;
using TodoListAPI.Data;
using TodoListAPI.Models;

namespace TodoListAPI.Repositories
{
    public abstract class CrudRepository<T> : ICrudRepository<T> where T : BaseEntity
    {
        protected TodoListAPIContext context;

        public CrudRepository(TodoListAPIContext Context)
        {
            this.context = Context;
        }

        public T Create(T item)
        {
            if (item == null)
                throw new InvalidOperationException($"{typeof(T)} is null");

            var createdItem = context.Set<T>().Add(item);
            return createdItem;
        }
        
        public IList<T> Read(SearchCriteria<T> criteria)
        {
            var query = Search(criteria);
            query = Paginate(criteria, query);
            var data = Sort(criteria, query);

            return data.ToList();
        }

        public T Update(T Item)
        {
            bool itemExists = ItemExists(Item.Id);

            if (itemExists == false) return null;
            
            context.Entry(Item).State = EntityState.Modified;

            return Item;
        }

        private bool ItemExists(int id)
        {
            return context.Set<T>().Count(e => e.Id == id) > 0;
        }

        public bool Delete(int id)
        {
            T item = context.Set<T>().Find(id);

            if (item == null) return false;

            context.Set<T>().Remove(item);

            return true;
        }

        private IQueryable<T> Paginate(SearchCriteria<T> criteria, IQueryable<T> query)
        {
            // check if pagination is applied.
            if (criteria.PageIndex > 0 && criteria.PageSize > 0)
            {
                query = query.Skip(criteria.PageSize * criteria.PageIndex)
                             .Take(criteria.PageSize);
            }
            return query;
        }

        protected virtual IOrderedQueryable<T> Sort(SearchCriteria<T> critera, IQueryable<T> query)
        {
            // because T is BaseEntity, so we guarntee that we have a property called "Id";
            critera.SortBy = critera.SortBy ?? "Id";
            return (critera.IsDesc) ? query.OrderByDescending(t => critera.SortBy) : query.OrderBy(t => critera.SortBy);
        }

        public abstract IQueryable<T> Search(SearchCriteria<T> criteria);

        public abstract T GetByID(int id);

        public abstract void Save();
    }
}