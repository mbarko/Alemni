using Alemni.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Alemni.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.NavBarType = "Image Nav Bar";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // GET: Program
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("Program/{programName}")]
        [System.Web.Http.AllowAnonymous]
        public ActionResult Program(string programName)
        {
            var TeachersControllerApi = new Api.TeachersController();
       

            ViewBag.NavBarType = "Image Nav Bar";

            if (!String.IsNullOrEmpty(programName))
            {

                ViewBag.search = programName;
               

            }
            else
            {
   
                ViewBag.programName = "non specified ";
            }


            var viewModel = TeachersControllerApi.GetProgramViewModel(programName);
            return View(viewModel);

        }




    }
}