using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alemni.Models.Dtos.VideoSeryViewDtos
{
    public class CommentDto
    {
        
        public System.DateTime created { get; set; }
        public string content { get; set; }
        public string fullname { get; set; }
        public int upvote_count { get; set; }
        public bool user_has_upvoted { get; set; }
        public Nullable<int> parent { get; set; }
        public bool createdByCurrentUser { get; set; }
        public System.DateTime modified { get; set; }
   
        public string cId { get; set; }

    }
}