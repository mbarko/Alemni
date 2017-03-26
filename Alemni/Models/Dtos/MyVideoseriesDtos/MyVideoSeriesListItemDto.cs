using Alemni.Models.Dtos.VidoSeriesViewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alemni.Models.Dtos.MyVideoseriesDtos
{
    public class MyVideoSeriesListItemDto
    {
        public VideoSeriesListItemDto VideoSery{ get; set; }
        public int? Count { get; set; }
    }
}