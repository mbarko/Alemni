using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alemni.Models.Dtos.VideoSeriesICreatedDtos
{
    public class VideoSeriesICreatedPatchDto
    {
        public int Id { get; set; }

        public int price { get; set; }

        public bool approved { get; set; }
    }
}