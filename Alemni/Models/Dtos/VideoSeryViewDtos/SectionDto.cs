using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alemni.Models.Dtos.VideoSeryViewDtos
{
    public class SectionDto
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int videosery { get; set; }
        public int localorder { get; set; }
        public Boolean state { get; set; }
    }
}