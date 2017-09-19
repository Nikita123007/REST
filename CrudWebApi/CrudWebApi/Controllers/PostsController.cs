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
using CrudWebApi.Resources.Posts;
using CrudWebApi.Resources.Reviews;

namespace CrudWebApi.Controllers
{
    public class PostsController : ApiController
    {
        private BlogContext db = new BlogContext();
        private int pageSize = 3;

        // GET posts
        public PostListRepresentation Get(int page = 1)
        {
            var posts = db.Posts.Include("Categories").Include("Reviews").Select(x => new PostRepresentation() { Id = x.Id, Content = x.Content, Title = x.Title }).ToList();
            var pageHandler = new PageHandler<PostRepresentation>(posts, page, pageSize);
            var resourceList = new PostListRepresentation(pageHandler.Result, pageHandler.Total, pageHandler.PagesTotal, pageHandler.Page, LinkTemplates.Posts.GetPosts);

            return resourceList;
        }

        // POST reviews
        public IHttpActionResult Post(PostRepresentation value)
        {
            Post post;
            try
            {
                post = new Post()
                {
                    Content = value.Content,
                    Title = value.Title
                };
            }
            catch
            {
                return BadRequest();
            }

            db.Posts.Add(post);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = post.Id }, new PostRepresentation()
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content
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