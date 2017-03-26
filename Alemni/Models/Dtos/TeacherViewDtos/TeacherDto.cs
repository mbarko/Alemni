using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alemni.Models.Dtos.TeacherViewDtos
{
    public class TeacherDto
    {
        public String email { get; set; }
        public String firstname { get; set; }
        public String lastname { get; set; }
        public String title { get; set; }
        public String id { get; set; }
        public String teacherimage { get; set; }
        public String credentials { get; set; }
        public String phone { get; set; }
        public String certification { get; set; }
        public String cover { get; set; }
    }
}