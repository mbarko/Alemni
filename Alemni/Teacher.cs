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
    
    public partial class Teacher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Teacher()
        {
            this.VideoSeries = new HashSet<VideoSery>();
        }
    
        public string Id { get; set; }
        public int collectioninfo { get; set; }
        public int collectiontype { get; set; }
        public byte[] photo { get; set; }
        public string credentials { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual CollectionInfo CollectionInfo1 { get; set; }
        public virtual CollectionType CollectionType1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VideoSery> VideoSeries { get; set; }
    }
}
