using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CrudWebApi.Models;
using CrudWebApi.Resources.Posts;
using CrudWebApi.Resources.Reviews;
using CrudWebApi.Resources.Users;

namespace CrudWebApi.Controllers
{
    public class ReviewController : ApiController
    {
        private BlogContext db = new BlogContext();

        // GET reviews/1
        public IHttpActionResult Get(int id)
        {
            var review = db.Reviews.Include("Post").Include("User").ToList().Find(x => x.Id == id);

            if (review == null)
            {
                return NotFound();
            }
            
            var reviewRepresentation = new ReviewRepresentation()
            {
                Id = review.Id,
                Content = review.Content
            };

            if (review.Post != null)
            {
                reviewRepresentation.Post = new PostRepresentation()
                {
                    Id = review.Post.Id,
                    Title = review.Post.Title,
                    Content = review.Post.Content
                };
            }

            if (review.User != null)
            {
                reviewRepresentation.User = new UserRepresentation()
                {
                    Id = review.User.Id,
                    Name = review.User.Name
                };
            }

            return Ok(reviewRepresentation);
        }

        // PUT: reviews/5
        public IHttpActionResult Put(int id, ReviewRepresentation newReviewRepresentation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newReview = new Review()
            {
                Id = newReviewRepresentation.Id,
                Content = newReviewRepresentation.Content
            };

            if (newReviewRepresentation.Post != null)
            {
                newReview.Post = new Post()
                {
                    Id = newReviewRepresentation.Post.Id,
                    Title = newReviewRepresentation.Post.Title,
                    Content = newReviewRepresentation.Post.Content
                };
            }

            if (newReviewRepresentation.User != null)
            {
                newReview.User = new User()
                {
                    Id = newReviewRepresentation.User.Id,
                    Name = newReviewRepresentation.User.Name
                };
            }

            if (id != newReviewRepresentation.Id)
            {
                return BadRequest();
            }

            try
            {
                var review = db.Reviews.Include("Post").Include("User").Single(x => x.Id == id);

                db.Entry(review).CurrentValues.SetValues(newReview);

                if (newReview.Post != null)
                {
                    if (review.Post != null)
                    {
                        if (review.Post.Id != newReview.Post.Id)
                        {
                            db.Posts.Attach(newReview.Post);
                            review.Post = newReview.Post;
                        }
                    }
                    else
                    {
                        db.Posts.Attach(newReview.Post);
                        review.Post = newReview.Post;
                    }
                }
                else
                {
                    review.Post = null;
                }

                if (newReview.User != null)
                {
                    if (review.User != null)
                    {
                        if (review.User.Id != newReview.User.Id)
                        {
                            db.Users.Attach(newReview.User);
                            review.User = newReview.User;
                        }
                    }
                    else
                    {
                        db.Users.Attach(newReview.User);
                        review.User = newReview.User;
                    }
                }
                else
                {
                    review.User = null;
                }

                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
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
            Review review;
            if (ReviewExists(id))
            {
                review = db.Reviews.Include("User").Include("Post").Single(x => x.Id == id);
            }
            else
            {
                return NotFound();
            }

            review.Post = null;
            review.User = null;

            db.Reviews.Remove(review);
            db.SaveChanges();

            var reviewRepresentation = new ReviewRepresentation()
            {
                Id = review.Id,
                Content = review.Content,
            };

            if (review.Post != null)
            {
                reviewRepresentation.Post = new PostRepresentation()
                {
                    Id = review.Post.Id,
                    Title = review.Post.Title,
                    Content = review.Post.Content
                };
            }

            if (review.User != null)
            {
                reviewRepresentation.User = new UserRepresentation()
                {
                    Id = review.User.Id,
                    Name = review.User.Name
                };
            }

            return Ok(reviewRepresentation);
        }
        
        private bool ReviewExists(int id)
        {
            return db.Reviews.Count(e => e.Id == id) > 0;
        }
    }
}
