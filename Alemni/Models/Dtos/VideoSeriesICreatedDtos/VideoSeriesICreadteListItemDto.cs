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
        public int VideoSeryId { get; set; }
        public string VideoSeryName { get; set; }
        
        [Required(ErrorMessage = "Price has to be greater than 399 L.E. & rounded")]
        [Range(400, int.MaxValue, ErrorMessage = "Price has to be greater than 399 L.E. & rounded")]
        public int Price { get; set; }
        public int Views { get; set; }
        public int Enrollments { get; set; }
        public bool Approved { get; set; }
        public string SectionName { get; set; }
        public int LocalOrder { get; set; }

        public decimal Profit { get; set; }

      


    }
}