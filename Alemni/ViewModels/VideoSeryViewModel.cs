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
        public List< VideoDto> Videos { get; set; }
        public UserDto User { get; set; }
        public List<SectionDto> Sections { get; set; }
        public List<QuizDto> Quizs { get; set; }
        public bool State { get; set; }
    }
}