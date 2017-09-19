using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CrudWebApi.Models;
using CrudWebApi.Resources.Categories;
using CrudWebApi.Resources.Posts;
using CrudWebApi.Resources.Reviews;

namespace CrudWebApi.Controllers
{
    public class CategoryController : ApiController
    {
        private BlogContext db = new BlogContext();

        // GET categories/1
        public IHttpActionResult Get(int id)
        {
            var category = db.Categories.Include("Posts").ToList().Find(x => x.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            var categoryRepresentation = new CategoryRepresentation()
            {
                Id = category.Id,
                Title = category.Title,
                Posts = category.Posts.Select(x => new PostRepresentation()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Content = x.Content
                }).ToList()
            };

            return Ok(categoryRepresentation);
        }

        // PUT: categories/1
        public IHttpActionResult Put(int id, CategoryRepresentation newCategoryRepresentation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCategory = new Category()
            {
                Id = newCategoryRepresentation.Id,
                Title = newCategoryRepresentation.Title,
                Posts = newCategoryRepresentation.Posts?.Select(x => new Post()
                {
                    Id = x.Id,
                    Title = x.Title
                }).ToList() ?? new List<Post>(),
            };

            if (id != newCategoryRepresentation.Id)
            {
                return BadRequest();
            }

            try
            {
                var category = db.Categories.Include(x => x.Posts).Single(x => x.Id == id);

                db.Entry(category).CurrentValues.SetValues(newCategory);

                foreach (var post in category.Posts.ToList())
                {
                    // ReSharper disable once SimplifyLinqExpression
                    if (!newCategory.Posts.Any(x => x.Id == post.Id))
                    {
                        category.Posts.Remove(post);
                    }
                }

                foreach (var post in newCategory.Posts)
                {
                    if (category.Posts.Any(x => x.Id == post.Id)) continue;
                    db.Posts.Attach(post);
                    category.Posts.Add(post);
                }

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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
            Category category;
            if (CategoryExists(id))
            {
                category = db.Categories.Include(x => x.Posts).Single(x => x.Id == id);
            }
            else
            {
                return NotFound();
            }

            category.Posts = null;

            db.Categories.Remove(category);
            db.SaveChanges();
            
            return Ok(new CategoryRepresentation()
            {
                Id = category.Id,
                Title = category.Title,
                Posts = category.Posts?.Select(x => new PostRepresentation()
                {
                    Id = x.Id,
                    Title = x.Title
                }).ToList()
            });
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.Id == id) > 0;
        }
    }
}
