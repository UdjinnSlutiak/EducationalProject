using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private IUserLogic logic;
        public UserController (IUserLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return logic.Get();
        }

        [HttpGet("{id}")]
        public User Get(int id)
        {
            return logic.Get(id);
        }

        [HttpPost]
        public void Post([FromBody] User user)
        {
            if (ModelState.IsValid)
                logic.Create(user);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            if (ModelState.IsValid)
                logic.Update(id, user);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            logic.Delete(id);
        }

    }
}
