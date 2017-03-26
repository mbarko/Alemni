using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alemni.Models.Dtos.ProgramViewDtos
{
    public class TeacherDto
    {
        public String email { get; set; }
        public String firstname { get; set; }
        public String lastname { get; set; }
        public String title { get; set; }
        public String id { get; set; }
        public String teacherimage { get; set; }
    }
}