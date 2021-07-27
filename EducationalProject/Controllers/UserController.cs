using System;
using System.Collections.Generic;
using System.Linq;
using EducationalProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EducationalProject.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {

        private readonly ProjectContext context;

        public UserController (ProjectContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return context.Users.ToList();
        }

        [HttpGet("{id}")]
        public User Get(int id)
        {
            return context.Users.Find(id);
        }

        [HttpPost]
        public void Post([FromBody] User user)
        {
            if (user.isValid())
            {
                context.Add(user);
                context.SaveChangesAsync();
            }
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
            if (user.isValid(id))
            {
                user.Id = id;
                context.Update(user);
                context.SaveChangesAsync();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (id > 0)
            {
                context.Remove(context.Users.Find(id));
                context.SaveChangesAsync();
            }
        }

    }
}
