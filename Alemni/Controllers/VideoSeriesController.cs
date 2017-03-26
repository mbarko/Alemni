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
            var transactionsControllerApi = new Api.TransactionsController();
            var sectionsControllerApi = new Api.SectionsController();
           
            var activeTransactions = new List<Transaction>();



            var videoSeryViewDto = videoSeriesControllerApi.GetVideoSery(id);
            if (videoSeryViewDto == null)
                return HttpNotFound();
            var videosControllerApi = new Api.VideosController();
            var videosDto = await videosControllerApi.GetVideos(id);
            var sectionsDto = await sectionsControllerApi.GetSections(id);
          

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


                var viewModel = new VideoSeryViewModel
                {
                    VideoSery = videoSeryViewDto,
                    Videos = videosDto.ToList(),
                    Sections = sectionsDtoList,
                    State = state,

                };
                return View(viewModel);
           

        
           
            
           
         
        }
    }
}