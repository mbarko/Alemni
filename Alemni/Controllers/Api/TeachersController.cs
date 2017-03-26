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
using Alemni.Models.Dtos.ProgramViewDtos;
using Alemni.ViewModels;

namespace Alemni.Controllers.Api
{
    [Authorize]
    public class TeachersController : ApiController
    {
        private EvilGenius0Entities db = new EvilGenius0Entities();

        // GET: api/Teachers
        public IQueryable<Teacher> GetTeachers()
        {
            return db.Teachers;
        }
        [AllowAnonymous]
        [Route("api/GetProgramViewModel/{program}")]
        public ProgramViewModel GetProgramViewModel(string program)
        {
            if (program == null || program == "")
                return null;


            var result = (from Programm in db.Programms
                          where (Programm.name == program)
                          select (new ProgramViewModel
                          {

                              Courses = from Cours in db.Courses
                                        where (Cours.Programm.name == program)
                                        select (new CoursDto
                                        {

                                            name = Cours.name,

                                        }),
                              ImageURL = Programm.programpic,
                              Teachers = (from VideoSery in db.VideoSeries
                                          where (VideoSery.Cours.Programm.name == program)
                                          select (new TeacherDto
                                          {

                                              email = VideoSery.Teacher1.AspNetUser.UserName,
                                              firstname = VideoSery.Teacher1.firstname,
                                              lastname = VideoSery.Teacher1.lastname,
                                              title = VideoSery.Teacher1.title,
                                              id = VideoSery.Teacher1.Id,
                                              teacherimage = VideoSery.Teacher1.teacherimage
                                          })).Distinct()
                         })).FirstOrDefault();








            return result;
        }

        [AllowAnonymous]
        [Route("api/Teachers/GetTeacherForTeacherView/{id}")]
        public async Task<Alemni.Models.Dtos.TeacherViewDtos.TeacherDto> GetTeacherForteacherView(string id)
        {
            if (id == null || id == "")
                return null;


            var result = (from Teacher in db.Teachers
                         where (Teacher.Id== id)
                         select (new  Alemni.Models.Dtos.TeacherViewDtos.TeacherDto
                         {

                             email =Teacher.AspNetUser.UserName,
                             firstname = Teacher.firstname,
                             lastname = Teacher.lastname,
                             title = Teacher.title,
                             id = Teacher.Id,
                             credentials = Teacher.credentials,
                             teacherimage = Teacher.teacherimage,
                             certification = Teacher.certification,
                             phone = Teacher.phone,
                             cover = Teacher.VideoSeries.FirstOrDefault().seryimage

                         })).SingleAsync();

            return result.Result;
        }

        // GET: api/Teachers/5
        [ResponseType(typeof(Teacher))]
        public async Task<IHttpActionResult> GetTeacher(string id)
        {
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        // PUT: api/Teachers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTeacher(string id, Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teacher.Id)
            {
                return BadRequest();
            }

            db.Entry(teacher).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
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

        // POST: api/Teachers
        [ResponseType(typeof(Teacher))]
        public async Task<IHttpActionResult> PostTeacher(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teachers.Add(teacher);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TeacherExists(teacher.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = teacher.Id }, teacher);
        }

        // DELETE: api/Teachers/5
        [ResponseType(typeof(Teacher))]
        public async Task<IHttpActionResult> DeleteTeacher(string id)
        {
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            db.Teachers.Remove(teacher);
            await db.SaveChangesAsync();

            return Ok(teacher);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeacherExists(string id)
        {
            return db.Teachers.Count(e => e.Id == id) > 0;
        }
    }
}