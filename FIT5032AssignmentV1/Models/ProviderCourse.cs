using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FIT5032AssignmentV1.Models
{
    public class ProviderCourse
    {
        public int ProviderCourseId { get; set; }

        [Required(ErrorMessage = "Course name is required")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Course time is required")]
        //[DataType(DataType.DateTime)]
        public DateTime CourseTime { get; set; }

        public double AggregateRating { get; set; }

        public CourseType CourseType { get; set; }
        public int CourseTypeId { get; set; }

        public Provider Provider { get; set; }
        public int ProviderId { get; set; }

    }
}