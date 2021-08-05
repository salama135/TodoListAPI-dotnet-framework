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
using TodoListAPI.Repositories;
using TodoListAPI.Services;

namespace TodoListAPI.Controllers
{
    public class TodoItemsController : ApiController
    {
        private IService<TodoItemDTO> service;

        public TodoItemsController(IService<TodoItemDTO> _service)
        {
            service = _service;
        }

        // GET: api/TodoItems
        [ResponseType(typeof(IEnumerable<TodoItemDTO>))]
        public IHttpActionResult Get(SearchCriteria<TodoItemDTO> searchCriteria, int? id = null)
        {
            if(searchCriteria == null) return BadRequest();

            IEnumerable<TodoItemDTO> result = service.Get(searchCriteria);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST: api/TodoItems
        [ResponseType(typeof(TodoItemDTO))]
        public IHttpActionResult Post(TodoItemDTO todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TodoItemDTO newtodoItem = service.Post(todoItem);

            if (newtodoItem == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("DefaultApi", new { id = todoItem.Id }, todoItem);
        }

        // PUT: api/TodoItems/5
        [ResponseType(typeof(TodoItemDTO))]
        public IHttpActionResult Put(int id, TodoItemDTO todoItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            TodoItemDTO newtodoItem = service.Put(todoItem);

            if (newtodoItem == null)
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