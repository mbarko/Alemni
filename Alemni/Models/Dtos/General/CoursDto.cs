using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alemni.Models.Dtos
{
    public class CoursDto
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int program { get; set; }

        
        public ProgrammDto Programm { get; set; }
    }
}