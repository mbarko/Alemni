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
using Alemni.Models;
using Alemni.Models.Dtos;
using Alemni.Models.Dtos.VideoSeryViewDtos;
using Alemni.Models.Dtos.VidoSeriesViewDtos;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Alemni.Controllers.Api
{
    [Authorize]
    public class VideoSeriesController : ApiController
    {
        private EvilGenius0Entities db = new EvilGenius0Entities();


        //// GET: api/VideoSeries/

        //public IEnumerable<VideoSeryDto> GetVideoSeries()
        //{


        //    return db.VideoSeries.Include(v => v.Teacher1 ).Include(v => v.Cours).ToList().Select(Mapper.Map<VideoSery,VideoSeryDto>);
        //}
        // GET: api/VideoSeries/search/5
        [AllowAnonymous]
        [Route("api/VideoSeries/search/{search}")]
        public IEnumerable<VideoSeriesListItemDto> GetVideoSeries(string search)
        {
            string[] searchWords = search.Split(null);
            var result = from videoSery in db.VideoSeries
                            where searchWords.Contains(videoSery.Cours.Programm.name)|| searchWords.Contains(videoSery.Cours.name ) || searchWords.Contains( videoSery.Teacher1.AspNetUser.UserName)
                         select (new VideoSeriesListItemDto
                         {
                             Id = videoSery.Id, name = videoSery.name,
                             teacherName = videoSery.Teacher1.AspNetUser.UserName,
                             programmName = videoSery.Cours.Programm.name,
                             price = videoSery.price,
                             enrollments = videoSery.enrollments,
                             views = videoSery.views,
                             videos = videoSery.videos,
                             approved = videoSery.approved,
                             duration = videoSery.duration,
                             seryimage = videoSery.seryimage


                         });
          

            return result;
        }
        [Route("api/VideoSeries/MyVideoSeries")]
        [HttpGet]
        public async Task<List<VideoSeriesListItemDto>> MyVideoSeries()
        {
           
            
            var currentUserId = User.Identity.GetUserId();

            String student = currentUserId;

            var result =  await db.Transactions.Where(x => (x.student == student)).Select(item => new VideoSeriesListItemDto
            {
                Id = item.VideoSery.Id,
                name = item.VideoSery.name,
                teacherName = item.VideoSery.Teacher1.AspNetUser.UserName,
                programmName = item.VideoSery.Cours.Programm.name,
                price = item.VideoSery.price,
                enrollments = item.VideoSery.enrollments,
                views = item.VideoSery.views,
                videos = item.VideoSery.videos,
                approved = item.VideoSery.approved,
                duration = item.VideoSery.duration,
                seryimage = item.VideoSery.seryimage,
                startdate = item.startdate,
                enddate = item.enddate,
                active = item.active


            }).GroupBy(v => v.Id).Select(v => v.FirstOrDefault()).ToListAsync();

            return result;
        }
        // GET: api/VideoSeries/5   

        public  VideoSeryViewDto GetVideoSery(int id)
        {
            var videoSeryViewDto =( from videoSery in db.VideoSeries
                            where videoSery.Id == id select(new VideoSeryViewDto
            {
                Id = videoSery.Id,
                name = videoSery.name,
               
                price = videoSery.price,
                enrollments = videoSery.enrollments,
                views = videoSery.views,
                videos = videoSery.videos,
                approved = videoSery.approved,
                duration = videoSery.duration,
                seryimage = videoSery.seryimage,
                teacherName = videoSery.Teacher1.AspNetUser.UserName,
            programmName = videoSery.Cours.Programm.name,
            teacherCredentials = videoSery.Teacher1.credentials,
            teacherEmail = videoSery.Teacher1.AspNetUser.Email,
            teacherPhone = videoSery.Teacher1.CollectionInfo1.cellnumber

        })).Single(); 
           
           


   

            return videoSeryViewDto;
        }

        // PUT: api/VideoSeries/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVideoSery(int id, VideoSery videoSery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != videoSery.Id)
            {
                return BadRequest();
            }

            db.Entry(videoSery).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoSeryExists(id))
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

        // POST: api/VideoSeries
        [ResponseType(typeof(VideoSery))]
        public async Task<IHttpActionResult> PostVideoSery(VideoSery videoSery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VideoSeries.Add(videoSery);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VideoSeryExists(videoSery.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = videoSery.Id }, videoSery);
        }

        // DELETE: api/VideoSeries/5
        [ResponseType(typeof(VideoSery))]
        public async Task<IHttpActionResult> DeleteVideoSery(int id)
        {
            VideoSery videoSery = await db.VideoSeries.FindAsync(id);
            if (videoSery == null)
            {
                return NotFound();
            }

            db.VideoSeries.Remove(videoSery);
            await db.SaveChangesAsync();

            return Ok(videoSery);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VideoSeryExists(int id)
        {
            return db.VideoSeries.Count(e => e.Id == id) > 0;
        }
    }
}