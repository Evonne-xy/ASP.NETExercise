using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FIT5032_W04_CodeFirst_Second_Edition_.Models
{
    public class CsharpDBContext : DbContext
    {
        public CsharpDBContext() : base("CsharpConnection")
        {
        }
        public DbSet<Student> students { get; set; }
        public DbSet<Unit> units { get; set; }
    }
}