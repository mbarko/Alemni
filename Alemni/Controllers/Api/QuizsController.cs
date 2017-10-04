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
using Alemni.Models.Dtos.VideoSeryViewDtos;
using Alemni.Models.Dtos.QuizViewDtos;

namespace Alemni.Controllers.Api
{
    [Authorize]
    public class QuizsController : ApiController
    {
        private EvilGenius0Entities db = new EvilGenius0Entities();

        // GET: api/Quizs
        public IQueryable<Quiz> GetQuizs()
        {
            return db.Quizs;
        }

        [Route("api/quizlist/{id}")]
        public async Task<IEnumerable<Alemni.Models.Dtos.VideoSeryViewDtos.QuizDto>> GetQuizs(int id)
        {

            IEnumerable<Alemni.Models.Dtos.VideoSeryViewDtos.QuizDto> result =  (from Quiz in db.Quizs
                                              where Quiz.videosery == id
                                            select (new Alemni.Models.Dtos.VideoSeryViewDtos.QuizDto
                                            {
                                                Id = Quiz.Id,
                                                name = Quiz.name,
                                                paid = Quiz.paid,
                                                localorder = Quiz.localorder,
                                                videosery = Quiz.videosery,
                                                questions = (from Question in Quiz.Questions

                                                             select (new Alemni.Models.Dtos.VideoSeryViewDtos.QuestionDto
                                                             {
                                                                 Id = Question.Id,
                                                                 title = Question.title,
                                                                 localorder = Question.localorder,
                                                                 quiz = Question.quiz
                                                             })).OrderBy(x => x.localorder).ToList()
                                            })).OrderBy(x => x.localorder);

            return result;
        }
        // GET: api/Quizs/5
        [ResponseType(typeof(Quiz))]
        public async Task<IHttpActionResult> GetQuiz(int id)
        {
            Quiz quiz = await db.Quizs.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            return Ok(quiz);
        }

        // GET: api/Quizs/5
        [Route("api/Quizs/GetQuizViewModel/{id}")]
        public async Task<Alemni.Models.Dtos.QuizViewDtos.QuizDto> GetQuizViewModel(int id)
        {

            Alemni.Models.Dtos.QuizViewDtos.QuizDto result = (from Quiz in db.Quizs
                                           where Quiz.videosery == id
                                           select (new Alemni.Models.Dtos.QuizViewDtos.QuizDto
                                           {
                                               Id = Quiz.Id,
                                               name = Quiz.name,
                                               paid = Quiz.paid,
                                               videosery = Quiz.videosery,
                                               url = Quiz.url,
                                               questions = (from Question in Quiz.Questions

                                                            select (new Alemni.Models.Dtos.QuizViewDtos.QuestionDto
                                                            {
                                                                Id = Question.Id,
                                                                title = Question.title,
                                                                localorder = Question.localorder,
                                                                quiz = Question.quiz,
                                 
                                                                answers = (from Answer in Question.Answers

                                                                           select (new AnswerDto
                                                                           {
                                                                               Id = Answer.Id,
                                                                               iscorrect = Answer.iscorrect,
                                                                             
                                                                               question = Answer.question,
                                                                               text = Answer.text
                                                                             
                                                                           })).ToList()
                                                            })).OrderBy(x => x.localorder).ToList()
                                           })).FirstOrDefault();

            return result;
        }
        // PUT: api/Quizs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutQuiz(int id, Quiz quiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != quiz.Id)
            {
                return BadRequest();
            }

            db.Entry(quiz).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizExists(id))
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

        // POST: api/Quizs
        [ResponseType(typeof(Quiz))]
        public async Task<IHttpActionResult> PostQuiz(Quiz quiz)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Quizs.Add(quiz);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = quiz.Id }, quiz);
        }

        // DELETE: api/Quizs/5
        [ResponseType(typeof(Quiz))]
        public async Task<IHttpActionResult> DeleteQuiz(int id)
        {
            Quiz quiz = await db.Quizs.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            db.Quizs.Remove(quiz);
            await db.SaveChangesAsync();

            return Ok(quiz);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuizExists(int id)
        {
            return db.Quizs.Count(e => e.Id == id) > 0;
        }
    }
}