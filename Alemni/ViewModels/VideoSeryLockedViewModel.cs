using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alemni.Models.Dtos;

namespace Alemni.ViewModels
{
    public class VideoSeryLockedViewModel
    {
        public VideoSeryDto VideoSery { get; set; }
        public IEnumerable<VideoLockedDto> Videos { get; set; }
        public bool State { get; set; }
    }
}