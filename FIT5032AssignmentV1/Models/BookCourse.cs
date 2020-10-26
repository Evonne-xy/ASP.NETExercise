using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FIT5032AssignmentV1.Models
{
    public class BookCourse
    {
        [Key]
        public int BookCourseId { get; set; }

        public int Rating { get; set; }

        public ApplicationUser applicationUser { get; set; }
        public String ApplicationUserId { get; set; }

        public ProviderCourse providerCourse { get; set; }
        public int ProviderCourseId { get; set; }
    }
}