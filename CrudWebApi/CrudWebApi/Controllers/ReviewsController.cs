using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CrudWebApi.Models;
using CrudWebApi.Resources;
using CrudWebApi.Resources.Posts;
using CrudWebApi.Resources.Reviews;
using CrudWebApi.Resources.Users;

namespace CrudWebApi.Controllers
{
    public class ReviewsController : ApiController
    {
        private BlogContext db = new BlogContext();
        private int pageSize = 3;

        // GET users
        public ReviewListRepresentation Get(int page = 1)
        {
            var reviews = db.Reviews.Include("User").Include("Post").Select(x => new ReviewRepresentation() { Id = x.Id, Content = x.Content }).ToList();
            var pageHandler = new PageHandler<ReviewRepresentation>(reviews, page, pageSize);
            var resourceList = new ReviewListRepresentation(pageHandler.Result, pageHandler.Total, pageHandler.PagesTotal, pageHandler.Page, LinkTemplates.Reviews.GetReviews);

            return resourceList;
        }

        // POST reviews
        public IHttpActionResult Post(ReviewRepresentation value)
        {
            Review review;
            try
            {
                review = new Review()
                {
                    Content = value.Content
                };
            }
            catch
            {
                return BadRequest();
            }

            db.Reviews.Add(review);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = review.Id }, new ReviewRepresentation()
            {
                Id = review.Id,
                Content = review.Content
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