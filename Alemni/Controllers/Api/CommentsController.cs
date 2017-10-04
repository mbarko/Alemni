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
using Alemni.Models.Dtos.VideoSeryViewDtos;

namespace Alemni.Controllers
{
    [Authorize]
    public class CommentsController : ApiController
    {
        private EvilGenius0Entities db = new EvilGenius0Entities();


        // GET: api/VideoSery/Comments/{vId}
        [Route("api/VideoSery/Comments/{vId}")]
        [AllowAnonymous]
        public IEnumerable<CommentDto> GetComments(int vId)
        {
            IEnumerable<CommentDto> CommentDtos = db.Comments.Where(c=> c.vId == vId).Select(item =>new CommentDto {

    parent= item.parent,
    created= item.created,
    modified= item.modified,
    content= item.content,
    cId= item.cId,
    fullname=item.fullname,

    createdByCurrentUser= item.createdByCurrentUser,
    upvote_count= item.upvote_count,
     user_has_upvoted=item.user_has_upvoted

            }) ;
                         
            return CommentDtos;
        }

        // GET: api/Comments/5
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> GetComment(int id)
        {
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // PUT: api/Comments/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutComment(int id, Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.Id)
            {
                return BadRequest();
            }

            db.Entry(comment).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comments
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> PostComment(Comment comment)
        {

         
           
            comment.uId = User.Identity.GetUserId();
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comments.Add(comment);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                String innerMessage = (ex.InnerException != null)
                       ? ex.InnerException.Message
                       : "";
                {
                    throw;
                }
            }
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comments/5
        [ResponseType(typeof(Comment))]
        public async Task<IHttpActionResult> DeleteComment(int id)
        {
            Comment comment = await db.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            db.Comments.Remove(comment);
            await db.SaveChangesAsync();

            return Ok(comment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int id)
        {
            return db.Comments.Count(e => e.Id == id) > 0;
        }
    }
}