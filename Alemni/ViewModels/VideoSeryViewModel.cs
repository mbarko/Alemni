using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alemni.Models.Dtos;
using Alemni.Models.Dtos.VideoSeryViewDtos;

namespace Alemni.ViewModels
{
    public class VideoSeryViewModel
    {
        public VideoSeryViewDto VideoSery { get; set; }
        public IEnumerable< VideoDto> Videos { get; set; }
        public IEnumerable<VideoLockedDto> VideosLocked { get; set; }
        public bool State { get; set; }
    }
}