using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using CrudWebApi.Models;
using CrudWebApi.Resources.Reviews;
using CrudWebApi.Resources.Users;

namespace CrudWebApi.Controllers
{
    public class UserController : ApiController
    {
        private BlogContext db = new BlogContext();

        // GET users/1
        public IHttpActionResult Get(int id)
        {
            var user = db.Users.Include("Reviews").ToList().Find(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var reviews = db.Reviews.Include("User")
                .Where(x => x.User.Id == user.Id)
                .ToList()
                .Select(x => new ReviewRepresentation
                {
                    Id = x.Id,
                    Content = x.Content
                })
                .ToList();

            var userRepresentation = new UserRepresentation()
            {
                Id = user.Id,
                Name = user.Name
            };

            if (reviews.Count > 0)
            {
                userRepresentation.Reviews = new List<ReviewRepresentation>();
                foreach (var review in reviews)
                {
                    userRepresentation.Reviews.Add(review);
                }
            }

            return Ok(userRepresentation);
        }

        // PUT: api/Users/5
        public IHttpActionResult Put(int id, UserRepresentation newUserRepresentation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = new User()
            {
                Id = newUserRepresentation.Id,
                Name = newUserRepresentation.Name,
                Reviews = newUserRepresentation.Reviews?.Select(x => new Review()
                {
                    Id = x.Id
                }).ToList() ?? new List<Review>()
            };
            

            if (id != newUserRepresentation.Id)
            {
                return BadRequest();
            }

            try
            {
                var user = db.Users.Include(x => x.Reviews).Single(x => x.Id == id);

                db.Entry(user).CurrentValues.SetValues(newUser);

                foreach (var review in user.Reviews.ToList())
                {
                    // ReSharper disable once SimplifyLinqExpression
                    if (!newUser.Reviews.Any(x => x.Id == review.Id))
                    {
                        user.Reviews.Remove(review);
                    }
                }

                foreach (var review in newUser.Reviews)
                {
                    if (user.Reviews.Any(x => x.Id == review.Id)) continue;
                    db.Reviews.Attach(review);
                    user.Reviews.Add(review);
                }

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: users/1
        public IHttpActionResult Delete(int id)
        {
            User user;
            if (UserExists(id))
            {
                user = db.Users.Include(x => x.Reviews).Single(x => x.Id == id);
            }
            else
            {
                return NotFound();
            }

            user.Reviews = null;
            
            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(new UserRepresentation()
            {
                Id = user.Id,
                Name = user.Name,
                Reviews = user.Reviews?.Select(x => new ReviewRepresentation()
                {
                    Id = x.Id,
                    Content = x.Content
                }).ToList()
            });
        }
       
        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}