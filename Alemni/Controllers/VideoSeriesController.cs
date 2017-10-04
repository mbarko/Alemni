using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Alemni.Models;
using Alemni.Models.Dtos;
using Alemni.ViewModels;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Alemni.Controllers
{
    [System.Web.Http.Authorize]
    public class VideoSeriesController : Controller
    {

        // GET: VideoSeries/MyVideoSeries
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("VideoSeries/MyVideoSeries")]
       
        public async Task<ActionResult> MyVideoSeries()
        {
           

            return View();
        }
        public ActionResult VideoSeriesICreated()
        {


            return View();
        }

        // GET: VideoSeries
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("VideoSeries")]
        [System.Web.Http.AllowAnonymous]
        public ActionResult VideoSeries(string search)
        {

            
            if (!String.IsNullOrEmpty(search))
            {

                ViewBag.search = search;
                ViewBag.NavBarType = "Image Nav Bar";

            }
            else
            {
                ViewBag.NavBarType = "Image Nav Bar";
                ViewBag.search = "nothing";
            }

            return View();
            
        }

        // GET: Teacher/search
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("Teacher/{id}")]
        [System.Web.Http.AllowAnonymous]
        public async Task<ActionResult> TeacherVideoSeries(string id)
        {

            var teacherControllerApi = new Api.TeachersController();
            var teacher = await teacherControllerApi.GetTeacherForteacherView(id);

            if (teacher != null)
            {

                ViewBag.search = teacher.title + " " + teacher.firstname +" "+teacher.lastname;
                ViewBag.NavBarType = "Image Nav Bar";

            }
            else
            {
                ViewBag.NavBarType = "Image Nav Bar";
                ViewBag.search = "nothing";
            }
            var viewModel = new TeacherViewModel
            {
                Teacher = teacher
            };
            return View(viewModel);
        }


        // GET: SubjectVideoSeries/search
        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("SubjectVideoSeries/{search}")]
        [System.Web.Http.AllowAnonymous]
        public ActionResult SubjectVideoSeries(string search)
        {


            if (!String.IsNullOrEmpty(search))
            {

                ViewBag.search = search;
                ViewBag.NavBarType = "Image Nav Bar";

            }
            else
            {
                ViewBag.NavBarType = "Image Nav Bar";
                ViewBag.search = "nothing";
            }
            return View();
        }

        // GET: VideoSeries/VideoSery/Id
        [System.Web.Mvc.Route("VideoSeries/VideoSery/{id}",Name = "VideoSeryPage")]
        [System.Web.Http.AllowAnonymous]
        public async Task<ActionResult> VideoSery(int id)
        {
            ViewBag.NavBarType = "";

            var videoSeriesControllerApi = new Api.VideoSeriesController();
            var videosControllerApi = new Api.VideosController();
            var transactionsControllerApi = new Api.TransactionsController();
            var sectionsControllerApi = new Api.SectionsController();
            var questionsControllerApi = new Api.QuestionsController();
            var quizControllerApi = new Api.QuizsController();
            var activeTransactions = new List<Transaction>();



            var videoSeryViewDto = videoSeriesControllerApi.GetVideoSery(id);
            if (videoSeryViewDto == null)
                return HttpNotFound();
           
            var videosDto = await videosControllerApi.GetVideos(id);
            var sectionsDto = await sectionsControllerApi.GetSections(id);
            var quizsDto = await quizControllerApi.GetQuizs(id);

            bool loggedIn = (System.Web.HttpContext.Current.User != null) &&
                            System.Web.HttpContext.Current.User.Identity.IsAuthenticated;


            //Check transactions for this student are exitent, active, and belong to the current VideoSery 

            if (loggedIn)
            {
                 activeTransactions = await transactionsControllerApi.GetMyActiveVideoSeryTransactions(id);
            }

            Boolean state = false;
            var sectionsDtoList = sectionsDto.ToList();
            foreach (var i in sectionsDtoList)
            {
                if (activeTransactions.Where(a => a.section == i.Id).Count()>0)

                   i.state = true;
            }

            String currentUserId ="";
            String firstName = "";
            String lastName = "";

            using (EvilGenius0Entities db = new EvilGenius0Entities())
            {
                try
                {
                    
                    currentUserId = User.Identity.GetUserId();

                    AspNetUser currentUser = db.AspNetUsers.FirstOrDefault(x => x.Id == currentUserId);

                    firstName = currentUser.firstName;
                    lastName = currentUser.lastName;
                }
                catch(Exception e) { }
                finally { db.Dispose(); }
               
            }
            var viewModel = new VideoSeryViewModel
            {
                VideoSery = videoSeryViewDto,
                Videos = videosDto.ToList(),
                Sections = sectionsDtoList,
                Quizs = quizsDto.ToList(),
                State = state,
               User = new Models.Dtos.VideoSeryViewDtos.UserDto()
               {
                   id =currentUserId,
                   firstName = firstName,
                   lastName = lastName

        }

                };
                return View(viewModel);
           

        
           
            
           
         
        }
    }
}