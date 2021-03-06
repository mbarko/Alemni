﻿using System;
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
using Alemni.Models.Dtos.VideoSeriesICreatedDtos;
using Alemni.Models.Dtos.VideoSeryViewDtos;
using Alemni.Models.Dtos.VidoSeriesViewDtos;
using AutoMapper;
using JsonPatch;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Alemni.Models.Dtos.MyVideoseriesDtos;

namespace Alemni.Controllers.Api
{
    [Authorize]
    public class VideoSeriesController : ApiController
    {
        private EvilGenius0Entities db = new EvilGenius0Entities();


        //GET: REQUESTS SECTION********************************************************************************
        
        // GET: api/VideoSeries/

        //public IEnumerable<VideoSeryDto> GetVideoSeries()
        //{


        //    return db.VideoSeries.Include(v => v.Teacher1 ).Include(v => v.Cours).ToList().Select(Mapper.Map<VideoSery,VideoSeryDto>);
        //}
        
        [AllowAnonymous]
        [Route("api/VideoSeries/search/{search}")]
        public IEnumerable<VideoSeriesListItemDto> GetVideoSeries(string search)
        {
            if (search.IsNullOrWhiteSpace() || search == "")
                return null;
            string[] searchWords = search.Split(null);

          
            var result = from videoSery in db.VideoSeries
                            where (searchWords.Contains(videoSery.Cours.Programm.name) || searchWords.Contains(videoSery.Cours.Id.ToString()) || searchWords.Contains(videoSery.Cours.name ) || searchWords.Contains( videoSery.Teacher1.Id)) && videoSery.approved == true
                         select (new VideoSeriesListItemDto
                         {
                             Id = videoSery.Id, name = videoSery.name,
                             teacherName = videoSery.Teacher1.title + " " +videoSery.Teacher1.firstname + " " + videoSery.Teacher1.lastname,
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
        public async Task<List<MyVideoSeriesListItemDto>> MyVideoSeries()
        {                    
            var currentUserId = User.Identity.GetUserId();
            String student = currentUserId;
            var result =  await db.Transactions.Where(x => (x.student == student)).Select(item => new MyVideoSeriesListItemDto
            {
                VideoSery = new VideoSeriesListItemDto()
                {
                    Id = item.VideoSery.Id,
                    name = item.VideoSery.name,
                    teacherName = item.VideoSery.Teacher1.firstname+" "+ item.VideoSery.Teacher1.lastname,
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
                    active = item.active,
                    
                },

                Count = db.Transactions.Where(x => (x.student == student)&& (x.VideoSery.Id == item.VideoSery.Id)).Count()
        }).GroupBy(v => v.VideoSery.Id).Select(v => v.FirstOrDefault()).ToListAsync();
          
            return result;
        }
        [Route("api/VideoSeries/VideoSeriesICreated")]
        [HttpGet]
        public async Task<List<VideoSeriesICreadteListItemDto>> VideoSeriesICreated()
        {


            var currentUserId = User.Identity.GetUserId();

            String teacher = currentUserId;
            decimal margin = 0.25M;

            var result = await db.Sections.Where(x => (x.VideoSery1.teacher == teacher)).Select(item => new VideoSeriesICreadteListItemDto()
            {
                Id = item.Id,
                VideoSeryId = item.videosery,
                VideoSeryName = item.VideoSery1.name,
                Price= item.price,
                Profit = (item.Transactions.Any()) ?  item.Transactions.Select(p=> p.payment *margin).Sum(): 0,
                Enrollments = (item.Transactions.Any()) ? item.Transactions.Where(i => i.paymentstatus == true ).Count(): 0,
                Views = 0,
                Approved = item.VideoSery1.approved,
                SectionName = item.name,
                LocalOrder = item.localorder
             
    

            }).ToListAsync();

            return result;
        }
    
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
            teacherPhone = videoSery.Teacher1.CollectionInfo1.cellnumber,
            teacher = videoSery.Teacher1.Id,
            teacherFirstName = videoSery.Teacher1.firstname,
            teacherLastName = videoSery.Teacher1.lastname,
            Teachertitle = videoSery.Teacher1.title,
            teacherImage = videoSery.Teacher1.teacherimage

                            })).Single(); 
           
           


   

            return videoSeryViewDto;
        }

        // PUT: api/VideoSeries/5*******************************************************************************
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVideoSery(VideoSeriesICreadteListItemDto videoSeryListItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var v = (from section in db.Sections
                     where section.Id == videoSeryListItem.Id
                     select section).First();
            if (videoSeryListItem.Id != v.Id)
            {
                return BadRequest();
            }

            v.price = videoSeryListItem.Price;
           

            db.Entry(v).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoSeryExists(videoSeryListItem.Id))
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
        //PATCH: api/VideoSeries/5

        [HttpPatch]
        [ResponseType(typeof(void))]

        public async Task<IHttpActionResult> Patch(int id, JsonPatchDocument<VideoSeriesICreatedPatchDto> patch)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var v = (from videoSery in db.VideoSeries
                where videoSery.Id == id
                select videoSery).First();
          //  patch.ApplyUpdatesTo(v);


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