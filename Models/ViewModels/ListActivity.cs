using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mindafy.Models.ViewModels
{
    public class ListActivity
    {
        public int? IdSubject { get; set; }
        public int IdActivity { get; set; }
        public string NameActivity { get; set; }
        public string DescriptionActivity  { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date Activity")]
        public System.DateTime? StartDateActivity   { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date Activity")]
        public System.DateTime? EndDateActivity     { get; set; }
        public bool? State { get; set; }
        public double? Note { get; set; }

        public virtual Subject IdSubjectNavigation { get; set; }

    }
}
