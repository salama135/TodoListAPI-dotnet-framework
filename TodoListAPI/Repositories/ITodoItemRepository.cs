using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoListAPI.Data;
using TodoListAPI.Models;

namespace TodoListAPI.Repositories
{
    public interface ITodoItemRepository
    {
        IEnumerable<TodoItem> GetAll();
        TodoItem GetById(int todoItemID);
        bool Insert(TodoItem todoItem);
        bool Update(int todoItemID, TodoItem todoItem);
        bool Delete(int todoItemID);
        void Save();

        IEnumerable<TodoItem> GetSorted(string sortBy, bool isDesc);
        IEnumerable<TodoItem> GetFiltered(string filterBy);
    }

    public interface IRepository<T> where T : BaseEntity
    {

        IList<T> Get(SearchCriteria<T> criteria);

        T Create(T item);

        T Update(T Item);

        bool Delete(int id);
    }

    public class SearchCriteria<T> where T : BaseEntity
    {
        public string Search { get; set; }
        public string SortBy { get; set; }
        public bool IsDesc { get; set; }
        public T Entity { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected TodoListAPIContext context;
        public Repository(TodoListAPIContext Context)
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

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IList<T> Get(SearchCriteria<T> criteria)
        {
            var query = Search(criteria);
            query = Paginate(criteria, query);
            var data = Sort(criteria, query);

            return data.ToList();
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

        public T Update(T Item)
        {
            throw new NotImplementedException();
        }

        protected virtual IOrderedQueryable<T> Sort(SearchCriteria<T> critera, IQueryable<T> query)
        {
            // because T is BaseEntity, so we guarntee that we have a property called "Id";
            critera.SortBy = critera.SortBy ?? "Id";
            return (critera.IsDesc) ? query.OrderByDescending() : query.OrderBy();
        }

        public abstract IQueryable<T> Search(SearchCriteria<T> criteria);
    }

    public class TodoRepository : Repository<TodoItem>
    {
        public TodoRepository(TodoListAPIContext Context) : base(Context)
        {
        }

        public override IQueryable<TodoItem> Search(SearchCriteria<TodoItem> criteria)
        {

            var query = context.TodoItems.Where(t => t.CreationData > criteria.Entity.CreationData);



            //var query = context.TodoItems.Where(t => string.IsNullOrEmpty(critera.Search) ||
            //                                            t.Title.Contains(critera.Search));
            return query;

        }
    }
}
