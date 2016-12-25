using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alemni.Models.Dtos
{
    public class VideoSeryDto
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int views { get; set; }
        public int enrollments { get; set; }
        public int videos { get; set; }
        public string description { get; set; }
        public String teacher { get; set; }
        public int course { get; set; }
        public bool approved { get; set; }
        public float duration { get; set; }
        public string seryimage { get; set; }
        public CoursDto Cours { get; set; }
        public TeacherDto Teacher1 { get; set; }
        public string overview { get; set; }
    }
}