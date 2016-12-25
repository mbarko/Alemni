using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alemni.Models.Dtos
{
    public class TeacherDto
    {
        public String Id { get; set; }
        public int collectioninfo { get; set; }
        public int collectiontype { get; set; }
        public byte[] photo { get; set; }
        public string credentials { get; set; }

        public CollectionInfoDto CollectionInfo1 { get; set; }
        //public CollectionType CollectionType1 { get; set; }
        public AspNetUserDto AspNetUser { get; set; }
    }
}