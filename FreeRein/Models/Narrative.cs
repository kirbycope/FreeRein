using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace FreeRein.Models
{
    public class Narrative
    {
        [Display(Name = "Narrative ID")]
        public int ID { get; set; }

        [Display(Name = "Parent ID")]
        public int ParentID { get; set; }

        [Display(Name = "Parent Option ID")]
        public int ParentOptionID { get; set; }

        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [Display(Name = "Story")]
        public string Story { get; set; }

        [Display(Name = "Option 1")]
        public string Option1 { get; set; }

        [Display(Name = "Option 2")]
        public string Option2 { get; set; }
    }
}