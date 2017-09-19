using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using CrudWebApi.Models;
using CrudWebApi.Resources;
using CrudWebApi.Resources.Users;

namespace CrudWebApi.Controllers
{
    public class PeopleController : ApiController
    {
        private BlogContext db = new BlogContext();
        private int pageSize = 3;

        // GET users
        public UserListRepresentation Get(int page = 1)
        {
            var users = db.Users.Include("Reviews").Select(x => new UserRepresentation() { Id = x.Id, Name = x.Name }).ToList();
            var pageHandler = new PageHandler<UserRepresentation>(users, page, pageSize);
            var resourceList = new UserListRepresentation(pageHandler.Result, pageHandler.Total, pageHandler.PagesTotal, pageHandler.Page, LinkTemplates.Users.GetUsers);

            return resourceList;
        }

        // POST users
        public IHttpActionResult Post(UserRepresentation value)
        {
            User user;
            try
            {
                user = new User()
                {
                    Name = value.Name
                };
            }
            catch
            {
                return BadRequest();
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, new UserRepresentation()
            {
                Id = user.Id,
                Name = user.Name
            });
        }

        // POST users
        //public IHttpActionResult Post(User user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    db.Users.Add(user);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = user.Id }, new UserRepresentation()
        //    {
        //        Id = user.Id,
        //        Name = user.Name
        //    });
        //}
    }
}