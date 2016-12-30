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
                
            }
            else
            {
                ViewBag.search = "nothing";
            }
            return View();
        }




        // GET: VideoSeries/VideoSery/Id
        [System.Web.Mvc.Route("VideoSeries/VideoSery/{id}",Name = "VideoSeryPage")]
        [System.Web.Http.AllowAnonymous]
        public async Task<ActionResult> VideoSery(int id)
        {
            ViewBag.NavBarType = "Image Nav Bar";

            var videoSeriesControllerApi = new Api.VideoSeriesController();
            var transactionsControllerApi = new Api.TransactionsController();
            var activeTransactions = new List<Transaction>();



            var videoSeryViewDto = videoSeriesControllerApi.GetVideoSery(id);    
            var videosControllerApi = new Api.VideosController();
            var videosDto = await videosControllerApi.GetVideos(id);
            if (videoSeryViewDto == null)
                return HttpNotFound();

            bool loggedIn = (System.Web.HttpContext.Current.User != null) &&
                            System.Web.HttpContext.Current.User.Identity.IsAuthenticated;


            //Check transactions for this student are exitent, active, and belong to the current VideoSery 

            if (loggedIn)
            {
                 activeTransactions = await transactionsControllerApi.GetMyActiveVideoSeryTransactions(id);
            }
           

            if (activeTransactions.Count > 0)
            {
                var viewModel = new VideoSeryViewModel
                {
                    VideoSery = videoSeryViewDto,
                    Videos = videosDto,
                    State= true

                };
                return View(viewModel);
            }
            else
            {
                var videosLockedDto = videosDto.Select(Mapper.Map<VideoDto, VideoLockedDto>);
            
            var viewModel = new VideoSeryViewModel
            {
                    VideoSery = videoSeryViewDto,
                    VideosLocked = videosLockedDto,
                    State = false
                };
                return View(viewModel);
            }
        
           
            
           
         
        }
    }
}