using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TodoListAPI.Models;
using TodoListAPI.Services;

namespace TodoListAPI.Controllers
{
    public class UserAuthController : ApiController
    {
        private IUserAuthService service;

        public UserAuthController(IUserAuthService _service)
        {
            service = _service;
        }

        [ResponseType(typeof(int))]
        public IHttpActionResult Get(UserDTO user)
        {
            if (user == null) return BadRequest();

            bool valid = service.AuthUser(user);

            if (valid == false)
            {
                return NotFound();
            }

            int result = service.GetUserId(user);

            return Ok(result);
        }
    }
}
