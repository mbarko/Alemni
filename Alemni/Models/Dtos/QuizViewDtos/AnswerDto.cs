using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alemni.Models.Dtos.QuizViewDtos
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public bool iscorrect { get; set; }
        public int question { get; set; }
        public string text { get; set; }
    }
}