using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CrudWebApi.Models;
using CrudWebApi.Resources;
using CrudWebApi.Resources.Categories;
using CrudWebApi.Resources.Posts;

namespace CrudWebApi.Controllers
{
    public class CategoriesController : ApiController
    {
        private BlogContext db = new BlogContext();
        private int pageSize = 3;

        // GET categories
        public CategoryListRepresentation Get(int page = 1)
        {
            var categories = db.Categories.Include("Posts").Select(x => new CategoryRepresentation() { Id = x.Id, Title = x.Title }).ToList();
            var pageHandler = new PageHandler<CategoryRepresentation>(categories, page, pageSize);
            var resourceList = new CategoryListRepresentation(pageHandler.Result, pageHandler.Total, pageHandler.PagesTotal, pageHandler.Page, LinkTemplates.Posts.GetPosts);

            return resourceList;
        }

        // POST categories
        public IHttpActionResult Post(CategoryRepresentation value)
        {
            Category category;
            try
            {
                category = new Category()
                {
                    Title = value.Title
                };
            }
            catch
            {
                return BadRequest();
            }

            db.Categories.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.Id }, new CategoryRepresentation()
            {
                Id = category.Id,
                Title = category.Title
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
    }
}