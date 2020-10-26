using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FIT5032AssignmentV1.Models
{
    public class fileUpload
    {
        [Key]
        public int FileId{ get; set; }

        public string Path{ get; set; }
        public string Name{ get; set; }
    }
}