using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CrudWebApi.Models;
using CrudWebApi.Resources.Categories;
using CrudWebApi.Resources.Posts;
using CrudWebApi.Resources.Reviews;
using CrudWebApi.Resources.Users;

namespace CrudWebApi.Controllers
{
    public class PostController : ApiController
    {
        private BlogContext db = new BlogContext();

        // GET posts/1
        public IHttpActionResult Get(int id)
        {
            var post = db.Posts.Include("Categories").Include("Reviews").ToList().Find(x => x.Id == id);

            if (post == null)
            {
                return NotFound();
            }
        
            var postRepresentation = new PostRepresentation()
            {
                Id = post.Id,
                Content = post.Content,
                Title = post.Title,
                Categories = post.Categories.Select(x => new CategoryRepresentation()
                {
                    Id = x.Id,
                    Title = x.Title
                }).ToList(),
                Reviews = post.Reviews.Select(x => new ReviewRepresentation()
                {
                    Id = x.Id,
                    Content = x.Content
                }).ToList()
            };
            
            return Ok(postRepresentation);
        }
        
        // PUT: posts/1
        public IHttpActionResult Put(int id, PostRepresentation newPostRepresentation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newPost = new Post()
            {
                Id = newPostRepresentation.Id,
                Content = newPostRepresentation.Content,
                Title = newPostRepresentation.Title,
                Reviews = newPostRepresentation.Reviews?.Select(x => new Review()
                {
                    Id = x.Id,
                    Content = x.Content
                }).ToList() ?? new List<Review>(),
                Categories = newPostRepresentation.Categories?.Select(x => new Category()
                {
                    Id = x.Id,
                    Title = x.Title
                }).ToList() ?? new List<Category>(),
            };
            
            if (id != newPostRepresentation.Id)
            {
                return BadRequest();
            }

            try
            {
                var post = db.Posts.Include("Reviews").Include("Categories").Single(x => x.Id == id);

                db.Entry(post).CurrentValues.SetValues(newPost);

                foreach (var review in post.Reviews.ToList())
                {
                    // ReSharper disable once SimplifyLinqExpression
                    if (!newPost.Reviews.Any(x => x.Id == review.Id))
                    {
                        post.Reviews.Remove(review);
                    }
                }

                foreach (var category in post.Categories.ToList())
                {
                    // ReSharper disable once SimplifyLinqExpression
                    if (!newPost.Categories.Any(x => x.Id == category.Id))
                    {
                        post.Categories.Remove(category);
                    }
                }

                foreach (var review in newPost.Reviews)
                {
                    if (post.Reviews.Any(x => x.Id == review.Id)) continue;
                    db.Reviews.Attach(review);
                    post.Reviews.Add(review);
                }

                foreach (var category in newPost.Categories)
                {
                    if (post.Categories.Any(x => x.Id == category.Id)) continue;
                    db.Categories.Attach(category);
                    post.Categories.Add(category);
                }

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
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

        // DELETE: posts/1
        public IHttpActionResult Delete(int id)
        {
            Post post;
            if (PostExists(id))
            {
                post = db.Posts.Include("Categories").Include("Reviews").Single(x => x.Id == id);
            }
            else
            {
                return NotFound();
            }
        
            db.Posts.Remove(post);
            db.SaveChanges();

            return Ok(new PostRepresentation()
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Reviews = post.Reviews?.Select(x => new ReviewRepresentation()
                {
                    Id = x.Id,
                    Content = x.Content
                }).ToList(),
                Categories = post.Categories?.Select(x => new CategoryRepresentation()
                {
                    Id = x.Id,
                    Title = x.Title
                }).ToList()
            });
        }

        private bool PostExists(int id)
        {
            return db.Posts.Count(e => e.Id == id) > 0;
        }
    }
}
