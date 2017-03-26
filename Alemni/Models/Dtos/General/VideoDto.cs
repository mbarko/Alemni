using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alemni.Models.Dtos
{
    public class VideoDto
    {
        public int Id { get; set; }
        public string name { get; set; }
        public bool paid { get; set; }
        public string URL { get; set; }
        public int videoseries { get; set; }
        public int section { get; set; }
        public string overview { get; set; }
        public Nullable<System.TimeSpan> duration { get; set; }
    }
}