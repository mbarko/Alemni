using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Alemni;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Alemni.Controllers.Api
{
    [Authorize]
    public class RatingsController : ApiController
    {
        private EvilGenius0Entities db = new EvilGenius0Entities();

        // GET: api/Ratings
        public IQueryable<Rating> GetRatings()
        {
            return db.Ratings;
        }

        // GET: api/Ratings/5
        [HttpGet]

        public async Task<Decimal> GetRating(string id)
        {
            var currentUserId = User.Identity.GetUserId();
            var search = id + currentUserId;
            Rating rating = await db.Ratings.FindAsync(search);
            if (rating == null)
            {
                return 0;
            }

            return rating.rating1;
        }

        // PUT: api/Ratings/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRating(string id, Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rating.Id)
            {
                return BadRequest();
            }

            db.Entry(rating).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Ratings
        [ResponseType(typeof(Rating))]
        public async Task<IHttpActionResult> PostRating(Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Ratings.Add(rating);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RatingExists(rating.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = rating.Id }, rating);
        }

        // DELETE: api/Ratings/5
        [ResponseType(typeof(Rating))]
        public async Task<IHttpActionResult> DeleteRating(string id)
        {
            Rating rating = await db.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            db.Ratings.Remove(rating);
            await db.SaveChangesAsync();

            return Ok(rating);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RatingExists(string id)
        {
            return db.Ratings.Count(e => e.Id == id) > 0;
        }
    }
}