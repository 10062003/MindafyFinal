using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mindafy.Models.ViewModels
{
    public class ActivityModel
    {
        public ActivityModel()
        {
            Conforms = new HashSet<Conforms>();
            Inserts = new HashSet<insert>();
        }

        public int IdActivity { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date Activity")]
        public System.DateTime? StartDateActivity { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date Activity")]
        public System.DateTime? EndDateActivity { get; set; }
        [StringLength(100)]
        [Display(Name = "Description")]
        public string DescriptionActivity { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string NameActivity { get; set; }
        [Display(Name = "State of the Activity")]
        public bool? State { get; set; }
        [Display(Name = "Note of the Activity")]
        public double? Note { get; set; }

        public virtual ICollection<Conforms> Conforms { get; set; }
        public virtual ICollection<insert> Inserts { get; set; }
    }
}