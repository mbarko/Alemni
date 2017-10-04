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

namespace Alemni.Controllers.Api
{
    public class ChoicesController : ApiController
    {
        private EvilGenius0Entities db = new EvilGenius0Entities();

        // GET: api/Choices
        public IQueryable<Choice> GetChoices()
        {
            return db.Choices;
        }

        // GET: api/Choices/5
        [ResponseType(typeof(Choice))]
        public async Task<IHttpActionResult> GetChoice(int id)
        {
            Choice choice = await db.Choices.FindAsync(id);
            if (choice == null)
            {
                return NotFound();
            }

            return Ok(choice);
        }

        // PUT: api/Choices/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutChoice(int id, Choice choice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != choice.Id)
            {
                return BadRequest();
            }

            db.Entry(choice).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChoiceExists(id))
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

        // POST: api/Choices
        [ResponseType(typeof(Choice))]
        public async Task<IHttpActionResult> PostChoice(Choice choice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Choices.Add(choice);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = choice.Id }, choice);
        }

        // DELETE: api/Choices/5
        [ResponseType(typeof(Choice))]
        public async Task<IHttpActionResult> DeleteChoice(int id)
        {
            Choice choice = await db.Choices.FindAsync(id);
            if (choice == null)
            {
                return NotFound();
            }

            db.Choices.Remove(choice);
            await db.SaveChangesAsync();

            return Ok(choice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ChoiceExists(int id)
        {
            return db.Choices.Count(e => e.Id == id) > 0;
        }
    }
}