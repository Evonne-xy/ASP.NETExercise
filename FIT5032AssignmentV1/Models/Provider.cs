using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FIT5032AssignmentV1.Models
{
    public class Provider
    {
        public int ProviderId { get; set; }

        [Required(ErrorMessage = "Provider name is required")]
        public string ProviderName { get; set; }

        [Required(ErrorMessage = "Provider address is required")]
        public string ProviderAddress { get; set; }
    }
}