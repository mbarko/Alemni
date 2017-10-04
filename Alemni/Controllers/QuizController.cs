using Alemni.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Alemni.Controllers
{
    public class QuizController : Controller
    {
        // GET: Quiz
        [System.Web.Mvc.Route("Quiz")]

        [System.Web.Mvc.Route("Quiz/{id}")]
        public async Task<ActionResult> Quiz(int id)
        {
            var quizControllerApi = new Api.QuizsController();
            var quizDto = await quizControllerApi.GetQuizViewModel(id);
            var quizModel = new QuizViewModel
            {
                QuizDto = quizDto
            };
            return View(quizModel);
        }
    }
}