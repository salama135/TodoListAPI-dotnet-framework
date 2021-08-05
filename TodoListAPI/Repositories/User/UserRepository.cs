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
    public interface IUserRepository : ICrudRepository<User>
    {
        User GetByID(int id);
    }

    public class UserRepository : CrudRepository<User>, IUserRepository
    {
        public UserRepository(TodoListAPIContext Context) : base(Context)
        {
        }

        public override IQueryable<User> Search(BaseSearchCriteria criteria)
        {
            var query = context.Users
                .Where(u => string.IsNullOrEmpty(criteria.Search) || u.Name.Contains(criteria.Search))
                .Where(u => (criteria.UserId >= 0 && u.Id == criteria.UserId) || (criteria.UserId < 0));

            // creationDate < 1-1-2020
            // Contains("title")
            // id = 28

            return query;
        }

        public User GetByID(int id)
        {
            return context.Users.Find(id);
        }
    }
}