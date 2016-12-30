using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alemni.Models.Dtos.VideoSeriesICreatedDtos
{
    public class VideoSeriesICreadteListItemDto
    {
        public int Id { get; set; }
        public string name { get; set; }
        
        [Required(ErrorMessage = "Price has to be greater than 399 L.E. & rounded")]
        [Range(400, int.MaxValue, ErrorMessage = "Price has to be greater than 399 L.E. & rounded")]
        public int price { get; set; }
        public int views { get; set; }
        public int enrollments { get; set; }

        public bool approved { get; set; }

        public decimal profit { get; set; }

      


    }
}