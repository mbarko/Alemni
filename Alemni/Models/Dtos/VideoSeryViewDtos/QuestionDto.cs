using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alemni.Models.Dtos.VideoSeryViewDtos
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string title { get; set; }
        public int quiz { get; set; }
        public int localorder { get; set; }
    }
}