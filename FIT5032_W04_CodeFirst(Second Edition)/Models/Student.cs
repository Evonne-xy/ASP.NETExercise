using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FIT5032_W04_CodeFirst_Second_Edition_.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }

        public String FirstName { get; set; }
        public String LastName { get; set; }

        public virtual ICollection<Unit> Units { get; set; }
    }
}