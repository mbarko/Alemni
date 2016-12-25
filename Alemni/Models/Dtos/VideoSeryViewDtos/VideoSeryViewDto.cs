using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alemni.Models.Dtos.VideoSeryViewDtos
{
    public class VideoSeryViewDto
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
        public double duration { get; set; }
        public string seryimage { get; set; }
        public string overview { get; set; }
        //Teacher info for this view
        public String teacherName { get; set; }
        public String teacherCredentials { get; set; }
        public String teacherEmail { get; set; }
        public String teacherPhone{ get; set; }
        public String programmName { get; set; }


     
    }
}