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
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Alemni.Controllers.Api
{[Authorize]
    public class TransactionsController : ApiController
    {
        private EvilGenius0Entities db = new EvilGenius0Entities();

        // GET: api/Transactions
      
        public IQueryable<Transaction> GetTransactions()
        {
            return db.Transactions;
        }

        // GET: api/Transactions/5
      
        public async Task<List<Transaction>> GetMyActiveVideoSeryTransactions(int videoSery )
        {
           // var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())); 
            // Get the current logged in User and look up the user in ASP.NET Identity
            var currentUserId = User.Identity.GetUserId();

            String student = currentUserId;
            DateTime now = DateTime.UtcNow;
            List<Transaction> transactions = await db.Transactions.Where(x =>( x.videoseries == videoSery &&  x.student == student && ( now < x.enddate))).ToListAsync();
            if (transactions == null)
            {
                return null;
            }

            return transactions ;
        }

        // GET: api/Transactions/GetMyTransactions
        [System.Web.Mvc.Route("api/Transactions/GetMyTransactions")]
   
        public async Task<List<Transaction>> GetMyTransactions()
        {
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            // Get the current logged in User and look up the user in ASP.NET Identity
            var currentUserId =User.Identity.GetUserId();

            String student = currentUserId;
         
            List<Transaction> transactions = await db.Transactions.Where(x => ( x.student == student)).ToListAsync();
            if (transactions == null)
            {
                return null;
            }

            return transactions;
        }


        // PUT: api/Transactions/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transaction.Id)
            {
                return BadRequest();
            }

            db.Entry(transaction).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST: api/Transactions
        //[Authorize]
        //[ResponseType(typeof(Transaction))]
        //public async Task<IHttpActionResult> PostTransaction(Transaction transaction)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Transactions.Add(transaction);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TransactionExists(transaction.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = transaction.Id }, transaction);
        //}

        // POST: api/Transactions
       
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostTransaction(Transaction videoseriesId)
        {
            

            // Get the current logged in User and look up the user in ASP.NET Identity
            var currentUser = User.Identity.GetUserId();
            if(currentUser == null)
                return Ok("please login");

            //Create the transaction object
            Transaction transaction = new Transaction
            {
                videoseries = videoseriesId.Id,
                payerInfo = 1,
                active = true,
                startdate = DateTime.UtcNow,
                enddate = DateTime.UtcNow.AddMonths(6),
                paymentstatus = true,
                student = currentUser,
                payment = 350,

            
            };
            var v = (from videoSery in db.VideoSeries
                    where videoSery.Id== videoseriesId.Id
                    select videoSery).First();
            v.enrollments += 1;
            db.Transactions.Add(transaction);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TransactionExists(transaction.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }


            return StatusCode(HttpStatusCode.NoContent);
            ;
        }
        // DELETE: api/Transactions/5
        [ResponseType(typeof(Transaction))]
        public async Task<IHttpActionResult> DeleteTransaction(int id)
        {
            Transaction transaction = await db.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            db.Transactions.Remove(transaction);
            await db.SaveChangesAsync();

            return Ok(transaction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionExists(int id)
        {
            return db.Transactions.Count(e => e.Id == id) > 0;
        }
    }
}