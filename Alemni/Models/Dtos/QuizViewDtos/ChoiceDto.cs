using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alemni.Models.Dtos.QuizViewDtos
{
    public class ChoiceDto
    {
        public int Id { get; set; }
        public bool modifiable { get; set; }
        public int answer { get; set; }

    }
}