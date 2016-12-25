using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alemni.Models.Dtos.VidoSeriesViewDtos
{
    public class VideoSeriesListItemDto
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int views { get; set; }
        public int enrollments { get; set; }
        public int videos { get; set; }
 
        public String teacherName { get; set; }
        public String programmName { get; set; }

        public bool approved { get; set; }
        public double duration { get; set; }
        public string seryimage { get; set; }

       
        public System.DateTime startdate { get; set; }
  
        public System.DateTime enddate { get; set; }

        public bool active { get; set; }

    }
}