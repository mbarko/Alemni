using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alemni.Models.Dtos.QuizViewDtos
{
    public class QuizDto
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int videosery { get; set; }
        public Boolean paid { get; set; }
        public string url { get; set; }

        public List<QuestionDto> questions { get; set; }
    }
}