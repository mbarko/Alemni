using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alemni.Models.Dtos
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
 //       public byte[] password { get; set; }
        public int usertype { get; set; }

        //public virtual Teacher Teacher { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Transaction> Transactions { get; set; }
        //public virtual UserType UserType1 { get; set; }
    }
}