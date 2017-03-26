using Alemni.Models.Dtos.ProgramViewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alemni.ViewModels
{
    public class ProgramViewModel
    {

        public IEnumerable<TeacherDto> Teachers{ get; set; }
        public IEnumerable<CoursDto> Courses { get; set; }
        public String ImageURL { get; set; }
    }
}