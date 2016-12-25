using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alemni.Models.Dtos
{
    public class TransactionDto
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

        public virtual AspNetUserDto AspNetUser { get; set; }
        public virtual CollectionInfoDto CollectionInfo { get; set; }
        public virtual VideoSeryDto VideoSery { get; set; }
    }
}