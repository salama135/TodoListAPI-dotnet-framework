using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TodoListAPI.Criteria;
using TodoListAPI.Data;
using TodoListAPI.Models;
using TodoListAPI.Services;

namespace TodoListAPI.Controllers
{
    public class TodoItemsController : ApiController
    {
        private TodoitemsService service = new TodoitemsService();

        // GET: api/TodoItems
        public IEnumerable<TodoItemDTO> Get(TodoItemSearchCriteria todoItemSearchCriteria)
        {
            return service.Get(todoItemSearchCriteria);
        }

        // GET: api/TodoItems/5
        [ResponseType(typeof(TodoItemDTO))]
        public IHttpActionResult Get(int id)
        {
            TodoItemDTO todoItem = service.Get(id);

            if (todoItem == null) return BadRequest();

            return Ok(todoItem);
        }

        // POST: api/TodoItems
        [ResponseType(typeof(TodoItem))]
        public IHttpActionResult Post(TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool successful = service.Post(todoItem);

            if (successful == false)
            {
                return NotFound();
            }

            return CreatedAtRoute("DefaultApi", new { id = todoItem.Id }, todoItem);
        }

        // PUT: api/TodoItems/5
        [ResponseType(typeof(TodoItem))]
        public IHttpActionResult Put(int id, TodoItem todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            bool successful = service.Put(id, todoItem);

            if (successful == false)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        // DELETE: api/TodoItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Delete(int id)
        {
            bool successful = service.Delete(id);

            if (successful == false)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}