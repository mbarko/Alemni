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
using Alemni.Models.Dtos;
using AutoMapper;

namespace Alemni.Controllers.Api
{
    [Authorize]
    public class VideosController : ApiController
    {
        private EvilGenius0Entities db = new EvilGenius0Entities();

        // GET: api/Videos
        public IQueryable<Video> GetVideos()
        {
            return db.Videos;
        }
        //GET: api/Videos/search/{id}
        [Route("api/Videos/search/{id}")]
        public async Task<IEnumerable<VideoDto>> GetVideos(int id)
        {
           
            var result = from Video in db.Videos
                         where Video.videoseries == id
                         select Video;

            return result.ToList().Select(Mapper.Map<Video, VideoDto>);
        }

        // GET: api/Videos/5
        [ResponseType(typeof(Video))]
        public async Task<IHttpActionResult> GetVideo(int id)
        {
            Video video = await db.Videos.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }

            return Ok(video);
        }


        //PUT: api/Videos/UpdateViews/{id}
        [Route("api/Videos/UpdateViews/{id}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> UpdateViews(int id)
        {
            var original = db.Videos.Find(id);

            if (original != null)
            {
                original.views += 1;
             
            }
            else
            {
                return BadRequest();
            }

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoExists(id))
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

        // PUT: api/Videos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVideo(int id, Video video)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != video.Id)
            {
                return BadRequest();
            }

            db.Entry(video).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoExists(id))
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

        // POST: api/Videos
        [ResponseType(typeof(Video))]
        public async Task<IHttpActionResult> PostVideo(Video video)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Videos.Add(video);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VideoExists(video.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = video.Id }, video);
        }

        // DELETE: api/Videos/5
        [ResponseType(typeof(Video))]
        public async Task<IHttpActionResult> DeleteVideo(int id)
        {
            Video video = await db.Videos.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }

            db.Videos.Remove(video);
            await db.SaveChangesAsync();

            return Ok(video);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VideoExists(int id)
        {
            return db.Videos.Count(e => e.Id == id) > 0;
        }
    }
}