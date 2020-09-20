using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FIT5032AssignmentV1.Models
{
    public class CourseType
    {
        public int CourseTypeID { get; set; }
         
       [Required(ErrorMessage = "CourseType name is required")]
       [StringLength (60,MinimumLength = 3)]
        public string CourseTypeName { get; set; }

        [Required(ErrorMessage = "CourseType description is required")]
        [StringLength(500, MinimumLength = 10)]
        public string CourseTypeDec { get; set; }

        public string Image { get; set; }
    }
}