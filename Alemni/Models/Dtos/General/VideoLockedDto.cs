using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alemni.Models.Dtos
{
    public class VideoLockedDto
    {
        public int Id { get; set; }
        public string name { get; set; }
        public bool paid { get; set; }
        public string URL { get; }= "none";
    public int videoseries { get; set; }

        public Nullable<System.TimeSpan> duration { get; set; }
    }
}