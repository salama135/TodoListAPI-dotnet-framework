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
    public class UsersController : ApiController
    {
        private IService<UserDTO> service;

        public UsersController(IService<UserDTO> _service)
        {
            service = _service;
        }

        // GET: api/Users
        [ResponseType(typeof(IEnumerable<UserDTO>))]
        public IHttpActionResult Get(SearchCriteria<UserDTO> searchCriteria, int? id = null)
        {
            if (searchCriteria == null) return BadRequest();

            IEnumerable<UserDTO> dtos = service.Get(searchCriteria);

            if (dtos == null)
            {
                return NotFound();
            }

            return Ok(dtos);
        }

        // GET: api/Users/5
        [ResponseType(typeof(UserDTO))]
        public IHttpActionResult Get(int id)
        {
            UserDTO dto = ((UsersService)service).GetByID(id);

            if (dto == null) return BadRequest();

            return Ok(dto);
        }

        // POST: api/Users
        [ResponseType(typeof(UserDTO))]
        public IHttpActionResult Post(UserDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserDTO createdDto = service.Post(dto);

            if (createdDto == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("DefaultApi", new { id = dto.Id }, dto);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(UserDTO))]
        public IHttpActionResult Put(int id, UserDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dto.Id)
            {
                return BadRequest();
            }

            UserDTO editedDto = service.Put(dto);

            if (editedDto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        // DELETE: api/Users/5
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