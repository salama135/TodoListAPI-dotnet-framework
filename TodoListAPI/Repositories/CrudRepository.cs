using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TodoListAPI.Criteria;
using TodoListAPI.Data;
using TodoListAPI.Extension;
using TodoListAPI.Models;

namespace TodoListAPI.Repositories
{
    public abstract class CrudRepository<T> : ICrudRepository<T> where T : BaseEntity
    {
        protected TodoListAPIContext context;

        public CrudRepository(TodoListAPIContext Context)
        {
            context = Context;
        }

        public T Create(T item)
        {
            if (item == null)
                throw new InvalidOperationException($"{typeof(T)} is null");

            var createdItem = context.Set<T>().Add(item);
            return createdItem;
        }
        
        public IList<T> Get(BaseSearchCriteria criteria)
        {
            var query = Search(criteria);
            query = Sort(criteria, query);
            var data = Paginate(criteria, query);

            return data.ToList();
        }

        public T Update(T Item)
        {
            bool itemExists = EnteryExists(Item.Id);

            if (itemExists == false) return null;

            MarkAsModified(Item);

            return Item;
        }

        public bool EnteryExists(int id)
        {
            return context.Set<T>().Count(e => e.Id == id) > 0;
        }

        protected void MarkAsModified(T Item)
        {
            context.Entry(Item).State = EntityState.Modified;
        }

        public bool Delete(int id)
        {
            T item = context.Set<T>().Find(id);

            if (item == null) return false;

            context.Set<T>().Remove(item);

            return true;
        }

        private IQueryable<T> Paginate(BaseSearchCriteria criteria, IQueryable<T> query)
        {
            // check if pagination is applied.
            if (criteria.PageIndex > 0 && criteria.PageSize > 0)
            {
                query = query.Skip(criteria.PageSize * criteria.PageIndex)
                             .Take(criteria.PageSize);
            }

            return query;
        }

        protected virtual IQueryable<T> Sort(BaseSearchCriteria criteria, IQueryable<T> query)
        {
            // because T is BaseEntity, so we guarntee that we have a property called "Id";
            criteria.SortBy = criteria.SortBy ?? "Id";

            return query.OrderByDynamically(criteria.SortBy, criteria.IsDesc);
        }

        public abstract IQueryable<T> Search(BaseSearchCriteria criteria);
    }
}