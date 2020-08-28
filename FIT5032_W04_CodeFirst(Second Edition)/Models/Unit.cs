using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FIT5032_W04_CodeFirst_Second_Edition_.Models
{
    public class Unit
    {
        [Key]
        public int UnitID { get; set; }

        public String Name { get; set; }
        public String Description { get; set; }

        public int StudentID { get; set; }

        public virtual Student Student { get; set; }
    }
}