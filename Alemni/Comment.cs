//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Alemni
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public string Id { get; set; }
        public System.DateTime created { get; set; }
        public string content { get; set; }
        public string fullname { get; set; }
        public int upvote_count { get; set; }
        public bool user_has_upvoted { get; set; }
        public string parent { get; set; }
        public bool createdByCurrentUser { get; set; }
        public System.DateTime modified { get; set; }
    }
}
