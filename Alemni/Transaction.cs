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
    
    public partial class Transaction
    {
        public int Id { get; set; }
        public string student { get; set; }
        public int videoseries { get; set; }
        public System.DateTime startdate { get; set; }
        public decimal payment { get; set; }
        public bool paymentstatus { get; set; }
        public int payerInfo { get; set; }
        public System.DateTime enddate { get; set; }
        public bool active { get; set; }
        public Nullable<int> section { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual CollectionInfo CollectionInfo { get; set; }
        public virtual VideoSery VideoSery { get; set; }
        public virtual Section Section1 { get; set; }
    }
}
